using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ecdmin.Application.Common.Vos
{
    public class PageRequest 
    {
        [FromQuery(Name = "page_size")]
        public int PageSize { get; set; } = 10;
        
        [FromQuery(Name = "page")]
        public int Page { get; set; } = 1;
        
        public int Total { get; set; } 
    }
}
