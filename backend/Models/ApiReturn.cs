using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using stack.Helper;
using System.Collections.Generic;

namespace stack.Models
{
    public class ApiReturn<T>
    {
        public int Code { get; set; }

        public bool Success { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }

        public override string ToString() => this.ToSerializeAsCamelCase();
    }

    public class PagedApiReturn<T> : ApiReturn<List<T>>
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int PerPage { get; set; }
    }
}
