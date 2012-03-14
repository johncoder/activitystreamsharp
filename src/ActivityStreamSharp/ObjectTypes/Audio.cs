using System;
using ActivityStreamSharp.Converters;
using Newtonsoft.Json;

namespace ActivityStreamSharp.ObjectTypes
{
    /// <summary>
    /// The "audio" type represents audio content.
    /// </summary>
    public class Audio : ForgivingExpandoObject
    {
        /// <summary>
        /// Activity Object, typically using the "person" object
        /// type, responsible for the creation of the audio stream. If
        /// the audio object appears within an activity, and the
        /// author of the audio object is the actor for the activity,
        /// the author property MAY be omitted.
        /// </summary>
        [JsonConverter(typeof(ObjectTypeConverter))]
        public dynamic Author { get; set; }

        /// <summary>
        /// The natural-language, human-readable and plain-text title
        /// of the audio stream. HTML markup MOST NOT be included.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// An optional HTML fragment that, when embedded in an HTML
        /// page, will provide an interactive player UI for the audio stream.
        /// </summary>
        public string EmbedCode { get; set; }

        /// <summary>
        /// The unique identifier for the audio stream.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The optional time the audio stream was created in the form of a DateTime.
        /// </summary>
        public DateTime Published { get; set; }

        /// <summary>
        /// The optional time the audio stream was last updated in the form of a DateTime.
        /// </summary>
        public DateTime Updated { get; set; }

        /// <summary>
        /// A link to the audio content itself. Represented in JSON as a
        /// media link as defined in JSON Activity Streams 1.0.
        /// </summary>
        public MediaLink Stream { get; set; }

        /// <summary>
        /// Optional short, natural-language, human-readable snippet or
        /// summary of the audio stream represented as a fragment of HTML.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// The ObjectType that identifies this object.
        /// </summary>
        [JsonProperty("ObjectType")]
        public static readonly string ObjectTypeKey = Objects.Audio;
    }
}