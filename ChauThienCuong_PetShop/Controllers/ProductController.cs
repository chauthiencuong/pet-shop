using ChauThienCuong_PetShop.Context;
using ChauThienCuong_PetShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChauThienCuong_PetShop.Controllers
{
    public class ProductController : Controller
    {
        PetShopEntities obj = new PetShopEntities();
        // GET: Product
        public ActionResult Index(string searchString)
        {

            var listProduct = obj.Product.ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                // Chuyển chuỗi tìm kiếm về chữ thường
                string searchLower = searchString.ToLower();
                // Lọc danh sách danh mục theo chuỗi tìm kiếm chữ thường
                listProduct = listProduct.Where(c =>
                    c.name.ToLower().Contains(searchLower)).ToList();
            }
            return View(listProduct);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var objProduct = obj.Product.Where(n => n.id == id).FirstOrDefault();
            return View(objProduct);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.Category = new SelectList(obj.Category, "id", "name");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product pro, HttpPostedFileBase uploadhinh)
        {
            obj.Product.Add(pro);

            obj.SaveChanges();

            if (uploadhinh != null && uploadhinh.ContentLength > 0)
            {
                int id = int.Parse(obj.Product.ToList().Last().id.ToString());

                string _FileName = Path.GetFileName(uploadhinh.FileName);
                int index = uploadhinh.FileName.IndexOf('.');
                string _path = Path.Combine(Server.MapPath("~/Context/images/productImage"), _FileName);
                uploadhinh.SaveAs(_path);

                Product unv = obj.Product.FirstOrDefault(x => x.id == id);
                unv.image = _FileName;
                obj.SaveChanges();
            }


            return RedirectToAction("Index");
        }
        // GET: Product/Edit/5
        // GET: Product/Edit/5
        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            // Lấy sản phẩm từ cơ sở dữ liệu bằng id
            Product pro = obj.Product.FirstOrDefault(x => x.id == id);

            // Lấy danh sách các danh mục sản phẩm từ cơ sở dữ liệu và gán cho ViewBag.Categories
            ViewBag.Category = new SelectList(obj.Category, "id", "name");

            // Gán giá trị cũ của category_id cho ViewBag để sử dụng trong view
            ViewBag.CurrentCategory = pro.category_id;

            return View(pro);
        }


        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product pro, HttpPostedFileBase uploadhinh)
        {
            // Lấy sản phẩm từ cơ sở dữ liệu dựa trên ID
            Product unv = obj.Product.FirstOrDefault(x => x.id == pro.id);
            if (unv == null)
            {
                return HttpNotFound(); // Hoặc xử lý lỗi khác tùy vào yêu cầu của ứng dụng
            }

            // Cập nhật thông tin sản phẩm
            unv.name = pro.name;
            unv.price = pro.price;
            unv.descreption = pro.descreption;
            unv.category_id = pro.category_id; // Cập nhật category_id

            // Xử lý tập tin ảnh
            if (uploadhinh != null && uploadhinh.ContentLength > 0)
            {
                // Xử lý tập tin ảnh
                string _FileName = Path.GetFileName(uploadhinh.FileName);
                string _path = Path.Combine(Server.MapPath("~/Context/images/productImage"), _FileName);
                uploadhinh.SaveAs(_path);
                unv.image = _FileName;
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            obj.SaveChanges();

            return RedirectToAction("Index");
        }


        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            var objProduct = obj.Product.Find(id);
            return View(objProduct);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var productToDelete = obj.Product.Find(id);

                obj.Product.Remove(productToDelete);

                obj.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult GetAllProducts()
        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListCategory = obj.Category.ToList();
            objHomeModel.ListProduct = obj.Product.ToList();
            return View(objHomeModel);
        }

    }
}
