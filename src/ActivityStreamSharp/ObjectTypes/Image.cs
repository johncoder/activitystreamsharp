using System;
using ActivityStreamSharp.Converters;
using Newtonsoft.Json;

namespace ActivityStreamSharp.ObjectTypes
{
    public class Image : ForgivingExpandoObject
    {
        [JsonConverter(typeof(ObjectTypeConverter))]
        public dynamic Author { get; set; }
        public string DisplayName { get; set; }
        [JsonProperty("Image")]
        public MediaLink ImageLink { get; set; }
        public MediaLink FullImage { get; set; }
        public string Id { get; set; }
        public DateTime Published { get; set; }
        public string Summary { get; set; }
        public DateTime Updated { get; set; }
        public string Url { get; set; }

        [JsonProperty("ObjectType")]
        public static readonly string ObjectTypeKey = Objects.Image;
    }
}