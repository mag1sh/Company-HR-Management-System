using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyHRManagementSystem.Application.Interfaces
{
    public interface IPositionRepository
    {
        void Save(Position position);

        IReadOnlyList<Position> GetAll();

        Position GetById(int positionId);

        List<Position> GetByDepartmentId(int departmentId);

        bool ExistsByTitle(string title);
    }
}
