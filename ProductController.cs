using MahindraCrud.DAL;
using MahindraCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MahindraCrud.Controllers
{
    public class ProductController : Controller
    {
        ProductRepository repo = new ProductRepository();

        public ActionResult Index()
        {
            return View(repo.GetAllProducts());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (repo.IsMobileExists(product.Mobile))
            {
                ModelState.AddModelError("Mobile", "Mobile number already exists.");
                return View(product);
            }

            if (ModelState.IsValid)
            {
                repo.InsertProduct(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // Edit - GET
        public ActionResult Edit(int id)
        {
            var product = repo.GetProductById(id);
            return View(product);
        }

        // Edit - POST
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            var existingProduct = repo.GetProductById(product.Id);

            // Only check for duplicate if mobile number was changed
            if (existingProduct.Mobile != product.Mobile && repo.IsMobileExists(product.Mobile))
            {
                ModelState.AddModelError("Mobile", "Mobile number already exists.");
                return View(product);
            }

            if (ModelState.IsValid)
            {
                repo.UpdateProduct(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // Delete - GET (confirmation)
        public ActionResult Delete(int id)
        {
            var product = repo.GetProductById(id);
            return View(product);
        }

        // Delete - POST
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.DeleteProduct(id);
            return RedirectToAction("Index");
        }


    }
}
