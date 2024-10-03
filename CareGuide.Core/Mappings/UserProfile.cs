using AutoMapper;
using CareGuide.Models.DTOs.User;
using CareGuide.Models.Tables;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponseDto>();
    }
}
