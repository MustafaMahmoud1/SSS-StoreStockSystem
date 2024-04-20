using AutoMapper;
using SSS_StoreStockSystem.DAL.Models;
using SSS_StoreStockSystem.ViewModels;

namespace SSS_StoreStockSystem.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Item, ItemViewModel>().ReverseMap();
            CreateMap<Store, StoreViewModel>().ReverseMap();
        }
    }
}
