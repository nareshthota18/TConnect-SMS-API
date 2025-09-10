using AutoMapper;
using RSMS.Common.Models;
using RSMS.Data.Models;
using RSMS.Data.Models.CoreEntities;
using RSMS.Data.Models.InventoryEntities;
using RSMS.Data.Models.SecurityEntities;


namespace RSMS.Services
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        { 
            CreateMap<AssetIssue,AssetDTO>().ReverseMap();
            CreateMap<Student,StudentDTO>().ReverseMap();
            CreateMap<StudentAttendance,StudentAttendanceDTO>().ReverseMap();
            CreateMap<Staff,StaffDTO>().ReverseMap();
            CreateMap<StaffAttendance,StaffAttendanceDTO>().ReverseMap();
            CreateMap<User,UserDTO>().ReverseMap();
            CreateMap<Item,ItemDTO>().ReverseMap();


        } 
    }
}
