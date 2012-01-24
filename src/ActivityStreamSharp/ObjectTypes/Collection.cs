using System;
using ActivityStreamSharp.Converters;
using Newtonsoft.Json;

namespace ActivityStreamSharp.ObjectTypes
{
    public class Collection : ForgivingExpandoObject
    {
        [JsonConverter(typeof(ObjectTypeConverter))]
        public dynamic Author { get; set; }
        public string DisplayName { get; set; }
        public string Id { get; set; }
        public MediaLink Image { get; set; }
        public string[] ObjectTypes { get; set; }
        public DateTime Published { get; set; }
        public DateTime Updated { get; set; }
        public string Url { get; set; }
        public dynamic[] Items { get; set; } // NOTE: Look this up in the spec.
        public int TotalItems { get; set; }

        [JsonProperty("ObjectType")]
        public readonly string ObjectTypeKey = "bookmark";
    }
}