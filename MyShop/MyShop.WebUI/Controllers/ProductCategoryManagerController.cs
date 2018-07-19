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
        InMemoryRepository<ModelProductCategory> cRepository;

        #region SUB - Constructor
        public ProductCategoryManagerController()
        {
            cRepository = new InMemoryRepository<ModelProductCategory>();
        }
        #endregion

        public ActionResult Index()
        {
            List<ModelProductCategory> ltProducts = cRepository.Collection().ToList();
            return View(ltProducts);
        }

        public ActionResult Create()
        {
            ModelProductCategory cItem = new ModelProductCategory();
            return View(cItem);
        }

        [HttpPost]
        public ActionResult Create(ModelProductCategory cItem)
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
            ModelProductCategory cItem = cRepository.Find(sID);

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
        public ActionResult Edit(ModelProductCategory cItem, string sID)
        {
            ModelProductCategory cItemToEdit = cRepository.Find(sID);

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
            ModelProductCategory cItemToDelete = cRepository.Find(sID);

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
            ModelProductCategory cItemToDelete = cRepository.Find(sID);

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