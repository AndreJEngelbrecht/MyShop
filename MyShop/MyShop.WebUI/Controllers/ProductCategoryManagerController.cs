using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        InMemoryRepository<ProductCategory> contextCategory;

        public ProductCategoryManagerController()
        {
            contextCategory = new InMemoryRepository<ProductCategory>();
        }

        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = contextCategory.Collection().ToList();

            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                contextCategory.Insert(productCategory);
                contextCategory.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string Id)
        {
            ProductCategory productCategory = contextCategory.Find(Id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, String Id)
        {
            ProductCategory ProductCategoryToEdit = contextCategory.Find(Id);
            if (ProductCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }
                ProductCategoryToEdit.Category = productCategory.Category;
                ProductCategoryToEdit.Id = productCategory.Id;

                contextCategory.Commit();

                return RedirectToAction("Index");
            }

        }
        public ActionResult Delete(string Id)
        {
            ProductCategory ProductCategoryToDelete = contextCategory.Find(Id);
            if (ProductCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ProductCategoryToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory ProductCategoryToDelete = contextCategory.Find(Id);
            if (ProductCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                contextCategory.Delete(Id);
                contextCategory.Commit();

                return RedirectToAction("Index");
            }
        }
    }
}