using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        InMemoryRepository<ModelProduct> cRepositoryProducts;
        InMemoryRepository<ModelProductCategory> cRepositoryCategories;

        #region SUB - Constructor
        public ProductManagerController()
        {
            cRepositoryProducts = new InMemoryRepository<ModelProduct>();
            cRepositoryCategories = new InMemoryRepository<ModelProductCategory>();
        }
        #endregion

        #region SUB - Index
        public ActionResult Index()
        {
            List<ModelProduct> ltProducts = cRepositoryProducts.Collection().ToList();
            return View(ltProducts);
        }
        #endregion
        #region SUB - Create
        public ActionResult Create()
        {
            ViewModelProductManager viewModel = new ViewModelProductManager();

            viewModel.Product = new ModelProduct();
            viewModel.ProductCategories = cRepositoryCategories.Collection();
            return View(viewModel);
        }
        #endregion
        #region SUB - Create (Post)
        [HttpPost]
        public ActionResult Create(ModelProduct product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                cRepositoryProducts.Insert(product);
                cRepositoryProducts.Commit();

                return RedirectToAction("Index");
            }
        }
        #endregion
        #region SUB - Edit
        public ActionResult Edit(string sID)
        {
            ModelProduct cItem = cRepositoryProducts.Find(sID);

            if (cItem == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewModelProductManager viewModel = new ViewModelProductManager();
                viewModel.Product = cItem;
                viewModel.ProductCategories = cRepositoryCategories.Collection();

                return View(viewModel);
            }
        }
        #endregion
        #region SUB - Edit (Post)
        [HttpPost]
        public ActionResult Edit(ModelProduct product, string sID)
        {
            ModelProduct cItemToEdit = cRepositoryProducts.Find(sID);

            if (cItemToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(cItemToEdit);
                }

                cItemToEdit.Category = product.Category;
                cItemToEdit.Description = product.Description;
                cItemToEdit.Image = product.Image;
                cItemToEdit.Name = product.Name;
                cItemToEdit.Price = product.Price;

                cRepositoryProducts.Commit();

                return RedirectToAction("Index");
            }
        }
        #endregion
        #region SUB - Delete
        public ActionResult Delete(string sID)
        {
            ModelProduct cItemToDelete = cRepositoryProducts.Find(sID);

            if (cItemToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(cItemToDelete);
            }
        }
        #endregion
        #region SUB - Delete (Post)
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string sID)
        {
            ModelProduct cItemToDelete = cRepositoryProducts.Find(sID);

            if (cItemToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                cRepositoryProducts.Delete(sID);
                cRepositoryProducts.Commit();
                return RedirectToAction("Index");
            }
        }
        #endregion
    }
}