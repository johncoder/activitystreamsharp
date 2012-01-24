using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;

namespace ActivityStreamSharp
{
    /// <summary>
    /// A dynamic object that gracefully handles missing members.
    /// </summary>
    public class ForgivingExpandoObject : IDynamicMetaObjectProvider, IDictionary<string, object>, IForgivingExpandoObject
    {
        /// <summary>
        /// 
        /// </summary>
        internal IDictionary<string, object> Dictionary { get; set; }

        /// <summary>
        /// The underlying ExpandoObject.
        /// </summary>
        dynamic IForgivingExpandoObject.Expando { get; set; }

        ICollection<string> IDictionary<string, object>.Keys { get { return Dictionary.Keys; } }
        ICollection<object> IDictionary<string, object>.Values { get { return Dictionary.Values; } }
        int ICollection<KeyValuePair<string, object>>.Count { get { return Dictionary.Count; } }
        bool ICollection<KeyValuePair<string, object>>.IsReadOnly { get { return Dictionary.IsReadOnly; } }

        public object this[string index]
        {
            get { return GetValue(index); }
            set { SetValue(index, value); }
        }

        public ForgivingExpandoObject()
            : this(new ExpandoObject())
        {
        }

        public ForgivingExpandoObject(ExpandoObject expandoObject)
        {
            ((IForgivingExpandoObject)this).Expando = expandoObject;
            Dictionary = expandoObject;
        }

        public void Add(string key, object value)
        {
            SetValue(key, value);
        }

        public void Add(KeyValuePair<string, object> item)
        {
            SetValue(item.Key, item.Value);
        }

        public void Clear()
        {
            Dictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            // TODO: Include fields and properties.
            return Dictionary.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            // TODO: Include fields and properties.
            return Dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            // NOTE: What does this mean to a dynamic object with its own defined properties?
            Dictionary.CopyTo(array, arrayIndex);
        }

        public IEnumerable<string> GetDynamicMemberNames()
        {
            // NOTE: Should this also include property and field names?
            return Dictionary.Keys;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            // NOTE: Should this also include properties and fields?
            return Dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Remove(string key)
        {
            return Dictionary.ContainsKey(key) && Dictionary.Remove(key);
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return Dictionary.ContainsKey(item.Key) && Dictionary.Remove(item);
        }

        public bool TrySetMember(SetMemberBinder binder, object value)
        {
            SetValue(binder.Name, value);
            return true;
        }

        public bool TryGetValue(string key, out object value)
        {
            value = GetValue(key);
            return true;
        }

        public bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = GetValue(binder.Name);
            return true;
        }

        public DynamicMetaObject GetMetaObject(Expression parameter)
        {
            return new ForgivingExpandoDynamicMetaObject(parameter, BindingRestrictions.GetInstanceRestriction(parameter, this), this);
        }

        internal object SetValue(string name, object value)
        {
            var member = GetType().GetProperties().Where(p => p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            if (member != null)
            {
                member.SetValue(this, value, null);
                return value;
            }

            var entry = Dictionary.Where(k => k.Key.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            if (entry.Any())
            {
                Dictionary[entry.FirstOrDefault().Key] = value;
                return value;
            }

            if (Dictionary.ContainsKey(name))
            {
                Dictionary[name] = value;
            }
            else
            {
                Dictionary.Add(name, value);
            }
            return value;
        }

        internal object GetValue(string name)
        {
            var existingProperty =
                GetType().GetProperties().FirstOrDefault(
                    p => p.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            if (existingProperty != null)
            {
                return existingProperty.GetValue(this, null);
            }

            object value;
            if (!Dictionary.TryGetValue(name, out value))
            {
                value = null;
            }
            return value;
        }
    }
}