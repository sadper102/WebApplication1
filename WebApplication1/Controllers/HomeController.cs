using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;
using WebApplication1.Models;

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
                        Savedata(item);
                        if (item.isLeaf == false) {
                            for(int i =0; i<item.subcategories.Count;i++)
                            {
                                //Console.WriteLine(item.subcategories[i].id);
                                Savedata(item.subcategories[i]);
                                if (item.subcategories[i].isLeaf == false)
                                {
                                    for (int j = 0; j < item.subcategories[i].subcategories.Count; j++)
                                    {
                                        //Console.WriteLine(item.subcategories[i].subcategories[j].id);
                                        Savedata(item.subcategories[i].subcategories[j]);
                                        if (item.subcategories[i].subcategories[j].isLeaf == false)
                                        {
                                            for (int k = 0; k < item.subcategories[i].subcategories[j].subcategories.Count; k++)
                                            {
                                                //Console.WriteLine(item.subcategories[i].subcategories[j].subcategories[k].id);
                                                Savedata(item.subcategories[i].subcategories[j].subcategories[k]);
                                                if (item.subcategories[i].subcategories[j].subcategories[k].isLeaf == false)
                                                {
                                                    for (int l = 0; l < item.subcategories[i].subcategories[j].subcategories[k].subcategories.Count; l ++)
                                                    {
                                                        //Console.WriteLine(item.subcategories[i].subcategories[j].subcategories[k].subcategories[l].id);
                                                        Savedata(item.subcategories[i].subcategories[j].subcategories[k].subcategories[l]);
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
        public  void Savedata(dynamic data) {
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
            _db.ctables.Add(tmp);
            _db.SaveChanges();
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