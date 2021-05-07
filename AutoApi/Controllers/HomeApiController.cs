using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoApi.Controllers
{
    public class HomeApiController : Controller
    {
        private readonly IWebHostEnvironment hostEnvironment;

        public HomeApiController(IWebHostEnvironment _hostEnvironment)
        {
            this.hostEnvironment = _hostEnvironment;
        }

        [Route("")]
        [Route("home")]
        public ActionResult Index()
        {
            string htmlPath = Path.Combine(hostEnvironment.ContentRootPath, "www", "site.html");
            string html = System.IO.File.ReadAllText(htmlPath);
            byte[] contentArray = System.Text.Encoding.Default.GetBytes(html);
            return File(contentArray, "text/html");
        }

        [Authorize(Roles = "admin")]
        [Route("file/{path}")]
        public ActionResult GetFile([FromRoute] string path)
        {
            string contentType = null;
            string filePath = Path.Combine(hostEnvironment.WebRootPath, "StaticFiles", path);
            byte[] fileContent = System.IO.File.ReadAllBytes(filePath);
            FileInfo info = new FileInfo(filePath);
            switch (info.Extension)
            {
                case ".png":
                    contentType = "image/png";
                    break;
                default:
                    break;
            }
            return File(fileContent, contentType);
        }
    }
}
