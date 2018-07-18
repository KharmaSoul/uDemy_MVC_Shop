using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        c_productCategoryRepository cRepository;

        #region SUB - Constructor
        public ProductCategoryManagerController()
        {
            cRepository = new c_productCategoryRepository();
        }
        #endregion

        public ActionResult Index()
        {
            List<c_modelProductCategory> ltProducts = cRepository.Collection().ToList();
            return View(ltProducts);
        }

        public ActionResult Create()
        {
            c_modelProductCategory cItem = new c_modelProductCategory();
            return View(cItem);
        }

        [HttpPost]
        public ActionResult Create(c_modelProductCategory cItem)
        {
            if (!ModelState.IsValid)
            {
                return View(cItem);
            }
            else
            {
                cRepository.Insert(cItem);
                cRepository.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string sID)
        {
            c_modelProductCategory cItem = cRepository.Find(sID);

            if (cItem == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(cItem);
            }
        }

        [HttpPost]
        public ActionResult Edit(c_modelProductCategory cItem, string sID)
        {
            c_modelProductCategory cItemToEdit = cRepository.Find(sID);

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

                cItemToEdit.Category = cItem.Category;
                cRepository.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string sID)
        {
            c_modelProductCategory cItemToDelete = cRepository.Find(sID);

            if (cItemToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(cItemToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string sID)
        {
            c_modelProductCategory cItemToDelete = cRepository.Find(sID);

            if (cItemToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                cRepository.Delete(sID);
                cRepository.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}