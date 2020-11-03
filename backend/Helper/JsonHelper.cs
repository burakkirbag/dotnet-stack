using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace stack.Helper
{
    public static class JsonHelper
    {
        public static string ToSerializeAsCamelCase(this object obj)
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var settings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            };

            var dataAsJson = JsonConvert.SerializeObject(obj, settings);

            return dataAsJson;
        }

        public static string ToSerialize(this object obj) => JsonConvert.SerializeObject(obj);

        public static T ToDeserialize<T>(this string json) => JsonConvert.DeserializeObject<T>(json);
    }
}
