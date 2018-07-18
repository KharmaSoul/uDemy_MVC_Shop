using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        C_ProductRepository cProductRepository;

        #region SUB - Constructor
        public ProductManagerController()
        {
            cProductRepository = new C_ProductRepository();
        }
        #endregion

        public ActionResult Index()
        {
            List<C_ModelProduct> ltProducts = cProductRepository.Collection().ToList();
            return View(ltProducts);
        }

        public ActionResult Create()
        {
            C_ModelProduct cProduct = new C_ModelProduct();
            return View(cProduct);
        }

        [HttpPost]
        public ActionResult Create(C_ModelProduct cProduct)
        {
            if (!ModelState.IsValid)
            {
                return View(cProduct);
            }
            else
            {
                cProductRepository.Insert(cProduct);
                cProductRepository.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string sID)
        {
            C_ModelProduct cProduct = cProductRepository.Find(sID);

            if (cProduct == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(cProduct);
            }
        }

        [HttpPost]
        public ActionResult Edit(C_ModelProduct cProduct, string sID)
        {
            C_ModelProduct cProductToEdit = cProductRepository.Find(sID);

            if (cProductToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(cProductToEdit);
                }

                cProductToEdit.Category = cProduct.Category;
                cProductToEdit.Description = cProduct.Description;
                cProductToEdit.Image = cProduct.Image;
                cProductToEdit.Name = cProduct.Name;
                cProductToEdit.Price = cProduct.Price;

                cProductRepository.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string sID)
        {
            C_ModelProduct cProductToDelete = cProductRepository.Find(sID);

            if (cProductToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(cProductToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string sID)
        {
            C_ModelProduct cProductToDelete = cProductRepository.Find(sID);

            if (cProductToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                cProductRepository.Delete(sID);
                cProductRepository.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}