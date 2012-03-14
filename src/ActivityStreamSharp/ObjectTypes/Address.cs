using Newtonsoft.Json;

namespace ActivityStreamSharp.ObjectTypes
{
    /// <summary>
    /// Represents an Address object.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// The fully formatted representation of this address.
        /// </summary>
        public string Formatted { get; set; }

        /// <summary>
        /// The Street field.
        /// </summary>
        public string StreetAddress { get; set; }

        /// <summary>
        /// Most local area.
        /// </summary>
        public string Locality { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The ObjectType that identifies this object.
        /// </summary>
        [JsonProperty("ObjectType")]
        public static readonly string ObjectTypeKey = Objects.Address;
    }
}