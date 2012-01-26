using System;
using ActivityStreamSharp.Converters;
using Newtonsoft.Json;

namespace ActivityStreamSharp.ObjectTypes
{
    public class Question : ForgivingExpandoObject
    {
        [JsonConverter(typeof(ObjectTypeConverter))]
        public dynamic Author { get; set; }
        public string Content { get; set; }
        public string DisplayName { get; set; }
        public string Id { get; set; }
        public DateTime Published { get; set; }
        public float Rating { get; set; }
        public DateTime Updated { get; set; }
        public string Url { get; set; }

        [JsonProperty("ObjectType")]
        public static readonly string ObjectTypeKey = "question";
    }
}