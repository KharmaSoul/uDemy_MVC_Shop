﻿using System;
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
        c_productRepository cRepository;

        #region SUB - Constructor
        public ProductManagerController()
        {
            cRepository = new c_productRepository();
        }
        #endregion

        public ActionResult Index()
        {
            List<c_modelProduct> ltProducts = cRepository.Collection().ToList();
            return View(ltProducts);
        }

        public ActionResult Create()
        {
            c_modelProduct cItem = new c_modelProduct();
            return View(cItem);
        }

        [HttpPost]
        public ActionResult Create(c_modelProduct cItem)
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
            c_modelProduct cItem = cRepository.Find(sID);

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
        public ActionResult Edit(c_modelProduct cItem, string sID)
        {
            c_modelProduct cItemToEdit = cRepository.Find(sID);

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
                cItemToEdit.Description = cItem.Description;
                cItemToEdit.Image = cItem.Image;
                cItemToEdit.Name = cItem.Name;
                cItemToEdit.Price = cItem.Price;

                cRepository.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string sID)
        {
            c_modelProduct cItemToDelete = cRepository.Find(sID);

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
            c_modelProduct cItemToDelete = cRepository.Find(sID);

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