using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Xml.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _db;
        private readonly UserManager<AppUsers> _user;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db,UserManager<AppUsers> User    )
        {
            _db = db;
            _user = User;
            _logger = logger;
        }

        public IActionResult Index()
        {

            StaticClass.Current_User = User.Identity.Name;

            //var data = (from items in _db.Items
            //                     join comments in _db.Commnents on items.id equals comments.Itemid
            //                     into joined  from j in joined.DefaultIfEmpty()

            //            select new Preview_Files{FileName= items.FileName,UploadedDate= items.UploadedDate,comments= j.commnents,UserName=j.UserName,id=items.id,FilePath=items.FilePath}).ToList();

            var data = _db.Items.Where (x=>x.UserName==StaticClass.Current_User).ToList();

            //var data = (from comments in _db.Commnents
            //            join items in _db.Items on comments.Itemid equals items.id
            //            into joined  from j in joined.DefaultIfEmpty()

            //            select new Preview_Files { FileName = j.FileName, UploadedDate = j.UploadedDate, comments = comments.commnents }).ToList();


            return View(data);
        }

        public IActionResult UploadFiles()
        {
         

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data1 = _db.Items.Where(x => x.id == id).FirstOrDefault();
            var data2 = _db.Commnents.Where(x => x.Itemid == id).ToList();
            _db.Remove(data1);
            _db.RemoveRange(data2);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult uploadFile(Items itms)
        {


            int c = UploadFile(Request.Form.Files[0]);



            string filename = Request.Form.Files[0].FileName;





            itms.FilePath = filename;
            itms.UploadedDate = DateTime.Now;
            itms.UserName = StaticClass.Current_User;
            _db.Add(itms);
            _db.SaveChanges();

            return View("UploadFiles");
        }


        public int UploadFile(IFormFile file)
        {
            try
            {
                var path = "wwwroot/uploads/" + file.FileName;
                var stream = new FileStream(path, FileMode.Create);
                file.CopyToAsync(stream);
                stream.Close();
                return 0;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}