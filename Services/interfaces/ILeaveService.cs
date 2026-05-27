using CompanyHRManagementSystem.Employees.Domain.Entities;
using Domain.Entities;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface ILeaveService
    {
        void RequestLeave(Leave leave);

        void ApproveLeave(int leaveId);

        void RejectLeave(int leaveId);

        List<Leave> GetAllLeaves();
    }
}