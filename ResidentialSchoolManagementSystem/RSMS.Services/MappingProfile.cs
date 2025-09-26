using AutoMapper;
using RSMS.Common.Models;
using RSMS.Data.Models;
using RSMS.Data.Models.CoreEntities;
using RSMS.Data.Models.InventoryEntities;
using RSMS.Data.Models.LookupEntities;
using RSMS.Data.Models.SecurityEntities;


namespace RSMS.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<AssetIssue, AssetIssueDTO>()
            .ForMember(dest => dest.StudentName,
                       opt => opt.MapFrom(src => src.Student.FirstName + " " + src.Student.LastName))
            .ForMember(dest => dest.ItemName,
                       opt => opt.MapFrom(src => src.Item.Name))
            .ReverseMap();

            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<StudentAttendance, StudentAttendanceDTO>()
            .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FirstName + " " + src.Student.LastName))
            .ForMember(dest => dest.AdmissionNumber, opt => opt.MapFrom(src => src.Student.AdmissionNumber))
            .ReverseMap();

            CreateMap<Staff, StaffDTO>().ReverseMap();
            CreateMap<StaffAttendance, StaffAttendanceDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Item, ItemDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<Supplier, SupplierDTO>().ReverseMap();
            CreateMap<ItemType, ItemTypeDTO>().ReverseMap();
        }
    }
}
