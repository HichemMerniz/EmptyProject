using Application.DTOs.Roles;
using Application.DTOs.Users;
using AutoMapper;
using Domain.Models;

namespace Api.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        //Users
        CreateMap<Users, UserDto>()
            .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.UserName}")
            )
            .ForMember(
                dest => dest.Mail,
                opt => opt.MapFrom(src => $"{src.Email}")
            )
            .ForMember(
                dest => dest.Phone,
                opt => opt.MapFrom(src => $"{src.PhoneNumber}")
            )
            .ForMember(
                dest => dest.Company,
                opt => opt.MapFrom(src => $"{src.Company}")
            ).ForMember(
                dest => dest.Active,
                opt => opt.MapFrom(src => $"{src.Active}")
            ).ForMember(
                dest => dest.ProfileName,
                opt => opt.MapFrom(src => $"{src.ProfileName}")
            ).ForMember(
                dest => dest.Company,
                opt => opt.MapFrom(src => $"{src.Company}")
            );

        CreateMap<CreateUserDto, Users>()
            .ForMember(
                dest => dest.UserName,
                opt => opt.MapFrom(src => $"{src.UserName}")
            )
            .ForMember(
                dest => dest.Email,
                opt => opt.MapFrom(src => $"{src.Email}")
            )
            .ForMember(
                dest => dest.PhoneNumber,
                opt => opt.MapFrom(src => $"{src.Phone}")
            ).ForMember(
                dest => dest.Company,
                opt => opt.MapFrom(src => $"{src.Company}")
            );

        //Roles
        CreateMap<Roles, CreateRoleDto>().ReverseMap();
    }
}