using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ecdmin.Application;
using Furion;
using Furion.DataEncryption;
using Furion.DataValidation;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppSettingsOptions = Ecdmin.Web.Core.Options.AppSettingsOptions;

namespace Ecdmin.Web.Entry.Controllers
{
    public class UploadController : IDynamicApiController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UploadController(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [AppAuthorize]
        [RequestSizeLimit(1 * 1024 * 1024)]
        public async Task<IActionResult> Avatar([Required] IFormFile image)
        {
            var isImage = image.FileName.TryValidate(ValidationTypes.Image);
            if (!isImage.IsValid)
            {
                return Response.BadRequest(isImage.ValidationResults.First().ErrorMessage);
            }

            var md5FileName = MD5Encryption.Encrypt(image.FileName);
            var uploadFilePath = UploadFilePath($"{md5FileName}.{image.ContentType.Split("/").Last()}");

            using (var stream = new FileStream(uploadFilePath.AbsoluteFilePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            
            return Response.Data($"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/{uploadFilePath.RelativeFilePath}");
        }

        private FilePath UploadFilePath(string filename)
        {
            var appSettingsOptions = App.GetOptions<AppSettingsOptions>();
            var path = appSettingsOptions.UploadPath;
            path = Path.Combine(path, DateTime.Now.ToString("yyyyMMdd"));
            var contentRootPath = _webHostEnvironment.ContentRootPath;
            var uploadDir = Path.Combine(contentRootPath, path);
            
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }
            
            var filePath = new FilePath
            {
                RelativeFilePath = Path.Combine(path, filename),
                UploadDir = uploadDir
            };
        
            filePath.AbsoluteFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, filePath.RelativeFilePath);
            
            return filePath;
        }
        
        private class FilePath
        {
            public string AbsoluteFilePath { get; set; }
            public string RelativeFilePath { get; set; }
            
            public string UploadDir { get; set; }
        }
    }
}