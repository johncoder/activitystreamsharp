using System;
using Newtonsoft.Json;

namespace ActivityStreamSharp.ObjectTypes
{
    public class Group : ForgivingExpandoObject
    {
        public Person Author { get; set; }
        public string DisplayName { get; set; }
        public string Id { get; set; }
        public DateTime Published { get; set; }
        public DateTime Updated { get; set; }
        public string Url { get; set; }
        [JsonProperty("ObjectType")]
        public static readonly string ObjectTypeKey = Objects.Group;
    }
}