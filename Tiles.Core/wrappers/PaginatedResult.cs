// File: Tiles.Core/Wrappers/PaginatedResult.cs
using System;
using System.Collections.Generic;

namespace Tiles.Core.Wrappers
{
    public class PaginatedResult<T> : ServiceResult
    {
        public List<T> Data { get; set; } = new();
        public int PageNo { get; set; }
        public int RowsPerPage { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / RowsPerPage);

        public static PaginatedResult<T> CreateSuccess(List<T> data, int pageNo, int rowsPerPage, int totalRecords, string message = "Success")
        {
            return new PaginatedResult<T>
            {
                Data = data,
                PageNo = pageNo,
                RowsPerPage = rowsPerPage,
                TotalRecords = totalRecords,
                Success = true,
                Message = message
            };
        }

        public new static PaginatedResult<T> CreateFailure(string message = "Failure")
        {
            return new PaginatedResult<T>
            {
                Success = false,
                Message = message
            };
        }
    }
}
