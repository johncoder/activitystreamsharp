using System.Security.Policy;

namespace ActivityStreamSharp
{
    /// <summary>
    /// Represents a stream of activity.
    /// </summary>
    public class ActivityStream
    {
        /// <summary>
        /// The total number of items present in this collection.
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// A subset of items in the collection.
        /// </summary>
        public Activity[] Items { get; set; }

        /// <summary>
        /// An IRI containing a full reference of objects in the collection.
        /// </summary>
        public Url Url { get; set; }
    }
}
