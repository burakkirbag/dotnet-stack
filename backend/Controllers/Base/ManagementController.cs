using System.Collections.Generic;

namespace stack
{
    public class ManagementController : DbController
    {
        public const string MessageTypeError = "Error";
        public const string MessageTypeWarning = "Warning";
        public const string MessageTypeSuccess = "Success";

        protected void SetErrorMessage(string message, List<string> errors = null)
        {
            SetMessage(MessageTypeError, message, errors);
        }

        protected void SetWarningMessage(string message, List<string> errors = null)
        {
            SetMessage(MessageTypeWarning, message, errors);
        }

        protected void SetSuccessMessage(string message, List<string> errors = null)
        {
            SetMessage(MessageTypeSuccess, message, errors);
        }

        private void SetMessage(string type, string message, List<string> errors = null)
        {
            TempData["Type"] = type;
            TempData["Message"] = message;
            TempData["Errors"] = errors;
        }
    }
}
