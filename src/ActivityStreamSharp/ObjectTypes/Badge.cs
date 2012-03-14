using System;
using ActivityStreamSharp.Converters;
using Newtonsoft.Json;

namespace ActivityStreamSharp.ObjectTypes
{
    /// <summary>
    /// The "badge" object type represents a Badge or Award granted to an object (typically a person).
    /// </summary>
    public class Badge : ForgivingExpandoObject
    {
        /// <summary>
        /// Activity Object, typically using the "person" object
        /// type, responsible for the creation of the badge. If
        /// the badge object appears within an activity, and the
        /// author of the badge is the actor for the activity,
        /// the author property MAY be omitted.
        /// </summary>
        [JsonConverter(typeof(ObjectTypeConverter))]
        public dynamic Author { get; set; }

        /// <summary>
        /// The natural-language, human-readable and plain-text
        /// title of the badge. HTML markup MOST NOT be included.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The unique identifier for the badge.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// An optional link to an image resource providing a
        /// visual representation of the badge. Processors MAY
        /// ignore images that are of an inappropriate size for
        /// their user interface.
        /// </summary>
        public MediaLink Image { get; set; }

        /// <summary>
        /// The optional time the badge was created in the form of a DateTime.
        /// </summary>
        public DateTime Published { get; set; }

        /// <summary>
        /// The optional time the badge was last updated in the form of a DateTime.
        /// </summary>
        public DateTime Updated { get; set; }

        /// <summary>
        /// Optional short, natural-language, human-readable description of the badge represented as a fragment of HTML.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// The permanent IRI of hte badge's HTML representation.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The ObjectType that identifies this object.
        /// </summary>
        [JsonProperty("ObjectType")]
        public static readonly string ObjectTypeKey = Objects.Badge;
    }
}