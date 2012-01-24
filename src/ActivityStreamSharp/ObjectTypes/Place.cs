using Newtonsoft.Json;

namespace ActivityStreamSharp.ObjectTypes
{
    public class Place : ForgivingExpandoObject
    {
        public string DisplayName { get; set; }
        public string Id { get; set; }
        public string Position { get; set; }
        public Address Address { get; set; }
        public string Url { get; set; }

        [JsonProperty("ObjectType")]
        public readonly string ObjectTypeKey = "place";
    }
}