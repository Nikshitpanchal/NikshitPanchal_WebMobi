using NikshitPanchal_WebMobi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NikshitPanchal_WebMobi.Controllers
{
    public class HomeController : Controller
    {
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

        public JsonResult GetallProduct()
        {
            try
            {
                using (var ctx = new InventryDBEntities())
                {
                    int recordsTotal = 0;
                    var courseList = ctx.Database.SqlQuery<GetAllProducts_Result>("exec GetAllProducts").ToList<GetAllProducts_Result>();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    recordsTotal = courseList.Count();
                    return Json(new { draw = courseList, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = courseList });
                }
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.DenyGet);
            }
        }
        public ActionResult Add()
        {
            Tbl_products tbl = new Tbl_products();
            tbl.brand_id = 0;
            tbl.category_id = 0;
            return View(tbl);
        }
        [HttpPost]
        public ActionResult Add(Tbl_products Product, HttpPostedFileBase filesrefdoc)
        {
            //update student in DB using EntityFramework in real-life application
            try
            {
                using (InventryDBEntities db = new InventryDBEntities())
                {
                    //update list by removing old student and adding updated student for demo purpose
                    db.Tbl_products.Add(Product);
                    db.SaveChanges();

                    return RedirectToAction("index");
                }
            }
            catch (Exception e)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAllCategories(int cid)
        {
            using (InventryDBEntities db = new InventryDBEntities())
            {
                IEnumerable<GetAllCategories_Result> obj = db.Database.SqlQuery<GetAllCategories_Result>("exec GetAllCategories").ToList<GetAllCategories_Result>().ToList();
                List<SelectListItem> result = new List<SelectListItem>();
                result.Add(new SelectListItem { Text = "--Select--", Value = 0.ToString() });
                result.AddRange(obj.Select(d => new SelectListItem { Text = d.category_name, Value = d.category_id.ToString() }).ToList());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAllBrands(int bid)
        {
            using (InventryDBEntities db = new InventryDBEntities())
            {
                IEnumerable<GetAllBrands_Result> obj = db.Database.SqlQuery<GetAllBrands_Result>("exec GetAllBrands").ToList<GetAllBrands_Result>().ToList();
                List<SelectListItem> result = new List<SelectListItem>();
                result.Add(new SelectListItem { Text = "--Select--", Value = 0.ToString() });
                result.AddRange(obj.Select(d => new SelectListItem { Text = d.brand_name, Value = d.brand_id.ToString() }).ToList());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Edit(int Id)
        {
            using (InventryDBEntities db = new InventryDBEntities())
            {
                var std = db.Tbl_products.Where(s => s.product_id == Id).FirstOrDefault();
                //PSession.bid = std.brand_id;
                //PSession.cid = std.category_id;
                return View(std);

            }
        }
        [HttpPost]
        public ActionResult Edit(Tbl_products std, HttpPostedFileBase filesrefdoc)
        {
            //update student in DB using EntityFramework in real-life application
            try
            {
                using (InventryDBEntities db = new InventryDBEntities())
                {
                    ////update list by removing old student and adding updated student for demo purpose
                    var student = db.Tbl_products.Where(s => s.product_id == std.product_id).FirstOrDefault();
                    db.Tbl_products.Remove(student);
                    db.Tbl_products.Add(std);
                    db.SaveChanges();
                    return RedirectToAction("home/index");
                }
            }
            catch (Exception e)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DeleteProduct(int pid)
        {
            try
            {
                using (InventryDBEntities db = new InventryDBEntities())
                {
                    Tbl_products std = db.Tbl_products.Where(x => x.product_id == pid).FirstOrDefault();
                    db.Tbl_products.Remove(std);
                    db.SaveChanges();
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(false, JsonRequestBehavior.DenyGet);
            }

        }

    }

}