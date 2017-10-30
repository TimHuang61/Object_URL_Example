using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Web_API.Controllers
{
    [RoutePrefix("Files")]
    public class FilesController : ApiController
    {
        [HttpPost]
        [Route("Download")]
        public HttpResponseMessage Download()
        {
            // 取檔案的邏輯
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files/test.txt");
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                var response = new HttpResponseMessage();
                response.Content = new ByteArrayContent(bytes);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = "test.txt";

                return response;
            }
        }
    }
}
