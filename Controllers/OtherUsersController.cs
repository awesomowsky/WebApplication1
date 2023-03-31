using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]

    public class OtherUsersController : Controller
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;
        private readonly ApplicationDbContext _db;
        public OtherUsersController(ApplicationDbContext db, Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment)
        {
            Environment = _environment;
            _db = db;
        }
      

        public FileResult ViewFile(string fn)
        {

            string file = System.IO.Path.Combine(this.Environment.WebRootPath+"/uploads/"+fn);
            FileStream NFs = new FileStream(file, FileMode.Open);
            string ext = fn.Substring( fn.Length - 3,3);
            if (ext == "pdf")
            {
                return File(NFs, "application/pdf"); 
            }
            else
            {
                return File(NFs, "text/plain"); 
            }
        }
        public FileResult Download(string fn)
        {
            try
            {
                string contentPath = this.Environment.ContentRootPath;
                string wwwPath = this.Environment.WebRootPath + "\\uploads\\" + fn;
                byte[] fileBytes = System.IO.File.ReadAllBytes(wwwPath);
                string fileName = fn;
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }catch(Exception ex)
            {
                return null;
            }
        }
        public IActionResult Index()
        {
           var data= _db.Items.Where(x=>x.IsAvailableforOthers==true).ToList();

            return View(data);
        }


     
        public IActionResult ViewComments(int id,int typ)
        {

            var data=_db.Commnents.Where(x=>x.Itemid==id).OrderBy(x=>x.CreatedTime).ToList();
            if (data.Count > 0)
            {
                var data1 = _db.Items.Where(x => x.id == id).FirstOrDefault();
                data[0].typ= typ;
                data[0].Titlle = "File Name: " + data1.FileName + "   (" + data1.UploadedDate + ")";
            }
            return View(data);
        }

 
        public IActionResult AddComments(int id)
        {
            Commnents cmts = new Commnents();
            cmts.Itemid = id;
            cmts.UserName = StaticClass.Current_User;
            cmts.CreatedTime = DateTime.Now;
            return View(cmts);
        }


        [HttpPost]
        public IActionResult AddNewComments(Commnents cmts)
        {
            _db.Add(cmts);
            _db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
