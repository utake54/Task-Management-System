using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskManagement.Model.Model.ResponseModel
{
    public class ResponseModel
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public string? Error { get; set; }

        public void Ok()
        {
            Message = "Success";
        }
        public void Ok(object result)
        {
            Data = result;
            Message = "Success";
        }
        public void Failure()
        {
            Message = "Failed";
        }
        public void Failed(string error)
        {
            Error = error;
        }
    }
}
