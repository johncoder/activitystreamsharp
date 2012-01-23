using System.Security.Policy;

namespace ActivityStreamSharp
{
    public class ActivityStream : IActivityStream
    {
        public int TotalItems { get; set; }

        public Activity[] Items { get; set; }
        public Url Url { get; set; }
    }
}
