using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;
using WebApplication1.Models;
using System.Data.Entity.Validation;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public  ActionResult uploadfile(HttpPostedFileBase jsonfile) {
            try
            {
                using (StreamReader r = new StreamReader(jsonfile.InputStream))
                {
                    string json = r.ReadToEnd();
                    dynamic array = JsonConvert.DeserializeObject(json);
                    foreach (var item in array)
                    {
                        Savedata(item,item.id);
                        if (item.isLeaf == false) {
                            for(int i =0; i<item.subcategories.Count;i++)
                            {
                                //Console.WriteLine(item.subcategories[i].id);
                                Savedata(item.subcategories[i],item.id);
                                if (item.subcategories[i].isLeaf == false)
                                {
                                    for (int j = 0; j < item.subcategories[i].subcategories.Count; j++)
                                    {
                                        //Console.WriteLine(item.subcategories[i].subcategories[j].id);
                                        Savedata(item.subcategories[i].subcategories[j],item.subcategories[i].id);
                                        if (item.subcategories[i].subcategories[j].isLeaf == false)
                                        {
                                            for (int k = 0; k < item.subcategories[i].subcategories[j].subcategories.Count; k++)
                                            {
                                                //Console.WriteLine(item.subcategories[i].subcategories[j].subcategories[k].id);
                                                Savedata(item.subcategories[i].subcategories[j].subcategories[k], item.subcategories[i].subcategories[j].id);
                                                if (item.subcategories[i].subcategories[j].subcategories[k].isLeaf == false)
                                                {
                                                    for (int l = 0; l < item.subcategories[i].subcategories[j].subcategories[k].subcategories.Count; l ++)
                                                    {
                                                        //Console.WriteLine(item.subcategories[i].subcategories[j].subcategories[k].subcategories[l].id);
                                                        Savedata(item.subcategories[i].subcategories[j].subcategories[k].subcategories[l], item.subcategories[i].subcategories[j].subcategories[k].id);
                                                        if (item.subcategories[i].subcategories[j].subcategories[k].subcategories[l].isLeaf == false) {
                                                            Console.WriteLine("CHECK");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception err)
            {
                Console.WriteLine(err);
            }
            return RedirectToAction("/Category");
        }
        public  void Savedata(dynamic data,dynamic id1) {
            try
            {
                categoriesEntities _db = new categoriesEntities();
                ctable tmp = new ctable();
                tmp.id = data.id;
                tmp.isLeaf = data.isLeaf;
                tmp.name = data.name;
                tmp.label = data.label;
                tmp.firstLevelCatId = data.firstLevelCatId;
                tmp.lscSetId = data.lscSetId;
                tmp.variationCat = data.variationCat;
                tmp.active = data.active;
                tmp.Parentcatid = id1;
                ctable check = _db.ctables.Where(x => x.id == tmp.id).FirstOrDefault();
                if (check == null)
                {
                    _db.ctables.Add(tmp);
                    _db.SaveChanges();
                }
                else
                {
                    _db.Entry(check).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        public ActionResult Category() {
            categoriesEntities db = new categoriesEntities();
            List<ctable> lable = db.ctables.Where(x => x.id == x.firstLevelCatId).ToList();

            return View(lable);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}