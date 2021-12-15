using AutoMapper;
using Infrastructure.Dto.NavMenu;
using Infrastructure.Dto.ResetPassword;
using Infrastructure.Dto.Reviews;
using Infrastructure.Dto.ServiceRequest;
using Infrastructure.Dto.User;
using Infrastructure.Models.CommonModels;
using Infrastructure.Models.Identity;
using Infrastructure.Models.Menu;
using Infrastructure.Models.ResetPassword;
using Infrastructure.Models.Reviews;
using Infrastructure.Models.ServiceRequests;
using Infrastructure.Models.User;

namespace Infrastructure.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User and identity

            CreateMap<ApplicationUser, CurrentUser>();
            CreateMap<CurrentUser, UserModel>();
            CreateMap<ApplicationUser, UserModel>();
            CreateMap<UpdateUserInfoDto, UpdateUserInfo>();

            CreateMap<LoginUserDto, ApplicationUser>().
                ForMember(appUser => appUser.UserName,
                          opt => opt.MapFrom(userDto => userDto.Email));

            CreateMap<CreateUserDto, ApplicationUser>().
                ForMember(appUser => appUser.UserName,
                          opt => opt.MapFrom(userDto => userDto.Email)).
                ForMember(appUser => appUser.Name,
                          opt => opt.MapFrom(userDto => userDto.Name));

            CreateMap<UserModel, UserDto>();

            CreateMap<UserDto, ApplicationUser>().
                ForMember(appUser => appUser.UserName,
                          opt => opt.MapFrom(userDto => userDto.Email));

            CreateMap<ApplicationUser, LoginUserDto>().
                ForMember(appUser => appUser.Email,
                          opt => opt.MapFrom(userDto => userDto.UserName));
            CreateMap<ApplicationUser, CreateUserDto>().
                ForMember(appUser => appUser.Email,
                          opt => opt.MapFrom(userDto => userDto.UserName)).
                ForMember(appUser => appUser.Name,
                          opt => opt.MapFrom(userDto => userDto.Name));

            #endregion

            #region Service requests

            CreateMap<ServiceRequest, ServiceRequestDto>().
                ForMember(dto => dto.Reporter,
                    opt => opt.MapFrom(request => request.Reporter)).
                ForMember(dto => dto.Assignee,
                    opt => opt.MapFrom(request => request.Assignee));

            CreateMap<UpdateServiceRequestDto, ServiceRequest>();
            CreateMap<ReviewDto, Review>();
            CreateMap<Review, ReviewDto>();

            CreateMap<ServiceRequest, ReviewDisplayDto>().
                ForMember(dto => dto.ClientName,
                    opt => opt.MapFrom(request => request.Reporter.Name)).
                ForMember(dto => dto.Rate,
                    opt => opt.MapFrom(request => request.Review.Rate)).
                ForMember(dto => dto.ReviewText,
                    opt => opt.MapFrom(request => request.Review.ReviewText));

            #endregion

            #region Menu

            CreateMap<NavMenuItem, NavMenuItemDto>().
                ForMember(model => model.DisplayName,
                          opt => opt.MapFrom(appUser => appUser.Name)).
                ForMember(model => model.Href,
                          opt => opt.MapFrom(appUser => appUser.Link.ToString()));

            CreateMap<CreateNavMenuItemDto, NavMenuItem>().
                ForPath(model => model.Link.Action,
                          opt => opt.MapFrom(appUser => appUser.LinkAction)).
                ForPath(model => model.Link.Controller,
                          opt => opt.MapFrom(appUser => appUser.LinkController));

            CreateMap<UpdateNavMenuItemDto, NavMenuItem>().
                ForPath(model => model.Link.Action,
                          opt => opt.MapFrom(appUser => appUser.LinkAction)).
                ForPath(model => model.Link.Controller,
                          opt => opt.MapFrom(appUser => appUser.LinkController));

            #endregion

            #region password management

            CreateMap<ResetPasswordDto, ResetPassword>();
            CreateMap<ForgotPasswordDto, ForgotPassword>();
            CreateMap<UpdatePasswordDto, UpdatePassword>();

            #endregion
        }
    }
}
