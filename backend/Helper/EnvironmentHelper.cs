using System;

namespace stack.Helper
{
    public static class EnvironmentHelper
    {
        public static string GetValueAsString(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new Exception($"Environment variable was not found. Variable Key : {key}");

            var value = Environment.GetEnvironmentVariable(key);

            if (string.IsNullOrEmpty(value))
                throw new Exception($"Environment variable was not found. Key : {key}");

            return value;
        }

        public static T GetValueAsObject<T>(string key)
        {
            var valueAsJson = GetValueAsString(key);

            var valueAsObject = valueAsJson.ToDeserialize<T>();

            if (valueAsObject == null)
                throw new Exception($"Environment variable was not found. Key : {key}");

            return valueAsObject;
        }
    }
}
