using System;
using Newtonsoft.Json;

namespace ActivityStreamSharp.ObjectTypes
{
    public class MediaLink : ForgivingExpandoObject
    {
        public long Duration { get; set; }
        public long Height { get; set; }
        public string Url { get; set; }
        public long Width { get; set; }

        [JsonProperty("ObjectType")]
        public static readonly string ObjectTypeKey = Objects.MediaLink;
    }
}