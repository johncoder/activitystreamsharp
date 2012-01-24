using System;
using System.Linq;
using ActivityStreamSharp.ObjectTypes;
using ActivityStreamSharp.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace ActivityStreamSharp.Converters
{
    public class ObjectTypeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var forgivingExpando = value as ForgivingExpandoObject;

            if (forgivingExpando == null) return;

            var members = value.GetType().GetFields()
                .Where(f => !typeof(ForgivingExpandoObject).GetFields().Any(fp => fp.Name.Equals(f.Name, StringComparison.InvariantCultureIgnoreCase)));

            var properties = value.GetType().GetProperties()
                .Where(p => !typeof(ForgivingExpandoObject).GetProperties().Any(fp => fp.Name.Equals(p.Name, StringComparison.InvariantCultureIgnoreCase)));

            writer.WriteStartObject();
            foreach (var member in members)
            {
                var memberValue = member.GetValue(value);

                if (memberValue == null)
                    continue;

                var newNameAttribute = member.GetCustomAttributes(true).OfType<JsonPropertyAttribute>().FirstOrDefault();

                if (newNameAttribute != null)
                {
                    writer.WritePropertyName(newNameAttribute.PropertyName.ToCamelCase());
                }
                else
                {
                    writer.WritePropertyName(member.Name.ToCamelCase());
                }
                serializer.Serialize(writer, memberValue);
            }
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(value, null);

                if (propertyValue == null)
                    continue;

                writer.WritePropertyName(property.Name.ToCamelCase());
                serializer.Serialize(writer, propertyValue);
            }
            foreach (var item in forgivingExpando)
            {
                writer.WritePropertyName(item.Key);
                serializer.Serialize(writer, item.Value);
            }
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.ValueType == typeof(string))
                return reader.Value;

            var jObject = JObject.Load(reader);

            var currentObjectType = (string)jObject.Property("objectType");

            object objectToPopulate;

            // TODO: Automatically register these subclasses.
            switch (currentObjectType)
            {
                case "person":
                    objectToPopulate = new Person();
                    break;
                case "group":
                    objectToPopulate = new Group();
                    break;
                default:
                    objectToPopulate = new ForgivingExpandoObject();
                    break;
            }

            serializer.Populate(jObject.CreateReader(), objectToPopulate);

            return objectToPopulate;
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(object)) return true;

            if (objectType == typeof(ForgivingExpandoObject)) return true;

            var canConvert = objectType.GetMembers().Any(p => p.Name == "ObjectType");

            return canConvert;
        }
    }

}