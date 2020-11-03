using System.Collections.Generic;

namespace stack.Models.View
{
    public class ErrorVM
    {
        public string RequestId { get; set; }
        public string Message { get; set; }
        public string DetailMessage { get; set; }
        public List<string> Errors { get; set; }
            = new List<string>();
    }
}
