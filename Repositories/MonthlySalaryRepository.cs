using Entityframework.LabExercise2.Data;
using Entityframework.LabExercise2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entityframework.LabExercise2.Repositories
{
    internal class MonthlySalaryRepository
    {
        public RecruitmentDbContext Context { get; set; }

        public MonthlySalaryRepository(RecruitmentDbContext context)
        {
            this.Context = context;
        }

        public IEnumerable<MonthlySalary> FindAll(string employeeCode)
        {
            return this.Context.MonthlySalaries.Where(x => x.CEmployeeCode.Equals(employeeCode)).ToList();
        }
    }
}
