using System;
using Newtonsoft.Json;

namespace ActivityStreamSharp.ObjectTypes
{
    public class Person : ForgivingExpandoObject
    {
        public string DisplayName { get; set; }
        public MediaLink Image { get; set; }
        public string Id { get; set; }
        public DateTime Published { get; set; }
        public DateTime Updated { get; set; }

        [JsonProperty("ObjectType")]
        public static readonly string ObjectTypeKey = "person";
    }
}