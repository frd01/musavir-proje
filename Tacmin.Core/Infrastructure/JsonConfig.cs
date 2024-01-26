using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.IO;
using System.Threading.Tasks;
using Tacmin.Core.Extensions;

namespace Tacmin.Core.Infrastructure
{
    public class JsonConfig
    {
        public dynamic Json { get; set; }
        public string Content { get; private set; }
        public string WatchedFile => (Watcher != null ? Path.Combine(Watcher.Path, Watcher.Filter) : "");
        private JsonConfig OverrideConfig;
        public FileSystemWatcher Watcher { get; private set; }

        public delegate void ConfigFileChangedHandler(ConfigChangedEventArgs e);

        public event ConfigFileChangedHandler OnConfigFileChanged;

        public delegate void ConfigFileErrorHandler(Exception e);

        public event ConfigFileErrorHandler OnConfigFileError;

        private JsonConfig(dynamic parsed)
        {
            Json = ConfigObject.FromExpando(parsed);
        }

        public static dynamic ParseJson(string json)
        {
            dynamic parsed = JsonConvert.DeserializeObject<ExpandoObject>(json);
            return parsed;
        }

        public static JsonConfig From(string json)
        {
            var parsed = ParseJson(json);
            return new JsonConfig(parsed)
            {
                Content = json
            };
        }

        private void Watch(string file)
        {
            var info = new FileInfo(file);
            Watcher = new FileSystemWatcher(info.Directory.FullName, info.Name)
            {
                IncludeSubdirectories = false,
                EnableRaisingEvents = false,
            };

            Watcher.Changed += OnConfigFileChangedFileSystemWatcherEvent;
            Watcher.Created += OnConfigFileChangedFileSystemWatcherEvent;
            Watcher.Renamed += OnConfigFileChangedFileSystemWatcherEvent;

            Watcher.EnableRaisingEvents = true;
        }

        public static dynamic FromFile(string file, string override_file = null)
        {
            var json = "{}";

            if (File.Exists(file))
            {
                using (var sr = new StreamReader(new FileStream(file, FileMode.Open, FileAccess.Read)))
                {
                    json = sr.ReadToEnd();
                }
            }

            var cfg = From(json);

            cfg.Watch(file);

            if (override_file != null)
            {
                cfg.OverrideConfig = JsonConfig.FromFile(override_file);
                cfg.Json = Merger.Merge(cfg.OverrideConfig.Json, cfg.Json);

                cfg.OverrideConfig.OnConfigFileChanged += cfg.OverrideConfig_OnConfigFileChanged;
                cfg.OverrideConfig.Watcher.Deleted += cfg.OverrideConfig_Deleted;
                cfg.OverrideConfig.Watcher.Renamed += cfg.OverrideConfig_Renamed;
                cfg.OverrideConfig.OnConfigFileError += cfg.OverrideConfig_OnConfigFileError;

                cfg.OnConfigFileChanged += cfg.cfg_OnConfigFileChanged;
            }

            return cfg;
        }

        private void OverrideConfig_Renamed(object sender, RenamedEventArgs e)
        {
            if (e == null || e.FullPath != OverrideConfig.WatchedFile)
            {
                var src = "{}";
                OverrideConfig.Json = ParseJson(src);
                OverrideConfig.Content = src;
                ReloadWatchedFile();
            }
        }

        private void OverrideConfig_OnConfigFileError(Exception e)
        {
            RaiseConfigFileError(e);
        }

        private void OverrideConfig_Deleted(object sender, FileSystemEventArgs e)
        {
            var src = "{}";
            OverrideConfig.Json = ParseJson(src);
            OverrideConfig.Content = src;
            ReloadWatchedFile();
        }

        private void cfg_OnConfigFileChanged(ConfigChangedEventArgs e)
        {
            Json = Merger.Merge(OverrideConfig.Json, Json);
        }

        private void OverrideConfig_OnConfigFileChanged(ConfigChangedEventArgs e)
        {
            ReloadWatchedFile(slient: true);
            Json = Merger.Merge(OverrideConfig.Json, Json);

            var configobject = Json as ConfigObject;

            var content = configobject.GetJsonToSave();

            var eargs = new ConfigChangedEventArgs
            {
                Content = content
            };

            RaiseConfigFileChanged(eargs);
        }

        private Task<string> ReadWhenAvailable(string file, TimeSpan? ts = null)
        {
            var task = Task.Run(() =>
            {
                ts = ts ?? new TimeSpan(long.MaxValue);
                var start = DateTime.Now;
                while (DateTime.Now - start < ts)
                {
                    Task.Delay(100);
                    try
                    {
                        using (var sr = new StreamReader(new FileStream(file, FileMode.Open, FileAccess.Read)))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                    catch { }
                }
                return null;
            });

            return task;
        }

        private void OnConfigFileChangedFileSystemWatcherEvent(Object sender, FileSystemEventArgs e)
        {
            if (e == null || e.FullPath != WatchedFile)
                return;

            Watcher.EnableRaisingEvents = false;

            ReloadWatchedFile();

            Watcher.EnableRaisingEvents = true;
        }

        public void ReloadWatchedFile(bool slient = false)
        {
            if (!WatchedFile.IsEmpty())
            {
                var task = ReadWhenAvailable(WatchedFile);
                task.Wait();
                var src = task.Result;

                try
                {
                    var oldJson = Json;
                    var oldContent = Content;

                    Json = ParseJson(src);

                    var eargs = new ConfigChangedEventArgs
                    {
                        Content = src
                    };

                    if (!slient)
                        OnConfigFileChanged?.Invoke(eargs);

                    if (eargs.Cancel)
                    {
                        //iptal olmuş ise eski verileri yüklüyoruz. reload olsun diye tekrar tetikliyoruz.
                        Json = oldJson;
                        Content = oldContent;

                        if (!slient)
                        {
                            OnConfigFileChanged?.Invoke(new ConfigChangedEventArgs
                            {
                                Content = oldContent
                            });
                        }
                    }
                }
                catch (JsonException ex)
                {
                    //valid json olmayabilir
                    if (!slient)
                        OnConfigFileError?.Invoke(ex);
                }
            }
        }

        public void RaiseConfigFileChanged(ConfigChangedEventArgs e)
        {
            OnConfigFileChanged?.Invoke(e);
        }

        public void RaiseConfigFileError(Exception e)
        {
            OnConfigFileError?.Invoke(e);
        }
    }

    public class ConfigChangedEventArgs : EventArgs
    {
        public bool Cancel { get; set; }
        public string Content { get; set; }
    }
}
