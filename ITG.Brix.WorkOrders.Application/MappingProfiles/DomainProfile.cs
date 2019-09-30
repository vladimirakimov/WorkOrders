using AutoMapper;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Models;
using ITG.Brix.WorkOrders.Domain;

namespace ITG.Brix.WorkOrders.Application.MappingProfiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {


            CreateMap<Order, OrderModel>();

            CreateMap<OperationType, string>().ConvertUsing(src => src.Name);
            CreateMap<UnitType, string>().ConvertUsing(src => src.Name);
            CreateMap<Weight, string>().ConvertUsing(src => src != null ? (string)src : null);
            CreateMap<Quantity, int>().ConvertUsing(src => (int)src);
            CreateMap<Units, int>().ConvertUsing(src => (int)src);



            CreateMap<Warehouse, string>().ConvertUsing(src => (string)src);
            CreateMap<Gate, string>().ConvertUsing(src => (string)src);
            CreateMap<Row, string>().ConvertUsing(src => (string)src);
            CreateMap<Position, string>().ConvertUsing(src => (string)src);

            CreateMap<CreatedOn, string>().ConvertUsing(src => (string)src);
            CreateMap<DateOn, string>().ConvertUsing(src => (string)src);
            CreateMap<HandledOn, string>().ConvertUsing(src => (string)src);

            CreateMap<Login, string>().ConvertUsing(src => (string)src);
            CreateMap<Remark, RemarkModel>()
                .ForMember(dest => dest.Operant, opt => opt.MapFrom(src => src.Operant.Login));
            CreateMap<Picture, PictureModel>()
                .ForMember(dest => dest.Operant, opt => opt.MapFrom(src => src.Operant.Login));

            CreateMap<HandledUnit, HandledUnitModel>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Goods));
            CreateMap<Operational, OperationalModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.HandledUnits));
            CreateMap<Input, InputModel>()
               .ForMember(dest => dest.Operant, opt => opt.MapFrom(src => src.Operant.Login));


            //.ForMember(dest => dest.StartedOn, opt => opt.MapFrom(src => src.StartedOn._value))
            //.ForMember(dest => dest.StoppedOn, opt => opt.MapFrom(src => src.StoppedOn._value));
        }
    }
}
