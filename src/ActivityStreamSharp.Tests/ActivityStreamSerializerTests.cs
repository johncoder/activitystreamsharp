using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace ActivityStreamSharp.Tests
{
    [TestClass]
    public class ActivityStreamSerializerTests
    {
        [TestMethod]
        public void ActivityStreamSerializer_should_serialize_new_activitystream()
        {
            var serializer = new ActivityStreamSerializer();
            var result = serializer.SerializeStream(new ActivityStream());
            result.ShouldEqual("{\"totalItems\":0}");
        }

        [TestMethod]
        public void ActivityStreamSerializer_should_serialize_new_activity()
        {
            var serializer = new ActivityStreamSerializer();
            var result = serializer.Serialize(new Activity());
            result.ShouldEqual("{\"published\":\"0001-01-01T05:00:00Z\"}");
        }

        [TestMethod]
        public void ActivityStreamSerializer_should_deserialize_activity()
        {
            var activity = new Activity {Title = "Some Activity"};
            var serializer = new ActivityStreamSerializer();
            var result = serializer.Deserialize("{\"published\":\"0001-01-01T05:00:00Z\",\"title\":\"Some Activity\"}");
            result.Title.ShouldEqual(activity.Title);
        }

        [TestMethod]
        public void ActivityStreamSerializer_should_deserialize_activity_with_author()
        {
            var displayName = "johncoder";
            var serializer = new ActivityStreamSerializer();
            var result = serializer.Deserialize("{\"published\":\"0001-01-01T05:00:00Z\",\"actor\":{\"displayName\":\"johncoder\"}}");
            result.ShouldNotBeNull();
            ((string)result.Actor.displayName).ShouldEqual(displayName);
        }
    }
}
