using AutoMapper;
using TreePorts.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace TreePorts.DTO.ReturnDTO
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			/* User Mapping*/
			CreateMap<User, UserResponse>()

					.ForMember(dest => dest.CountryArabicName, opt => opt.MapFrom(src => src.Country.ArabicName))
					.ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name))
					.ForMember(dest => dest.CityArabicName, opt => opt.MapFrom(src => src.City.ArabicName))
					.ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
					.ForMember(dest => dest.ResidenceCityName, opt => opt.MapFrom(src => src.ResidenceCityName))
					.ForMember(dest => dest.ResidenceCountryName, opt => opt.MapFrom(src => src.ResidenceCountryName))
					.ForMember(dest => dest.ResidenceCountryArabicName, opt => opt.MapFrom(src => src.ResidenceCountryArabicName))
					.ForMember(dest => dest.ResidenceCityArabicName, opt => opt.MapFrom(src => src.ResidenceCityArabicName))
					.ForMember(dest => dest.StatusTypeId, opt => opt.MapFrom(src => src.UserAccounts.FirstOrDefault().StatusTypeId));

			/* User Mapping*/

			/* Admin Mapping*/
			CreateMap<AdminUser, AdminResponse>()

					.ForMember(dest => dest.CountryArabicName, opt => opt.MapFrom(src => src.Country.ArabicName))
					.ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name))
					.ForMember(dest => dest.CityArabicName, opt => opt.MapFrom(src => src.City.ArabicName))
					.ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
					.ForMember(dest => dest.ResidenceCityName, opt => opt.MapFrom(src => src.ResidenceCityName))
					.ForMember(dest => dest.ResidenceCountryName, opt => opt.MapFrom(src => src.ResidenceCountryName))
					.ForMember(dest => dest.ResidenceCountryArabicName, opt => opt.MapFrom(src => src.ResidenceCountryArabicName))
					.ForMember(dest => dest.ResidenceCityArabicName, opt => opt.MapFrom(src => src.ResidenceCityArabicName));

			/* Admin Mapping*/

			/* Agent Mapping*/
			CreateMap<Agent, AgentResponse>()

					.ForMember(dest => dest.CountryArabicName, opt => opt.MapFrom(src => src.Country.ArabicName))
					.ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name))
					.ForMember(dest => dest.CityArabicName, opt => opt.MapFrom(src => src.City.ArabicName))
					.ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name));

			
			/* Agent Mapping*/

			/*SupportUser Mapping*/
			CreateMap<SupportUser, SupportUserResponse>()

					.ForMember(dest => dest.CountryArabicName, opt => opt.MapFrom(src => src.Country.ArabicName))
					.ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name))
					.ForMember(dest => dest.CityArabicName, opt => opt.MapFrom(src => src.City.ArabicName))
					.ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
					.ForMember(dest => dest.ResidenceCityName, opt => opt.MapFrom(src => src.ResidenceCityName))
					.ForMember(dest => dest.ResidenceCountryName, opt => opt.MapFrom(src => src.ResidenceCountryName))
					.ForMember(dest => dest.ResidenceCountryArabicName, opt => opt.MapFrom(src => src.ResidenceCountryArabicName))
					.ForMember(dest => dest.ResidenceCityArabicName, opt => opt.MapFrom(src => src.ResidenceCityArabicName));
			/* Agent Mapping*/

			/*Order Mapping*/
			CreateMap<City, CityReponse>();
			CreateMap<Country, CountryResponse>();
		    CreateMap<OrderItem, OrderItemResponse>();
			CreateMap<OrderAssignment, OrderAssignReponse>();

			CreateMap<UserAcceptedRequest, UserAcceptedResponse>();
					
			CreateMap<Order, OrderResponse>()
				.ForMember(dest => dest.AgnetName, opt => opt.MapFrom(src => src.Agent.Fullname))
				.ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Qrcodes.FirstOrDefault().Code))
				.ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType.Type))
				.ForMember(dest => dest.PaymentArabicType, opt => opt.MapFrom(src => src.PaymentType.ArabicType))
				.ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Type))
				.ForMember(dest => dest.ProductArabicType, opt => opt.MapFrom(src => src.ProductType.ArabicType))
				.ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
				.ForMember(dest => dest.CurrentStatus, opt => opt.MapFrom(src => src.OrderCurrentStatus.FirstOrDefault().StatusTypeId)); ;
				
				

			//.ConvertUsing<EventLogConverter>(); ;
			/*Order Mapping*/

			/* Web Hook Types Mapping*/
			CreateMap<WebHookType, WebHookTypeResponse>();
			/* Web Hook Types Mapping*/
		}
	}
}
