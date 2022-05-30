using AutoMapper;
using BusinessLogicLayer.Models;
using DataAccessLayer.Models;
using DRInformationSystem.DTO;

namespace DRInformationSystem.AutoMapperProfiles;

public class OrderMapperProfile : Profile
{
	public OrderMapperProfile()
	{
		CreateMap<OrderDto, OrderModel>();
		CreateMap<OrderModel, OrderDto>();
		CreateMap<CreateOrderDto, CreateOrderData>();
		CreateMap<CreateOrderData, CreateOrderDto>();
	}
}