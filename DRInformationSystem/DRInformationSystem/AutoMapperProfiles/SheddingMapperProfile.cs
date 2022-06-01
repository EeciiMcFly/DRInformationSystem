using AutoMapper;
using DataAccessLayer.Models;
using DRInformationSystem.DTO;

namespace DRInformationSystem.AutoMapperProfiles;

public class SheddingMapperProfile : Profile
{
	public SheddingMapperProfile()
	{
		CreateMap<SheddingDto, SheddingModel>();
		CreateMap<SheddingModel, SheddingDto>();
	}
}