using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace ActivityStreamSharp.Tests
{
    [TestClass]
    public class ForgivingExpandoObjectTests
    {
        [TestMethod]
        public void ForgivingExpando_can_be_initialized()
        {
            var forgivingExpando = new ForgivingExpandoObject();
        }

        [TestMethod]
        public void ForgivingExpando_can_set_extra_properties()
        {
            dynamic forgivingExpando = new ForgivingExpandoObject();
            forgivingExpando.SomeUndefinedProperty = "unknown";

            string value = forgivingExpando.SomeUndefinedProperty;
            value.ShouldEqual("unknown");
        }

        [TestMethod]
        public void ForgivingExpando_subclass_with_property_can_be_set()
        {
            dynamic forgivingExpando = new Gizmo();
            forgivingExpando.Name = "Amazing Gizmo";

            string name = forgivingExpando.Name;
            name.ShouldEqual("Amazing Gizmo");
        }

        public class Gizmo : ForgivingExpandoObject
        {
            public string Name { get; set; }
        }
    }
}