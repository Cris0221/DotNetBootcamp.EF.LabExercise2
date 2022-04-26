using Entityframework.LabExercise2.Data;
using Entityframework.LabExercise2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entityframework.LabExercise2.Repositories
{
    internal class SkillRepository
    {
        public RecruitmentDbContext Context { get; set; }

        public SkillRepository(RecruitmentDbContext context)
        {
            this.Context = context;
        }

        public IEnumerable<dynamic> FindAll(string employeeCode)
        {
            var skills = this.Context.EmployeeSkills.Join(Context.Skills,x => x.CSkillCode, p => p.CSkillCode,(x, p) => new
            {
                CEmployeeCode = x.CEmployeeCode,
                CSkillCode = p.VSkill
            }).Where(e => e.CEmployeeCode == employeeCode).ToList();
            return skills;
        }
    }
}
