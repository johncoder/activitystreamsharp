using System;
using ActivityStreamSharp.Converters;
using Newtonsoft.Json;

namespace ActivityStreamSharp.ObjectTypes
{
    public class Event : ForgivingExpandoObject
    {
        public Collection Attending { get; set; }

        [JsonConverter(typeof(ObjectTypeConverter))]
        public dynamic Author { get; set; }
        public string DisplayName { get; set; }
        public DateTime EndTime { get; set; }
        public string Id { get; set; }
        public Place Location { get; set; }
        public Collection MaybeAttending { get; set; }
        public Collection NotAttending { get; set; }
        public DateTime Published { get; set; }
        public DateTime StartTime { get; set; }
        public string Summary { get; set; }
        public DateTime Updated { get; set; }
        public string Url { get; set; }

        [JsonProperty("ObjectType")]
        public static readonly string ObjectTypeKey = Objects.Event;
    }
}