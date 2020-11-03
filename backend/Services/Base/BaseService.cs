using stack.Data;
using stack.Models;

namespace stack.Services
{
    public abstract class BaseService
    {
        protected readonly StackDbContext Db;

        public BaseService(StackDbContext db)
        {
            Db = db;
        }

        protected ServiceReturn<T> Success<T>(T data, string message = "")
        {
            return new ServiceReturn<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        protected ServiceReturn<T> Success<T>(string message)
        {
            return new ServiceReturn<T>
            {
                Success = true,
                Message = message
            };
        }

        protected ServiceReturn<T> Failed<T>(T data, string message)
        {
            return new ServiceReturn<T>
            {
                Success = false,
                Message = message,
                Data = data
            };
        }

        protected ServiceReturn<T> Failed<T>(string message)
        {
            return new ServiceReturn<T>
            {
                Success = false,
                Message = message
            };
        }
    }
}
