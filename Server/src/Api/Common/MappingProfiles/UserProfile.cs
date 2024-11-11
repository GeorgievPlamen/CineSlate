using Api.Users.Requests;
using Application.Users.Login;
using Application.Users.Register;
using AutoMapper;

namespace Api.Common.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<LoginRequest, LoginCommand>();
        CreateMap<RegisterRequest, RegisterCommand>();
    }
}