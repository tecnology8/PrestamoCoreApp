using Microsoft.AspNetCore.Mvc;
using PrestamoCoreApp.Models;
using PrestamoCoreApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoCoreApp.Controllers
{
    public class PrestamoController : Controller
    {
        public readonly PrestamoServices _prestamos;

        public PrestamoController(PrestamoServices prestamos)
        {
            _prestamos = prestamos;
        }
        public IActionResult Index()
        {
            var lista = new List<PrestamoModel>();
            
            lista = _prestamos.GetPrestamos().ToList();

            return View(lista);
        }


        #region CRUD

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind] PrestamoModel model)
        {
            if (ModelState.IsValid)
            {
                _prestamos.AddPrestamo(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            var prestamo = _prestamos.GetById(id);
            if (prestamo == null)
                return RedirectToAction("Index");
            return View(prestamo);
        }

        [HttpPost]
        public ActionResult Edit(int id, [Bind] PrestamoModel model)
        {
            if (id != model.Id)
                return RedirectToAction("Index");
            if (ModelState.IsValid)
            {
                _prestamos.UpdatePrestamo(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var prestamo = _prestamos.GetById(id);

            if (prestamo == null)
            {
                return RedirectToAction("Index");
            }
            return View(prestamo);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            var prestamo = _prestamos.GetById(id);
            if (prestamo == null)
                return RedirectToAction("Index");

            return View(prestamo);
        }


        [HttpPost]
        public ActionResult DeleteConfirmed(int? id)
        {
            _prestamos.DeletePrestamo(id);
            return RedirectToAction("Index");
        }

        #endregion
    }
}
