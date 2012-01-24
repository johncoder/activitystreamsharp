using System;
using System.Dynamic;
using System.Linq;
using ActivityStreamSharp.ObjectTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ActivityStreamSharp.Converters
{
    public class ObjectTypeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var forgivingExpando = value as ForgivingExpandoObject;

            if (forgivingExpando == null) return;

            writer.WriteStartObject();
            foreach(var property in value.GetType().GetProperties())
            {
                writer.WritePropertyName(property.Name);
                serializer.Serialize(writer, property.GetValue(value, null));
            }
            foreach(var item in forgivingExpando)
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

            var canConvert = objectType.GetMembers().Any(p => p.Name == "ObjectType");

            return canConvert;
        }
    }

}