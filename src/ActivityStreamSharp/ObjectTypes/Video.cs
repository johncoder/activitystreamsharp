using System;
using ActivityStreamSharp.Converters;
using Newtonsoft.Json;

namespace ActivityStreamSharp.ObjectTypes
{
    public class Video : ForgivingExpandoObject
    {
        [JsonConverter(typeof(ObjectTypeConverter))]
        public dynamic Author { get; set; }
        public string DisplayName { get; set; }
        public string EmbedCode { get; set; }
        public string Id { get; set; }
        public DateTime Published { get; set; }
        public MediaLink Stream { get; set; }
        public string Summary { get; set; }
        public DateTime Updated { get; set; }

        [JsonProperty("ObjectType")]
        public static readonly string ObjectTypeKey = "video";
    }
}