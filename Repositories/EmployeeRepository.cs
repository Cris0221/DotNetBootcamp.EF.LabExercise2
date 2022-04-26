using Entityframework.LabExercise2.Data;
using Entityframework.LabExercise2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entityframework.LabExercise2.Repositories
{
    internal class EmployeeRepository
    {
        public RecruitmentDbContext Context { get; set; }

        public EmployeeRepository(RecruitmentDbContext context)
        {
            this.Context = context;
        }

        public IEnumerable<Employee> FindAll()
        {
            return this.Context.Employees.Select(p => p).ToList();
        }

        public Employee FindByEmployeeCode(string code)
        {
            var employee = this.Context.Employees.Where(p=>p.CEmployeeCode == code).FirstOrDefault();
           
            if (employee != null)
            {
                return employee;
            }
            throw new Exception($"Model with ID {code} doesn't exist");
        }

        public Employee EmployeePosition(Employee employee)
        {
            this.FindByEmployeeCode(employee.CEmployeeCode);
            string code = employee.CCurrentPosition.ToString();
            var position = this.Context.Positions.Where(e => e.CPositionCode == code).FirstOrDefault();
            if (position != null)
            {
                employee.CCurrentPosition = position.VDescription;
                return employee;
            }
            throw new Exception($"Employee with Current Position code {code} does'nt exist");
        }
    }
}
