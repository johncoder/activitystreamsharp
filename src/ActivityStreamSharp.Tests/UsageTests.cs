using System;
using ActivityStreamSharp.ObjectTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace ActivityStreamSharp.Tests
{
    [TestClass]
    public class UsageTests
    {
        [TestMethod]
        public void Activity_can_be_created()
        {
            dynamic blog = new ForgivingExpandoObject();
            blog.ObjectType = "blog";
            blog.Url = "http://somefarawayserver/blog/johncoder";

            dynamic person = new Person
                {
                    DisplayName = "johncoder",
                    Id = "users/1",
                    Image = new MediaLink {Height = 50, Width = 50, Url = "http://somefarawayserver/johncoder.png"},
                    Published = new DateTime(2012, 1, 1),
                    Updated = new DateTime(2012, 1, 1)
                };

            person.Tags = new[] {"Person", "Developer", "Employee"};

            var activity = new Activity
                {
                    Actor = person,
                    Id = "activity/johncoder/1",
                    Content = "something",
                    Verb = Verbs.Post,
                    Object = new Article
                        {
                            Content = "This is my post",
                            Id = "posts/johncoder/1",
                            Published = new DateTime(2012,1,1),
                            Updated = new DateTime(2012,1,1),
                            Url = "http://somefarawayserver/blogs/johncoder/new-post"
                        },
                    Target = blog,
                    Title = "John posted a new article to his blog.",
                    Url = "http://somefarawayserver/blogs/johncoder",
                    Provider = "http://somefarawayserver/blogs/johncoder/feed",
                    Published = new DateTime(2012,1,1)
                };

            var serializer = new ActivityStreamSerializer();
            var result = serializer.Serialize(activity);

            result.ShouldEqual("{\"published\":\"2012-01-01T05:00:00Z\",\"actor\":{\"objectType\":\"person\",\"displayName\":\"johncoder\",\"image\":{\"objectType\":\"medialink\",\"duration\":0,\"height\":50,\"url\":\"http://somefarawayserver/johncoder.png\",\"width\":50},\"id\":\"users/1\",\"published\":\"2012-01-01T05:00:00Z\",\"updated\":\"2012-01-01T05:00:00Z\",\"tags\":[\"Person\",\"Developer\",\"Employee\"]},\"content\":\"something\",\"id\":\"activity/johncoder/1\",\"object\":{\"objectType\":\"article\",\"content\":\"This is my post\",\"id\":\"posts/johncoder/1\",\"published\":\"2012-01-01T05:00:00Z\",\"updated\":\"2012-01-01T05:00:00Z\",\"url\":\"http://somefarawayserver/blogs/johncoder/new-post\"},\"provider\":\"http://somefarawayserver/blogs/johncoder/feed\",\"target\":{\"objectType\":\"blog\",\"url\":\"http://somefarawayserver/blog/johncoder\"},\"title\":\"John posted a new article to his blog.\",\"url\":\"http://somefarawayserver/blogs/johncoder\",\"verb\":\"post\"}");
        }

        [TestMethod]
        public void Activity_target_may_be_a_collection()
        {
            dynamic targets = new Collection()
                {
                    ObjectTypes = new[] { "person" },
                    Items = new dynamic[]
                        {
                            new Person { DisplayName = "Steve" },
                            new Person { DisplayName = "Todd" },
                            new Person { DisplayName = "Jason" }
                        },
                    TotalItems = 3
                };

            var activity = new Activity
            {
                Actor = new Person
                {
                    DisplayName = "johncoder",
                    Id = "users/1",
                    Image = new MediaLink { Height = 50, Width = 50, Url = "http://somefarawayserver/johncoder.png" },
                    Published = new DateTime(2012, 1, 1),
                    Updated = new DateTime(2012, 1, 1)
                },
                Id = "activity/johncoder/1",
                Content = "something",
                Verb = Verbs.Share,
                Object = new Article
                {
                    Content = "This is my post",
                    Id = "posts/johncoder/1",
                    Published = new DateTime(2012,1,1),
                    Updated = new DateTime(2012, 1, 1),
                    Url = "http://somefarawayserver/blogs/johncoder/new-post"
                },
                Target = targets,
                Title = "John shared an article with people.",
                Url = "http://somefarawayserver/blogs/johncoder",
                Provider = "http://somefarawayserver/blogs/johncoder/feed",
                Published = new DateTime(2012, 1, 1)
            };

            var serializer = new ActivityStreamSerializer();
            var result = serializer.Serialize(activity);

            result.ShouldEqual("{\"published\":\"2012-01-01T05:00:00Z\",\"actor\":{\"objectType\":\"person\",\"displayName\":\"johncoder\",\"image\":{\"objectType\":\"medialink\",\"duration\":0,\"height\":50,\"url\":\"http://somefarawayserver/johncoder.png\",\"width\":50},\"id\":\"users/1\",\"published\":\"2012-01-01T05:00:00Z\",\"updated\":\"2012-01-01T05:00:00Z\"},\"content\":\"something\",\"id\":\"activity/johncoder/1\",\"object\":{\"objectType\":\"article\",\"content\":\"This is my post\",\"id\":\"posts/johncoder/1\",\"published\":\"2012-01-01T05:00:00Z\",\"updated\":\"2012-01-01T05:00:00Z\",\"url\":\"http://somefarawayserver/blogs/johncoder/new-post\"},\"provider\":\"http://somefarawayserver/blogs/johncoder/feed\",\"target\":{\"objectType\":\"collection\",\"objectTypes\":[\"person\"],\"published\":\"0001-01-01T05:00:00Z\",\"updated\":\"0001-01-01T05:00:00Z\",\"items\":[{\"objectType\":\"person\",\"displayName\":\"Steve\",\"published\":\"0001-01-01T05:00:00Z\",\"updated\":\"0001-01-01T05:00:00Z\"},{\"objectType\":\"person\",\"displayName\":\"Todd\",\"published\":\"0001-01-01T05:00:00Z\",\"updated\":\"0001-01-01T05:00:00Z\"},{\"objectType\":\"person\",\"displayName\":\"Jason\",\"published\":\"0001-01-01T05:00:00Z\",\"updated\":\"0001-01-01T05:00:00Z\"}],\"totalItems\":3},\"title\":\"John shared an article with people.\",\"url\":\"http://somefarawayserver/blogs/johncoder\",\"verb\":\"share\"}");
        }

        [TestMethod]
        public void Activity_can_be_deserialized_with_a_complicated_structure()
        {
            var serializer = new ActivityStreamSerializer();
            var activity = serializer.Deserialize("{\"published\":\"2012-01-01T05:00:00Z\",\"actor\":{\"objectType\":\"person\",\"displayName\":\"johncoder\",\"image\":{\"objectType\":\"medialink\",\"duration\":0,\"height\":50,\"url\":\"http://somefarawayserver/johncoder.png\",\"width\":50},\"id\":\"users/1\",\"published\":\"2012-01-01T05:00:00Z\",\"updated\":\"2012-01-01T05:00:00Z\"},\"content\":\"something\",\"id\":\"activity/johncoder/1\",\"object\":{\"objectType\":\"article\",\"content\":\"This is my post\",\"id\":\"posts/johncoder/1\",\"published\":\"2012-01-01T05:00:00Z\",\"updated\":\"2012-01-01T05:00:00Z\",\"url\":\"http://somefarawayserver/blogs/johncoder/new-post\"},\"provider\":\"http://somefarawayserver/blogs/johncoder/feed\",\"target\":{\"objectType\":\"collection\",\"objectTypes\":[\"person\"],\"published\":\"0001-01-01T05:00:00Z\",\"updated\":\"0001-01-01T05:00:00Z\",\"items\":[{\"objectType\":\"person\",\"displayName\":\"Steve\",\"published\":\"0001-01-01T05:00:00Z\",\"updated\":\"0001-01-01T05:00:00Z\"},{\"objectType\":\"person\",\"displayName\":\"Todd\",\"published\":\"0001-01-01T05:00:00Z\",\"updated\":\"0001-01-01T05:00:00Z\"},{\"objectType\":\"person\",\"displayName\":\"Jason\",\"published\":\"0001-01-01T05:00:00Z\",\"updated\":\"0001-01-01T05:00:00Z\"}],\"totalItems\":3},\"title\":\"John shared an article with people.\",\"url\":\"http://somefarawayserver/blogs/johncoder\",\"verb\":\"share\"}");

            activity.Id.ShouldEqual("activity/johncoder/1");
        }

        [TestMethod]
        public void Activity_Actor_can_be_passed_to_overloaded_method()
        {
            var activity = new Activity {Actor = new Person {DisplayName = "johncoder"}};
            Process(activity.Actor);
        }

        private static void Process(Person person)
        {
            person.ShouldNotBeNull();
        }

        private static void Process(Group group)
        {
            group.ShouldNotBeNull();
        }
    }
}