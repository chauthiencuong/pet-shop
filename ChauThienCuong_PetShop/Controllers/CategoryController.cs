using ChauThienCuong_PetShop.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChauThienCuong_PetShop.Controllers
{
    public class CategoryController : Controller
    {
        PetShopEntities obj = new PetShopEntities();
        // GET: Category
        public ActionResult Index(string searchString)
        {
            var listCategory = obj.Category.ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                string searchLower = searchString.ToLower();
                listCategory = listCategory.Where(c =>
                    c.name.ToLower().Contains(searchLower)).ToList();
            }


            return View(listCategory);
        }




        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            var objCategory = obj.Category.Where(n => n.id == id).FirstOrDefault();
            return View(objCategory);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    obj.Category.Add(category);
                    obj.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Đã xảy ra lỗi khi thêm mới Category: " + ex.Message);
            }

            return View(category);
        }


        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            var objCategory = obj.Category.Find(id);
            return View(objCategory);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            try
            {
                var categoryToUpdate = obj.Category.Find(category.id);

                categoryToUpdate.name = category.name;
                obj.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            var objCategory = obj.Category.Find(id);
            return View(objCategory);
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var categoryToDelete = obj.Category.Find(id);

                obj.Category.Remove(categoryToDelete);

                obj.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //Lấy sản phẩm theo danh mục
        public ActionResult ProductCategory(int id)
        {
            var listProduct = obj.Product.Where(n => n.category_id == id).ToList();
            return View(listProduct);
        }
    }
}
