using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>
            {
                new Restaurant{Id=1,Cuisine=CuisineType.Indian,Location="Cairo",Name="IndianFood"},
                new Restaurant{Id=2,Cuisine=CuisineType.Italian,Location="Alex",Name="ItalianFood"},
                new Restaurant{Id=3,Cuisine=CuisineType.None,Location="Qalubia",Name="ShoubraFood"},
                new Restaurant{Id=4,Cuisine=CuisineType.Mexican,Location="Luxer",Name="MexicanFood"},
            };
        }
        public IEnumerable<Restaurant> GetRestaurantByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.ToLower().StartsWith(name.ToLower())
                   orderby r.Name
                   select r;
        }
        public Restaurant GetRestaurantById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }
        public Restaurant Update(Restaurant updatedRestaurant)
        {
            Restaurant restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if (restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }
        public Restaurant AddRestaurant(Restaurant restaurant)
        {
            restaurants.Add(restaurant);
            restaurant.Id = restaurants.Max(r => r.Id) + 1;
            return restaurant;
        }
        public Restaurant Delete(int id)
        {
            Restaurant restaurant = restaurants.FirstOrDefault(r => r.Id == id);
            if(restaurant!=null)
            {
                restaurants.Remove(restaurant);
            }
            return restaurant;
        }
        public int Commit()
        {
            return 0;
        }
    }
}
