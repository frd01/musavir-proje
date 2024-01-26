using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Tacmin.Core.Infrastructure
{
    public static class Merger
    {
        /// <summary>
        ///     Merge the specified obj2 and obj1, where obj1 has precendence and
        ///     overrules obj2 if necessary.
        /// </summary>
        /// <exception cref='TypeMissmatchException'>
        ///     Is thrown when the type missmatch exception.
        /// </exception>
        public static dynamic Merge(dynamic m_obj1, dynamic m_obj2)
        {
            dynamic obj1 = m_obj1;
            dynamic obj2 = m_obj2;

            // make sure we only deal with ConfigObject but not ExpandoObject as currently
            if (obj1 is ExpandoObject)
                obj1 = ConfigObject.FromExpando(obj1);
            if (obj2 is ExpandoObject)
                obj2 = ConfigObject.FromExpando(obj2);

            var isObj1Default = ((obj1 is ConfigObjectMember) && ((ConfigObjectMember)obj1).IsDefault);

            var isObj1EmptyConfigObject = obj1 is ConfigObject && ((ConfigObject)obj1).Count == 0;
            var isObj2EmptyConfigObject = obj2 is ConfigObject && ((ConfigObject)obj2).Count == 0;

            // if both objects are empty ConfigObject, return a ConfigObject so the
            // user gets an "Empty" ConfigObject
            if (isObj1EmptyConfigObject && isObj2EmptyConfigObject)
                return new ConfigObject(/*isObj1Default*/);

            if (isObj2EmptyConfigObject && obj1 is ConfigObject)
                return obj1;

            // handle what happens if one of the args is null
            if (obj1 == null && obj2 == null)
                return new ConfigObject();

            if (obj2 == null)
                return obj1;
            if (obj1 == null)
                return obj2;

            if (obj1.GetType() != obj2.GetType())
                throw new TypeMissmatchException();

            // changes in the dictionary WILL REFLECT back to the object
            var dict1 = (IDictionary<string, object>)(obj1);
            var dict2 = (IDictionary<string, object>)(obj2);

            var result = new ConfigObject();
            //var rdict = (IDictionary<string, object>) result;

            // first, copy all non colliding keys over
            foreach (var kvp in dict1)
            {
                if (!dict2.Keys.Contains(kvp.Key))
                {
                    var isDefault = false;
                    if (obj1 is ConfigObject)
                    {
                        if (((ConfigObject)obj1)._members.TryGetValue(kvp.Key, out var member) && member is ConfigObjectMember && ((ConfigObjectMember)member).IsDefault)
                        {
                            isDefault = true;
                        }
                    }
                    result.Set(kvp.Key, kvp.Value, isDefault);
                }
            }

            foreach (var kvp in dict2)
            {
                if (!dict1.Keys.Contains(kvp.Key))
                {
                    var isDefault = false;
                    if (obj2 is ConfigObject)
                    {
                        if (((ConfigObject)obj2)._members.TryGetValue(kvp.Key, out var member) && member is ConfigObjectMember && ((ConfigObjectMember)member).IsDefault)
                        {
                            isDefault = true;
                        }
                    }
                    result.Set(kvp.Key, kvp.Value, isDefault);
                }
            }

            // now handle the colliding keys
            foreach (var kvp1 in dict1)
            {
                // skip already copied over keys
                if (!dict2.Keys.Contains(kvp1.Key) || dict2[kvp1.Key] == null)
                    continue;

                var kvp2 = new KeyValuePair<string, object>(kvp1.Key, dict2[kvp1.Key]);

                // some shortcut variables to make code more readable
                object valueToAdd = null;
                var key = kvp1.Key;
                var value1 = kvp1.Value;
                var value2 = kvp2.Value;
                if (ReferenceEquals(null, value1))
                {
                    valueToAdd = value2;
                }
                else if (ReferenceEquals(null, value2))
                {
                    valueToAdd = value1;
                }
                else
                {
                    var type1 = value1.GetType();
                    var type2 = value2.GetType();
                    if (type1.IsArray && ((Array)value1).Length == 0)
                    {
                        valueToAdd = value2;
                    }
                    else if (type2.IsArray && ((Array)value2).Length == 0)
                    {
                        valueToAdd = value1;
                    }
                    else
                    {
                        // check if both are same type
                        if (type1 != type2)
                            throw new TypeMissmatchException();

                        if (value1 is ConfigObject[])
                        {
                            valueToAdd = CollectionMerge(value1, value2);
                        }
                        else if (value1 is ConfigObject)
                        {
                            valueToAdd = Merge(value1, value2);
                        }
                        else if (value1 is string)
                        {
                            valueToAdd = value1;
                        }
                        else if (value1 is IEnumerable)
                        {
                            valueToAdd = CollectionMerge(value1, value2);
                        }
                        else
                        {
                            valueToAdd = value1;
                        }
                    }
                }
                if (ReferenceEquals(null, valueToAdd))
                {
                    valueToAdd = new ConfigObject();
                }

                var isDefault = false;
                if (obj1 is ConfigObject configObjects)
                {
                    if ((configObjects)._members.TryGetValue(key, out var member) && member is ConfigObjectMember && ((ConfigObjectMember)member).IsDefault)
                    {
                        isDefault = true;
                    }
                }
                result.Set(key, valueToAdd, isDefault);
            }
            return result;
        }

        /// <summary>
        ///     Merges the multiple ConfigObjects, accepts infinite list of arguments
        ///     First named objects overrule preceeding objects.
        /// </summary>
        /// <returns>
        ///     The merged ConfigObject.
        /// </returns>
        /// <param name='objects'>
        ///     List of objects which are to be merged.
        /// </param>
        public static dynamic MergeMultiple(params object[] objects)
        {
            if (objects.Length == 1)
                return objects[0];

            if (objects.Length == 2)
                return Merge(objects[0], objects[1]);

            var head = objects.First();
            var tail = objects.Skip(1).Take(objects.Length - 1).ToArray();

            return Merge(head, MergeMultiple(tail));
        }

        public static dynamic CollectionMerge(dynamic obj1, dynamic obj2)
        {
            var x = new ArrayList();
            x.AddRange(obj1);
            x.AddRange(obj2);

            var obj1_type = obj1.GetType().GetElementType();
            if (obj1_type == typeof(ConfigObject))
                return x.ToArray(typeof(ConfigObject));
            return x.ToArray(obj1_type);
        }
    }

    /// <summary>
    ///     Get thrown if two types do not match and can't be merges
    /// </summary>
    public class TypeMissmatchException : Exception
    {
    }
}
