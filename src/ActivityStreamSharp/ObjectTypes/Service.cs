using System;
using Newtonsoft.Json;

namespace ActivityStreamSharp.ObjectTypes
{
    public class Service : ForgivingExpandoObject
    {
        public string DisplayName { get; set; }
        public string Id { get; set; }
        public MediaLink Image { get; set; }
        public DateTime Published { get; set; }
        public string Summary { get; set; }
        public DateTime Updated { get; set; }
        public string Url { get; set; }

        [JsonProperty("ObjectType")]
        public static readonly string ObjectTypeKey = Objects.Service;
    }
}