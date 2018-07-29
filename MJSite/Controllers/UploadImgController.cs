using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace MJSite.Controllers
{
    public class UploadImgController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        private DbBase _DB;


        public UploadImgController(IHostingEnvironment IHostingEnvironment,DbBase DB)
        {
            _hostingEnvironment = IHostingEnvironment;
            _DB = DB;
        }
        [HttpPost]
        public async Task<IActionResult> Upload()
        {

            var files = Request.Form.Files;

            long size = files.Sum(f => f.Length);

            string webRootPath = _hostingEnvironment.WebRootPath;

            string contentRootPath = _hostingEnvironment.ContentRootPath;
            List<ImageModel> imageSrcList = new List<ImageModel>();

            foreach (var formFile in files)
            {

                if (formFile.Length > 0)

                {
                    string fileExt = GetFileExt(formFile.FileName); //文件扩展名，不含“.”

                    long fileSize = formFile.Length; //获得文件大小，以字节为单位

                    string newFileName = System.Guid.NewGuid().ToString() + "." + fileExt; //随机生成新的文件名

                    var filePath = webRootPath + "/UploadImgs/" + newFileName;

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);

                    }
                    imageSrcList.Add(new ImageModel {
                        CreateTime = DateTime.Now,
                        ImageID = Guid.NewGuid(),
                        ImgURL= "/UploadImgs/"+newFileName,
                    });

                }

            }
            if (imageSrcList.Count!=0)
            {
                await _DB.AddRangeAsync(imageSrcList);
                await _DB.SaveChangesAsync();
            }


            return Ok(new { count = files.Count, size });
        }
    

        private string GetFileExt(string fileName)
        {
            int index = fileName.IndexOf('.');
            return fileName.Substring(index + 1);
        }

    }
}