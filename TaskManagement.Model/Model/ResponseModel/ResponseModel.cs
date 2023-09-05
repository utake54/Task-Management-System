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
        public List<Error> Errors { get; set; }
        public bool Result { get; set; } = true;
        public void Ok()
        {
            Message = "Success";
        }
        public void Ok(object result)
        {
            Data = result;
            Message = "Success";
        }
        public void Ok(object result, string message)
        {
            Data = result;
            Message = message;
        }
        public void Failure(string message)
        {
            Message = message;
        }
    }

    public class Errors
    {
        public string Erros { get; set; }
    }
}
