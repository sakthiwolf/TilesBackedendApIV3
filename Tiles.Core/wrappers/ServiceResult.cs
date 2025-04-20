// File: Tiles.Core/Wrappers/ServiceResult.cs
using System;

namespace Tiles.Core.Wrappers
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public static ServiceResult CreateSuccess(string message = "Success")
        {
            return new ServiceResult { Success = true, Message = message };
        }

        public static ServiceResult CreateFailure(string message = "Failure")
        {
            return new ServiceResult { Success = false, Message = message };
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }

        public static ServiceResult<T> CreateSuccess(T data, string message = "Success")
        {
            return new ServiceResult<T> { Success = true, Data = data, Message = message };
        }

        public new static ServiceResult<T> CreateFailure(string message = "Failure")
        {
            return new ServiceResult<T> { Success = false, Message = message };
        }
    }
}
