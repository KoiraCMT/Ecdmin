using System.Collections.Generic;
using System.Net;
using Ecdmin.Application.Common.Vos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Ecdmin.Application
{
    public static class Response
    {
        public static JsonResult Success()
        {
            return Msg(HttpStatusCode.OK, null);
        }

        public static JsonResult Msg(HttpStatusCode code, string msg)
        {
            return new JsonResult(new
            {
                statusCode = code,
                errors = msg
            });
        }

        public static JsonResult BadRequest(string msg)
        {
            return Msg(HttpStatusCode.BadRequest, msg);
        }

        public static JsonResult Data(object data, string msg = null)
        {
            return new JsonResult(new
            {
                statusCode = HttpStatusCode.OK,
                errors = msg,
                data
            });
        }

        public static JsonResult Pagination<T>(IEnumerable<T> data, PageRequest pageRequest, string msg = null)
        {
            return new JsonResult(new
            {
                statusCode = HttpStatusCode.OK,
                errors = msg,
                data,
                meta = pageRequest
            }); 
        }
    }
}