using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;
using System.Xml.Serialization;


namespace Restaurants.Application.Restaurants.Dtos
{
    public class RestaurantsProfile : Profile
    {
        public RestaurantsProfile()
        {
            CreateMap<UpdateRestaurantCommand, Restaurant>();

            CreateMap<CreateRestaurantCommand, Restaurant>()
                .ForMember(d => d.Adress, opt => opt.MapFrom(
                    src => new Adress
                    {
                        City = src.City,
                        Street = src.Street,
                        PostalCode = src.PostalCode,
                    }));
           
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(d => d.City, opt => 
                    opt.MapFrom(src => src.Adress == null ? null : src.Adress.City))
                .ForMember(d => d.Street, opt =>
                    opt.MapFrom(src => src.Adress == null ? null : src.Adress.Street))
                .ForMember(d => d.PostalCode, opt => 
                    opt.MapFrom(src => src.Adress == null ? null : src.Adress.PostalCode))
                .ForMember(d => d.Dishes, opt => opt.MapFrom(src => src.Dishes));
        }
    }
}
