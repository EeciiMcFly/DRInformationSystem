using AutoMapper;
using DataAccessLayer.Models;
using DRInformationSystem.DTO;

namespace DRInformationSystem.AutoMapperProfiles;

public class AggregatorMapperProfile : Profile
{
	public AggregatorMapperProfile()
	{
		CreateMap<AggregatorModel, AggregatorDto>();
		CreateMap<AggregatorDto, AggregatorModel>();
	}
}