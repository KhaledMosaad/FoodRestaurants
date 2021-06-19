using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData RestaurantData;
        private readonly IHtmlHelper HtmlHelper;
        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }
        public EditModel(IRestaurantData restaurantData,IHtmlHelper htmlHelper)
        {
            this.RestaurantData = restaurantData;
            this.HtmlHelper = htmlHelper;
            
        }
        public IActionResult OnGet(int? restaurantId)
        {
            Cuisines = HtmlHelper.GetEnumSelectList<CuisineType>();
            if (restaurantId.HasValue)
            {
                Restaurant = RestaurantData.GetRestaurantById(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }
            if(Restaurant==null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Restaurant.Id > 0)
                {
                    RestaurantData.Update(Restaurant);
                }
                else
                {
                    RestaurantData.AddRestaurant(Restaurant);
                }
                RestaurantData.Commit();
                TempData["Message"] = "Restaurant Saved!";
                return RedirectToPage("./Detail",
                    new {restaurantId=Restaurant.Id });
            }
            Cuisines = HtmlHelper.GetEnumSelectList<CuisineType>();
            return Page();
        }
    }
}
