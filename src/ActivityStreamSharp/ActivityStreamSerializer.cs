using System.Collections.Generic;
using System.Globalization;
using ActivityStreamSharp.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace ActivityStreamSharp
{
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
                    new ExpandoObjectConverter(),
                }
            };

        public string SerializeStream(ActivityStream stream)
        {
            var result = JsonConvert.SerializeObject(stream, Formatting.None, _settings);

            return result;
        }

        public string Serialize(Activity activity)
        {
            var result = JsonConvert.SerializeObject(activity, Formatting.None, _settings);

            return result;
        }

        public ActivityStream DeserializeStream(string input)
        {
            return JsonConvert.DeserializeObject<ActivityStream>(input, _settings);
        }

        public Activity Deserialize(string input)
        {
            return JsonConvert.DeserializeObject<Activity>(input, _settings);
        }
    }
}