using AutoMapper;
using RSMS.Common.DTO;
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
               opt => opt.MapFrom(src => src.Item.Name));

            CreateMap<AssetIssueDTO, AssetIssue>()
                .ForMember(dest => dest.Student, opt => opt.Ignore())
                .ForMember(dest => dest.Item, opt => opt.Ignore());


            CreateMap<Student, StudentDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ForMember(dest => dest.GradeName, opt => opt.MapFrom(src => src.Grade != null ? src.Grade.Name : null))
                .ForMember(dest => dest.RSHostelName, opt => opt.MapFrom(src => src.RSHostel != null ? src.RSHostel.Name : null));

            CreateMap<StudentDTO, Student>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Grade, opt => opt.Ignore())
                .ForMember(dest => dest.RSHostel, opt => opt.Ignore());

            CreateMap<StudentAttendance, StudentAttendanceDTO>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FirstName + " " + src.Student.LastName))
                .ForMember(dest => dest.AdmissionNumber, opt => opt.MapFrom(src => src.Student.AdmissionNumber));

            CreateMap<StudentAttendanceDTO, StudentAttendance>()
                .ForMember(dest => dest.Student, opt => opt.Ignore());


            CreateMap<Staff, StaffDTO>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null))
                .ForMember(dest => dest.DesignationName, opt => opt.MapFrom(src => src.Designation != null ? src.Designation.Name : null));

            CreateMap<StaffDTO, Staff>()
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                .ForMember(dest => dest.Designation, opt => opt.Ignore());


            CreateMap<StaffAttendance, StaffAttendanceDTO>()
                .ForMember(dest => dest.StaffName, opt => opt.MapFrom(src => src.Staff.FullName));

            CreateMap<StaffAttendanceDTO, StaffAttendance>()
                .ForMember(dest => dest.Staff, opt => opt.Ignore());

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Item, ItemDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<Supplier, SupplierDTO>().ReverseMap();
            CreateMap<ItemType, ItemTypeDTO>().ReverseMap(); 
            CreateMap<RSHostel, HostelDTO>().ReverseMap();

            CreateMap<Inventory, InventoryDTO>().ReverseMap();
        }
    }
}
