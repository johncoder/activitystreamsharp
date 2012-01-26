using Newtonsoft.Json;

namespace ActivityStreamSharp.ObjectTypes
{
    public class Address
    {
        public string Formatted { get; set; }
        public string StreetAddress { get; set; }
        public string Locality { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        [JsonProperty("ObjectType")]
        public static readonly string ObjectTypeKey = "address";
    }
}