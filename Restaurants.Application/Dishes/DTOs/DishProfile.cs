using AutoMapper;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.UpdateDish;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.DTOs
{
    public class DishProfile:Profile
    {
        public DishProfile()
        {
            CreateMap<UpdateDishCommand, Dish>();
            CreateMap<CreateDishCommand, Dish>();
            CreateMap<Dish, DishDTO>();
        }
    }
}
