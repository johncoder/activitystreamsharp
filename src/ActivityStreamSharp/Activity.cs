using System;
using ActivityStreamSharp.Converters;
using Newtonsoft.Json;

namespace ActivityStreamSharp
{
    /// <summary>
    /// Represents an activity in a stream.
    /// </summary>
    /// <remarks>
    /// To add extension properties, use Activity as a base class.
    /// </remarks>
    public class Activity
    {
        /// <summary>
        /// DateTime that this activity was published.
        /// </summary>
        public DateTime Published { get; set; }

        /// <summary>
        /// Describes the entity that performed the activity.
        /// </summary>
        /// <remarks>
        /// Use a class from ObjectTypes.
        /// </remarks>
        [JsonConverter(typeof(ObjectTypeConverter))]
        public dynamic Actor { get; set; }

        /// <summary>
        /// Natural-language description of the activity encoded as a single JSON String containing HTML markup.
        /// </summary>
        /// <remarks>
        /// A complex type assigned to this property will be JSON serialized.
        /// </remarks>
        public dynamic Content { get; set; }

        /// <summary>
        /// A permanent, unique identifier for the activity in the form of an IRI (or URI).
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Describes the primary object of the activity.
        /// </summary>
        /// <remarks>
        /// Use a class from ObjectTypes.
        /// </remarks>
        public dynamic Object { get; set; }

        /// <summary>
        /// The application that published the activity.
        /// </summary>
        /// <remarks>
        /// This is automatically filled in.
        /// </remarks>
        public string Provider { get; set; }

        /// <summary>
        /// Describes the target of the activity.
        /// </summary>
        /// <remarks>
        /// Use a class from ObjectTypes.
        /// </remarks>
        public dynamic Target { get; set; }

        /// <summary>
        /// Natural-language title or headline for the activity.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A resource providing an HTML representation of the activity.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Identifies the action that the activity describes.
        /// </summary>
        /// <value>Default is "post"</value>
        public string Verb { get; set; }
    }
}