using AutoMapper;
using DataAccessLayer.Models;
using DRInformationSystem.DTO;

namespace DRInformationSystem.AutoMapperProfiles;

public class ResponseMapperProfile : Profile
{
	public ResponseMapperProfile()
	{
		CreateMap<ResponseDto, ResponseModel>();
		CreateMap<ResponseModel, ResponseDto>();
	}
}