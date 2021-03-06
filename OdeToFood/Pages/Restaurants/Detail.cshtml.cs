using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class DetailModel : PageModel
    {
        public Restaurant Restaurant {get; set;}
        public IRestaurantData RestaurantData { get; set; }
        [TempData]
        public string  Message { get; set; }
        public DetailModel(IRestaurantData restaurantData)
        {
            this.RestaurantData = restaurantData;
        }
        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = RestaurantData.GetRestaurantById(restaurantId);
            if(Restaurant==null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}
