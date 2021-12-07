using AutoMapper;
using Infrastructure.Dto.NavMenu;
using Infrastructure.Dto.ServiceRequest;
using Infrastructure.Dto.User;
using Infrastructure.Models.CommonModels;
using Infrastructure.Models.Identity;
using Infrastructure.Models.Menu;
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

            CreateMap<LoginUserDto, ApplicationUser>().
                ForMember(appUser => appUser.UserName,
                          opt => opt.MapFrom(userDto => userDto.Email));

            CreateMap<UserModel, UserDto>();

            CreateMap<UserDto, ApplicationUser>().
                ForMember(appUser => appUser.UserName,
                          opt => opt.MapFrom(userDto => userDto.Email));

            CreateMap<ApplicationUser, LoginUserDto>().
                ForMember(appUser => appUser.Email,
                          opt => opt.MapFrom(userDto => userDto.UserName));
            #endregion

            #region Service requests

            CreateMap<ServiceRequest, ServiceRequestDto>().
                ForMember(dto => dto.Status,
                    opt => opt.MapFrom(request => request.Status.ToString())).
                ForMember(dto => dto.Reporter,
                    opt => opt.MapFrom(request => request.Reporter)).
                ForMember(dto => dto.Assignee,
                    opt => opt.MapFrom(request => request.Assignee));

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
        }
    }
}
