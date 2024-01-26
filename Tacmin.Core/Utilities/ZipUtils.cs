using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.Collections.Generic;
using System.IO;

namespace Tacmin.Core.Utilities
{
    public class ZipUtils
    {
        public static bool TryDecompressZlib(byte[] input, out byte[] output)
        {
            try
            {
                output = DecompressZlib(input);
                return true;
            }
            catch
            {
                output = null;
                return false;
            }
        }

        public static byte[] DecompressZlib(byte[] input)
        {
            var source = new MemoryStream(input);
            byte[] result = null;
            using (var outStream = new MemoryStream())
            {
                using (var inf = new InflaterInputStream(source))
                {
                    inf.CopyTo(outStream);
                }
                result = outStream.ToArray();
            }
            return result;
        }

        public static byte[] CompressZlib(byte[] input)
        {
            var m = new MemoryStream();
            var zipStream = new DeflaterOutputStream(m, new ICSharpCode.SharpZipLib.Zip.Compression.Deflater(8));
            zipStream.Write(input, 0, input.Length);
            zipStream.Finish();
            return m.ToArray();
        }

        public static byte[] CreateZipFolder(Dictionary<string, byte[]> files)
        {
            var outputMemStream = new MemoryStream();
            var zipStream = new ZipOutputStream(outputMemStream);
            zipStream.SetLevel(3);

            foreach (var file in files)
            {
                var zipentry = new ZipEntry(file.Key);
                zipentry.IsUnicodeText = true;
                zipentry.DateTime = DateTime.Now;
                zipStream.PutNextEntry(zipentry);
                var memBeyan = new MemoryStream(file.Value);
                StreamUtils.Copy(memBeyan, zipStream, new byte[4096]);
                memBeyan.Close();
                zipStream.CloseEntry();
            }

            zipStream.IsStreamOwner = false;
            zipStream.Close();

            outputMemStream.Position = 0;

            return outputMemStream.ToArray();
        }
    }
}
