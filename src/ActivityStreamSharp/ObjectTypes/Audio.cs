using System;
using ActivityStreamSharp.Converters;
using Newtonsoft.Json;

namespace ActivityStreamSharp.ObjectTypes
{
    public class Audio : ForgivingExpandoObject
    {
        [JsonConverter(typeof(ObjectTypeConverter))]
        public dynamic Author { get; set; }
        public string DisplayName { get; set; }
        public string EmbedCode { get; set; }
        public string Id { get; set; }
        public DateTime Published { get; set; }
        public DateTime Updated { get; set; }
        public MediaLink Stream { get; set; }
        public string Summary { get; set; }

        [JsonProperty("ObjectType")]
        public static readonly string ObjectTypeKey = "audio";
    }
}