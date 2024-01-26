using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Tacmin.Core.Infrastructure
{
    internal class ConfigObjectMember
    {
        public bool IsDefault => _isDefault;

        public object Value => _value;

        private readonly bool _isDefault;
        private readonly object _value;

        public ConfigObjectMember(bool isDefault, object value)
        {
            _isDefault = isDefault;
            _value = value;
        }
    }

    public class ConfigObject : DynamicObject, IDictionary<string, object>
    {
        internal volatile ConcurrentDictionary<string, object> _members = new ConcurrentDictionary<string, object>();

        #region IEnumerable implementation

        public IEnumerator GetEnumerator()
        {
            return _members.GetEnumerator();
        }

        #endregion IEnumerable implementation

        #region IEnumerable implementation

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return _members.ToList().Select(a => new KeyValuePair<string, object>(a.Key, (a.Value is ConfigObjectMember) ? ((ConfigObjectMember)a.Value).Value : a.Value)).GetEnumerator();
        }

        #endregion IEnumerable implementation

        public static ConfigObject FromExpando(ExpandoObject e, bool isDefault = false)
        {
            var edict = e as IDictionary<string, object>;
            var c = new ConfigObject();
            var cdict = c._members;

            // this is not complete. It will, however work for JsonFX ExpandoObjects
            // which consits only of primitive types, ExpandoObject or ExpandoObject []
            // but won't work for generic ExpandoObjects which might include collections etc.
            foreach (var kvp in edict)
            {
                // recursively convert and add ExpandoObjects
                if (kvp.Value is ExpandoObject)
                {
                    cdict.TryAdd(kvp.Key, FromExpando((ExpandoObject)kvp.Value, isDefault));
                }
                else if (kvp.Value is ExpandoObject[])
                {
                    var configObjects = new List<ConfigObject>();
                    foreach (var ex in ((ExpandoObject[])kvp.Value))
                    {
                        configObjects.Add(FromExpando(ex, isDefault));
                    }
                    cdict.TryAdd(kvp.Key, configObjects.ToArray());
                }
                else
                {
                    cdict.TryAdd(kvp.Key, new ConfigObjectMember(isDefault, kvp.Value));
                }
            }
            return c;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_members.TryGetValue(binder.Name, out var member))
            {
                result = (member is ConfigObjectMember) ? ((ConfigObjectMember)member).Value : member;
            }
            else
            {
                result = new ConfigObject();
                _members[binder.Name] = result;
            }
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (value is ConfigObject || value is ConfigObject[])
            {
                _members[binder.Name] = value;
            }
            else
            {
                _members[binder.Name] = new ConfigObjectMember(false, value);
            }
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (binder.Name == "Clone")
            {
                result = Clone();
                return true;
            }
            if (binder.Name == "Exists" && args.Length == 1 && args[0] is string)
            {
                result = _members.ContainsKey((string)args[0]);
                return true;
            }

            // no other methods availabe, error
            result = null;
            return false;
        }

        public override string ToString()
        {
            if (_members.Count == 0)
            {
                return "";
            }

            return JsonConvert.SerializeObject(GetMapForOutput(true));
        }

        private IDictionary<string, object> GetMapForOutput(bool isOutputDefault)
        {
            return _members.Where(a => (isOutputDefault || !(a.Value is ConfigObjectMember) || !((ConfigObjectMember)a.Value).IsDefault))
                .Select(a => new KeyValuePair<string, object>(a.Key,
                    (a.Value is ConfigObject)
                        ? ((ConfigObject)a.Value).GetMapForOutput(isOutputDefault)
                        : ((a.Value is ConfigObjectMember) ? ((ConfigObjectMember)a.Value).Value : a.Value)))
                .Where(
                    a => !(a.Value is IDictionary<string, object>) || ((IDictionary<string, object>)a.Value).Count > 0)
                .ToDictionary(a => a.Key, a => a.Value);
        }

        /// <summary>
        ///     Get the json string to save to user config json file. The config items from default config will not be saved.
        /// </summary>
        /// <returns></returns>
        internal string GetJsonToSave()
        {
            var nonDefaultMembers = GetMapForOutput(false);
            return JsonConvert.SerializeObject(nonDefaultMembers, Formatting.Indented);
        }

        public static implicit operator ConfigObject(ExpandoObject exp)
        {
            return FromExpando(exp);
        }

        #region ctor

        /// <summary>
        ///     Create a <see cref="ConfigObject" /> instance.
        /// </summary>
        ///// <param name="isDefault">Indicates whether the config object is the default config.</param>
        public ConfigObject()
        {
        }

        #endregion ctor

        #region ICollection implementation

        public void Add(KeyValuePair<string, object> item)
        {
            _members.TryAdd(item.Key, new ConfigObjectMember(false, item.Value));
        }

        public void Clear()
        {
            _members.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return _members.ContainsKey(item.Key) && _members[item.Key] == item.Value;
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, object>>)_members.ToDictionary(a => a.Key, a => (a.Value is ConfigObjectMember) ? ((ConfigObjectMember)a.Value).Value : a.Value)).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return _members.TryRemove(item.Key, out _);
        }

        public int Count => _members.Count;

        public bool IsReadOnly => false;

        #endregion ICollection implementation

        #region IDictionary implementation

        public void Add(string key, object value)
        {
            _members.TryAdd(key, new ConfigObjectMember(false, value));
        }

        public bool ContainsKey(string key)
        {
            return _members.ContainsKey(key);
        }

        public bool Remove(string key)
        {
            return _members.TryRemove(key, out _);
        }

        public object this[string key]
        {
            get
            {
                if (ReferenceEquals(key, null))
                {
                    throw new ArgumentNullException(nameof(key));
                }
                if (_members.TryGetValue(key, out var member))
                {
                    return (member is ConfigObjectMember) ? ((ConfigObjectMember)member).Value : member;
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
            set => _members[key] = new ConfigObjectMember(false, value);
        }

        public ICollection<string> Keys => _members.Keys;

        public ICollection<object> Values => _members.Values.Select(a => (a is ConfigObjectMember) ? ((ConfigObjectMember)a).Value : a).ToArray();

        public bool TryGetValue(string key, out object value)
        {
            if (_members.TryGetValue(key, out var member))
            {
                value = (member is ConfigObjectMember) ? ((ConfigObjectMember)member).Value : member;
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }

        #region ICloneable implementation

        public object Clone()
        {
            return Merger.Merge(new ConfigObject(), this);
        }

        #endregion ICloneable implementation

        #endregion IDictionary implementation

        #region operator +

        public static dynamic operator +(ConfigObject a, ConfigObject b)
        {
            return Merger.Merge(b, a);
        }

        public static dynamic operator +(dynamic a, ConfigObject b)
        {
            return Merger.Merge(b, a);
        }

        public static dynamic operator +(ConfigObject a, dynamic b)
        {
            return Merger.Merge(b, a);
        }

        #endregion operator +

        public void Set(string key, object value, bool isFromDefaultConfig)
        {
            if (value is ConfigObject || value is ConfigObject[])
            {
                _members[key] = value;
            }
            else
            {
                _members[key] = new ConfigObjectMember(isFromDefaultConfig, value);
            }
        }

        // Add all kinds of datatypes we can cast it to, and return default values
        // cast to string will be null
        public static implicit operator string(ConfigObject nep)
        {
            if (nep == null || nep._members.Count == 0)
            {
                return "";
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public static implicit operator string[](ConfigObject nep)
        {
            if (nep == null || nep._members.Count == 0)
            {
                return new string[] { };
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        // cast to bool will always be false
        public static implicit operator bool(ConfigObject nep)
        {
            if (nep == null || nep._members.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static implicit operator bool[](ConfigObject nep)
        {
            if (nep == null || nep._members.Count == 0)
            {
                return new bool[] { };
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public static implicit operator int[](ConfigObject nep)
        {
            if (nep == null || nep._members.Count == 0)
            {
                return new int[] { };
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public static implicit operator long[](ConfigObject nep)
        {
            if (nep == null || nep._members.Count == 0)
            {
                return new long[] { };
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public static implicit operator int(ConfigObject nep)
        {
            if (nep == null || nep._members.Count == 0)
            {
                return 0;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public static implicit operator long(ConfigObject nep)
        {
            if (nep == null || nep._members.Count == 0)
            {
                return 0;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        // nullable types always return null
        public static implicit operator bool?(ConfigObject nep)
        {
            if (nep == null || nep._members.Count == 0)
            {
                return null;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public static implicit operator int?(ConfigObject nep)
        {
            if (nep == null || nep._members.Count == 0)
            {
                return null;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public static implicit operator long?(ConfigObject nep)
        {
            if (nep == null || nep._members.Count == 0)
            {
                return null;
            }
            else
            {
                throw new InvalidCastException();
            }
        }
    }
}
