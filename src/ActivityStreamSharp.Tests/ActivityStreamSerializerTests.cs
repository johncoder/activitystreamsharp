using System.Dynamic;
using ActivityStreamSharp.ObjectTypes;
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
        public void ActivityStreamSerializer_should_serialize_activity_with_actor()
        {
            var activity = new Activity {Title = "Some Activity", Actor = new Person {DisplayName = "johncoder", Id = "SOMEUNIQUEID"}};
            var serializer = new ActivityStreamSerializer();
            var result = serializer.Serialize(activity);

            result.ShouldEqual("{\"published\":\"0001-01-01T05:00:00Z\",\"actor\":{\"objectType\":\"person\",\"displayName\":\"johncoder\",\"id\":\"SOMEUNIQUEID\",\"published\":\"0001-01-01T05:00:00Z\",\"updated\":\"0001-01-01T05:00:00Z\"},\"title\":\"Some Activity\"}");
        }

        [TestMethod]
        public void ActivityStreamSerializer_should_deserialize_activity_with_person_actor()
        {
            var displayName = "johncoder";
            var serializer = new ActivityStreamSerializer();
            var result = serializer.Deserialize("{\"published\":\"0001-01-01T05:00:00Z\",\"actor\":{\"objectType\":\"person\",\"displayName\":\"johncoder\"}}");

            object resultActor = result.Actor;
            resultActor.ShouldBeType<Person>();

            ((string)result.Actor.DisplayName).ShouldEqual(displayName);
        }

        [TestMethod]
        public void ActivityStreamSerializer_should_deserialize_activity_with_group_actor()
        {
            var displayName = "superusers";
            var serializer = new ActivityStreamSerializer();
            var result = serializer.Deserialize("{\"published\":\"0001-01-01T05:00:00Z\",\"actor\":{\"objectType\":\"group\",\"displayName\":\"superusers\"}}");

            object resultActor = result.Actor;
            resultActor.ShouldBeType<Group>();

            ((string)result.Actor.DisplayName).ShouldEqual(displayName);
        }

        [TestMethod]
        public void ActivityStreamSerializer_should_deserialize_activity_with_group_actor_and_extra_values()
        {
            var displayName = "superusers";
            var serializer = new ActivityStreamSerializer();
            var result = serializer.Deserialize("{\"published\":\"0001-01-01T05:00:00Z\",\"actor\":{\"objectType\":\"group\",\"displayName\":\"superusers\",\"somethingExtra\":\"extra\"}}");

            object resultActor = result.Actor;
            resultActor.ShouldBeType<Group>();

            ((string)result.Actor.DisplayName).ShouldEqual(displayName);
        }

        [TestMethod]
        public void ActivityStreamSerializer_should_deserialize_activity_with_unknown_actor()
        {
            var serializer = new ActivityStreamSerializer();
            var result = serializer.Deserialize("{\"published\":\"0001-01-01T05:00:00Z\",\"actor\":{\"objectType\":\"unknown\",\"displayName\":\"unknown\"}}");

            object resultActor = result.Actor;
            resultActor.ShouldBeType<ForgivingExpandoObject>();
            string displayName = result.Actor.displayName;
            displayName.ShouldEqual("unknown");
        }
    }
}
