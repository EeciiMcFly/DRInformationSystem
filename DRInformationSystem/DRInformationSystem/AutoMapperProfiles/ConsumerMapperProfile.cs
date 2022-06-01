using AutoMapper;
using BusinessLogicLayer.Models;
using DataAccessLayer.Models;
using DRInformationSystem.DTO;

namespace DRInformationSystem.AutoMapperProfiles;

public class ConsumerMapperProfile : Profile
{
	public ConsumerMapperProfile()
	{
		CreateMap<RegisterConsumerData, RegistrationConsumerDto>();
		CreateMap<RegistrationConsumerDto, RegisterConsumerData>();
		CreateMap<ConsumerModel, ConsumerDto>();
		CreateMap<ConsumerDto, ConsumerModel>();
	}
}