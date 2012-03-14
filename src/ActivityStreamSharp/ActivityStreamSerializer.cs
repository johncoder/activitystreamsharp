using System.Collections.Generic;
using System.Globalization;
using ActivityStreamSharp.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace ActivityStreamSharp
{
    /// <summary>
    /// A serializer class that can convert entire activity streams as well
    /// as individual activity objects to JSON.
    /// </summary>
    public class ActivityStreamSerializer
    {
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Converters = new List<JsonConverter>
                {
                    new ObjectTypeConverter(),
                    new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AdjustToUniversal },
                    new StringEnumConverter(),
                    new ExpandoObjectConverter(),
                }
            };

        /// <summary>
        /// Serializes an ActivityStream object to JSON.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public string SerializeStream(ActivityStream stream)
        {
            var result = JsonConvert.SerializeObject(stream, Formatting.None, _settings);

            return result;
        }

        /// <summary>
        /// Serializes an Activity object to JSON.
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public string Serialize(Activity activity)
        {
            var result = JsonConvert.SerializeObject(activity, Formatting.None, _settings);

            return result;
        }

        /// <summary>
        /// Deserializes a JSON string into an ActivityStream object.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActivityStream DeserializeStream(string input)
        {
            return JsonConvert.DeserializeObject<ActivityStream>(input, _settings);
        }

        /// <summary>
        /// Deserializes a JSON string into an Activity object.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Activity Deserialize(string input)
        {
            return JsonConvert.DeserializeObject<Activity>(input, _settings);
        }
    }
}