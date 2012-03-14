using System;
using ActivityStreamSharp.Converters;
using Newtonsoft.Json;

namespace ActivityStreamSharp.ObjectTypes
{
    /// <summary>
    /// The "article" object type indicates that the object
    /// is an article, such as a news article, a knowledge base
    /// entry, or other similar construct. Such objects generally
    /// consist of paragraphs of text, in some cases incorporating
    /// embedded media such as photos and inline hyperlinks to
    /// other resources.
    /// </summary>
    public class Article : ForgivingExpandoObject
    {
        /// <summary>
        /// Activity Object, typically using the "person" object
        /// type, responsible for the creation of the article. If
        /// the article object appears within an activity, and the
        /// author of the article is the actor for the activity,
        /// the author property MAY be omitted.
        /// </summary>
        [JsonConverter(typeof(ObjectTypeConverter))]
        public dynamic Author { get; set; }

        /// <summary>
        /// The optional main body content represented as a fragment of HTML.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The natural-language, human-readable and plain-text title of the article. HTML markup MOST NOT be included.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The unique identifier for the article.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The optional time the article was created in the form of a DateTime.
        /// </summary>
        public DateTime Published { get; set; }

        /// <summary>
        /// The optional time the article was last updated in the form of a DateTime.
        /// </summary>
        public DateTime Updated { get; set; }

        /// <summary>
        /// The permanent IRI of hte article's HTML representation.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The ObjectType that identifies this object.
        /// </summary>
        [JsonProperty("ObjectType")]
        public static readonly string ObjectTypeKey = Objects.Article;
    }
}