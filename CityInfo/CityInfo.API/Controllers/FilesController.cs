﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider 
                ?? throw new System.ArgumentNullException(nameof(fileExtensionContentTypeProvider));

        }

        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId)
        {
            //Look up the actual fileyste
            var filePath = "nysc.pdf";

            //check that the file exists
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            if(!_fileExtensionContentTypeProvider.TryGetContentType(filePath, out var contentType)) 
            {
                contentType = "app;ication/octet-stream";
            }


            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));
            
        }
    }
}
