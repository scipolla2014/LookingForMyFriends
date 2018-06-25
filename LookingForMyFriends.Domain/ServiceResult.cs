using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LookingForMyFriends.Domain
{
    public class ServiceResult<T> where T : class
    {
        protected ServiceResult(T @object)
        {
            Succeeded = true;
            Object = @object;
        }

        protected ServiceResult(ServiceResultFailReason reason, IEnumerable<string> errors)
        {
            Succeeded = false;
            Reason = reason;
            Errors = errors;
        }

        public bool Succeeded { get; }

        public T Object { get; }

        public ServiceResultFailReason Reason { get; }

        public IEnumerable<string> Errors { get; }

        public string SerializedErrors => JsonConvert.SerializeObject(Errors.ToArray());

        public static ServiceResult<T> Success(T result)
        {
            return new ServiceResult<T>(result);
        }

        public static ServiceResult<T> Fail(ServiceResultFailReason reason, string error)
        {
            return new ServiceResult<T>(reason, new[] { error });
        }

        public static ServiceResult<T> Fail(ServiceResultFailReason reason, IEnumerable<string> errors)
        {
            return new ServiceResult<T>(reason, errors);
        }
    }
}