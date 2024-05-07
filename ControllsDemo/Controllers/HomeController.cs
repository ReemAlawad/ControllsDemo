using ControllsDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace ControllsDemo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new MyViewModel
            {
                 DepartmentItems = GetDepartmentItems()
               // CatItems = GetCatItems()
            };
            ViewBag.CatItems = GetCatItems();
            return View(model);
        }
        [HttpPost]
        public IActionResult SubmitForm(MyViewModel model)
        {
            model.DepartmentItems = GetDepartmentItems();
            if(ModelState.IsValid)
            {
                var selectedDepartmentId = model.DepartmentItems.FirstOrDefault(x => x.Value == model.SelectedDepartmentId.ToString());
                model.SelectedDepartmentText = selectedDepartmentId?.Text ?? "Selecteer een afdeling";
            }
            if(model.Cat.IsActive)
            {
                ViewBag.IsCatActive = "Check box selected";
            }
            else
            {
                ViewBag.IsCatActive = "Check box not selected";
            }
            return View("Index",model);
        }
        private List<SelectListItem> GetDepartmentItems()
        {
            var itemsFromDb = GetItems().ToList();
            // convert to SelectedListItem
            var selectedListItems = itemsFromDb.Select(x => new SelectListItem 
            {
              Value = x.Value,
              Text = x.Text,
            }).ToList();
            return selectedListItems;
        }
        // Ophalen data van database
        private List<SelectListItem> GetItems()
        {
            return new List<SelectListItem>
            {
               new SelectListItem{Value = "1", Text = "Optie 1"},
               new SelectListItem{Value = "2", Text = "Optie 2"},
               new SelectListItem{Value = "3", Text = "Optie 3"}
            };
        }


        [HttpPost]
        public IActionResult SubmitFormCat(MyViewModel model)
        {

            model.CatItems = GetCatItems();
            if (ModelState.IsValid)
            {
                var selectedCatId = model.CatItems.FirstOrDefault(x => x.Value == model.SelectedCatId.ToString());
                model.SelectedCatText = selectedCatId?.Text ?? "Selecteer een afdeling";
            }

            return View("Index", model);
        }
        private List<SelectListItem> GetCatItems()
        {
            var itemsFromDb = GetItemsCat().ToList();
            // convert to SelectedListItem
            var selectedListItems = itemsFromDb.Select(x => new SelectListItem
            {
                Value = x.Value,
                Text = x.Text,
            }).ToList();
            return selectedListItems;
        }
        private List<SelectListItem> GetItemsCat()
        {
            return new List<SelectListItem>
            {
               new SelectListItem{Value = "1", Text = "IT"},
               new SelectListItem{Value = "2", Text = "HR"},
               new SelectListItem{Value = "3", Text = "Finance"}
            };
        }






    }
}
