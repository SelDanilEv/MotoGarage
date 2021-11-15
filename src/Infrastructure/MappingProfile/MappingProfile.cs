using AutoMapper;
using Infrastructure.Dto.NavMenu;
using Infrastructure.Models.CommonModels;
using Infrastructure.Models.Identity;
using Infrastructure.Models.Menu;

namespace Infrastructure.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User and identity
            CreateMap<ApplicationUser, CurrentUser>();

            CreateMap<ApplicationUser, UserModel>().
                ForMember(model => model.Login, opt => opt.MapFrom(appUser => appUser.UserName));
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
