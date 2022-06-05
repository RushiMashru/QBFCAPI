using System;
using System.Collections.Generic;
using System.Text;

namespace QBFC.Models.ViewModel
{
    public class Response<T>
    {
        public Response()
        {
        }

        public Response(T Data)
        {
            this.Data = Data;
            this.Success = true;
            this.Errors = null;
            this.Message = string.Empty;
        }

        public T Data { get; set; }
        public string[] Errors { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
