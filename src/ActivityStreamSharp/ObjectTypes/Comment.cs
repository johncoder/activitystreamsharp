using System;
using ActivityStreamSharp.Converters;
using Newtonsoft.Json;

namespace ActivityStreamSharp.ObjectTypes
{
    public class Comment : ForgivingExpandoObject
    {
        [JsonConverter(typeof(ObjectTypeConverter))]
        public dynamic Author { get; set; }
        public string Content { get; set; }
        public string DisplayName { get; set; }
        public string Id { get; set; }
        public string[] InReplyTo { get; set; }
        public DateTime Published { get; set; }
        public DateTime Updated { get; set; }

        [JsonProperty("ObjectType")]
        public static readonly string ObjectTypeKey = Objects.Comment;
    }
}