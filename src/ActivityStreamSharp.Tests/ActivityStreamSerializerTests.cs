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
            //esultActor.ShouldBeType<Person>();

            ((string)result.Actor["displayName"]).ShouldEqual(displayName);
        }

        [TestMethod]
        public void ActivityStreamSerializer_should_deserialize_activity_with_group_actor()
        {
            var displayName = "superusers";
            var serializer = new ActivityStreamSerializer();
            var result = serializer.Deserialize("{\"published\":\"0001-01-01T05:00:00Z\",\"actor\":{\"objectType\":\"group\",\"displayName\":\"superusers\"}}");

            object resultActor = result.Actor;
            //resultActor.ShouldBeType<Group>();

            ((string)result.Actor["displayName"]).ShouldEqual(displayName);
        }

        [TestMethod]
        public void ActivityStreamSerializer_should_deserialize_activity_with_group_actor_and_extra_values()
        {
            var displayName = "superusers";
            var serializer = new ActivityStreamSerializer();
            var result = serializer.Deserialize("{\"published\":\"0001-01-01T05:00:00Z\",\"actor\":{\"objectType\":\"group\",\"displayName\":\"superusers\",\"somethingExtra\":\"extra\"}}");

            object resultActor = result.Actor;
            //resultActor.ShouldBeType<Group>();

            ((string)result.Actor["displayName"]).ShouldEqual(displayName);
        }

        [TestMethod]
        public void ActivityStreamSerializer_should_deserialize_activity_with_unknown_actor()
        {
            var serializer = new ActivityStreamSerializer();
            var result = serializer.Deserialize("{\"published\":\"0001-01-01T05:00:00Z\",\"actor\":{\"objectType\":\"unknown\",\"displayName\":\"unknown\"}}");

            object resultActor = result.Actor;
            //resultActor.ShouldBeType<ForgivingExpandoObject>();
            string displayName = result.Actor["displayName"];
            displayName.ShouldEqual("unknown");
        }

        [TestMethod]
        public void ActivityStreamSerializer_should_serialize_activitystream_object()
        {
            var serializer = new ActivityStreamSerializer();
            var activityStream = new ActivityStream {Items = new[] {new Activity(), new Activity()}, TotalItems = 2};
            var result = serializer.SerializeStream(activityStream);

            result.ShouldEqual("{\"totalItems\":2,\"items\":[{\"published\":\"0001-01-01T05:00:00Z\"},{\"published\":\"0001-01-01T05:00:00Z\"}]}");
        }

        [TestMethod]
        public void ActivityStreamSerializer_should_deserialize_activitystream_object()
        {
            var serializer = new ActivityStreamSerializer();
            var result = serializer.DeserializeStream("{\"totalItems\":2,\"items\":[{\"published\":\"0001-01-01T05:00:00Z\"},{\"published\":\"0001-01-01T05:00:00Z\"}]}");

            result.TotalItems.ShouldEqual(2);
            result.Items.Length.ShouldEqual(2);
        }

        [TestMethod]
        public void ActivityStreamSerializer_target_with_null_dynamic_property_serializes()
        {
            var activity = new Activity();
            activity.Target = new ForgivingExpandoObject {{"Sum", (string) null}, {"Id", 4}};
            var serializer = new ActivityStreamSerializer();
            var result = serializer.Serialize(activity);
            result.ShouldNotBeNull();
        }

        [Ignore]
        [TestMethod]
        public void ActivityStreamSerializer_deserialize_change_value_reserialize_works()
        {
            var serializer = new ActivityStreamSerializer();
            var result = serializer.Deserialize("{\"published\":\"2012-03-09T19:23:57.345Z\",\"actor\":{\"objectType\":\"person\",\"displayName\":\"John Nelson\",\"id\":\"314\",\"published\":\"0001-01-01T05:00:00Z\",\"updated\":\"0001-01-01T05:00:00Z\"},\"object\":{\"objectType\":\"comment\",\"content\":\"Sorry I keep adding all these comments, I can't help it!\",\"id\":\"690\",\"published\":\"2012-03-09T19:23:57.345Z\",\"updated\":\"2012-03-09T19:23:57.345Z\"},\"target\":{\"objectType\":\"video\",\"displayName\":\"PITT v MIAMI\",\"id\":\"304\",\"published\":\"0001-01-01T05:00:00Z\",\"summary\":\"1Mb\",\"updated\":\"0001-01-01T05:00:00Z\"},\"title\":\"John Nelson commented on PITT v MIAMI\",\"verb\":\"post\"}");
            result.Object.content = "This is something new!";

            var reserialized = serializer.Serialize(result);
            reserialized.ShouldEqual("{\"published\":\"2012-03-09T19:23:57.345Z\",\"actor\":{\"objectType\":\"person\",\"displayName\":\"John Nelson\",\"id\":\"314\",\"published\":\"0001-01-01T05:00:00Z\",\"updated\":\"0001-01-01T05:00:00Z\"},\"object\":{\"objectType\":\"comment\",\"content\":\"This is something new!\",\"id\":\"690\",\"published\":\"2012-03-09T19:23:57.345Z\",\"updated\":\"2012-03-09T19:23:57.345Z\"},\"target\":{\"objectType\":\"video\",\"displayName\":\"PITT v MIAMI\",\"id\":\"304\",\"published\":\"0001-01-01T05:00:00Z\",\"summary\":\"1Mb\",\"updated\":\"0001-01-01T05:00:00Z\"},\"title\":\"John Nelson commented on PITT v MIAMI\",\"verb\":\"post\"}");
        }
    }
}
