using System;
using ActivityStreamSharp.Converters;
using Newtonsoft.Json;

namespace ActivityStreamSharp.ObjectTypes
{
    public class Bookmark : ForgivingExpandoObject
    {
        [JsonConverter(typeof(ObjectTypeConverter))]
        public dynamic Author { get; set; }
        public string DisplayName { get; set; }
        public string Id { get; set; }
        public MediaLink Image { get; set; }
        public DateTime Published { get; set; }
        public string TargetUrl { get; set; }
        public DateTime Updated { get; set; }

        [JsonProperty("ObjectType")]
        public static readonly string ObjectTypeKey = Objects.Bookmark;
    }
}