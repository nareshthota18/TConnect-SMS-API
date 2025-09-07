using AutoMapper;
using RSMS.Business.Contracts;
using RSMS.Common.Models;
using RSMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSMS.Business.Implementation
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IMapper _mapper;
        public AttendanceRepository(IAttendanceService attendanceService,IMapper mapper)
        {
            _attendanceService = attendanceService; 
            _mapper=mapper;
        }
        public Task<StaffAttendanceDTO> AddStaffAttendanceAsync(StaffAttendanceDTO attendance)
        {
            throw new NotImplementedException();
        }

        public Task<StudentAttendanceDTO> AddStudentAttendanceAsync(StudentAttendanceDTO attendance)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteStaffAttendanceAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteStudentAttendanceAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StaffAttendanceDTO>> GetAllStaffAttendanceAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentAttendanceDTO>> GetAllStudentAttendanceAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StaffAttendanceDTO?> GetStaffAttendanceByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<StudentAttendanceDTO?> GetStudentAttendanceByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<StaffAttendanceDTO> UpdateStaffAttendanceAsync(StaffAttendanceDTO attendance)
        {
            throw new NotImplementedException();
        }

        public Task<StudentAttendanceDTO> UpdateStudentAttendanceAsync(StudentAttendanceDTO attendance)
        {
            throw new NotImplementedException();
        }
    }
}
