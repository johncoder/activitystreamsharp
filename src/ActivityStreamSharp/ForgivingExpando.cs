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
    public class ForgivingExpandoObject : IDynamicMetaObjectProvider, IDictionary<string, object>
    {
        public IDictionary<string, object> Dictionary { get; set; }

        /// <summary>
        /// The underlying ExpandoObject.
        /// </summary>
        public dynamic Expando { get; set; }
        public ICollection<string> Keys { get { return Dictionary.Keys; } }
        public ICollection<object> Values { get { return Dictionary.Values; } }
        public int Count { get { return Dictionary.Count; } }
        public bool IsReadOnly { get { return Dictionary.IsReadOnly; } }

        public object this[string index]
        {
            get
            {
                if (Dictionary.ContainsKey(index))
                    return Dictionary[index];

                var entry = Dictionary.Where(k => k.Key.Equals(index, StringComparison.InvariantCultureIgnoreCase));
                
                if(entry.Any())
                    return entry.FirstOrDefault().Value;

                var member = GetType().GetProperties().Where(p => p.Name.Equals(index, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

                if (member != null)
                {
                    return member.GetValue(this, null);
                }

                return null;
            }
            set
            {
                var member = GetType().GetProperties().Where(p => p.Name.Equals(index, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

                if (member != null)
                {
                    member.SetValue(this, value, null);
                    return;
                }

                var entry = Dictionary.Where(k => k.Key.Equals(index, StringComparison.InvariantCultureIgnoreCase));

                if (entry.Any())
                {
                    Dictionary[entry.FirstOrDefault().Key] = value;
                    return;
                }

                if (Dictionary.ContainsKey(index))
                {
                    Dictionary[index] = value;
                }
                else
                {
                    Dictionary.Add(index, value);
                }
            }
        }

        public ForgivingExpandoObject()
            : this(new ExpandoObject())
        {
        }

        public ForgivingExpandoObject(ExpandoObject expandoObject)
        {
            Expando = expandoObject;
            Dictionary = Expando as IDictionary<string, object>;
        }

        public void Add(string key, object value)
        {
            Dictionary.Add(key, value);
        }

        public void Add(KeyValuePair<string, object> item)
        {
            Dictionary.Add(item);
        }

        public void Clear()
        {
            Dictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return Dictionary.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return Dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            Dictionary.CopyTo(array, arrayIndex);
        }

        public IEnumerable<string> GetDynamicMemberNames()
        {
            return Dictionary.Keys;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return Dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Remove(string key)
        {
            return Dictionary.Remove(key);
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return Dictionary.Remove(item);
        }

        public bool TrySetMember(SetMemberBinder binder, object value)
        {
            var existingProperty =
                GetType().GetProperties().FirstOrDefault(
                    p => p.Name.Equals(binder.Name, StringComparison.InvariantCultureIgnoreCase));

            if (existingProperty != null)
            {
                existingProperty.SetValue(this, value, null);
            }
            else
            {
                Dictionary[binder.Name] = value;
            }

            return true;
        }

        public bool TryGetValue(string key, out object value)
        {
            return Dictionary.TryGetValue(key, out value);
        }

        public bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var existingProperty =
                GetType().GetProperties().FirstOrDefault(
                    p => p.Name.Equals(binder.Name, StringComparison.InvariantCultureIgnoreCase));

            if (existingProperty != null)
            {
                result = existingProperty.GetValue(this, null);
                return true;
            }

            if (Dictionary.ContainsKey(binder.Name))
            {
                result = Dictionary[binder.Name];
                return true;
            }

            if (binder.ReturnType.IsValueType)
            {
                result = Activator.CreateInstance(binder.ReturnType);
            }
            else
            {
                result = null;
            }

            return true;
        }

        public DynamicMetaObject GetMetaObject(Expression parameter)
        {
            return new ForgivingExpandoDynamicMetaObject(parameter, BindingRestrictions.GetInstanceRestriction(parameter, this), this);
        }

        internal object SetValue(string name, object value)
        {
            Dictionary.Add(name, value);
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