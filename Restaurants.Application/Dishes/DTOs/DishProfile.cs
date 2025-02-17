using AutoMapper;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.DTOs
{
    public class DishProfile:Profile
    {
        public DishProfile()
        {
            CreateMap<Dish, DishDTO>();
        }
    }
}
