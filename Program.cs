using Entityframework.LabExercise2.Data;
using Entityframework.LabExercise2.Models;
using Entityframework.LabExercise2.Repositories;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Entityframework.LabExercise2
{
    class Program
    {
        static Employee FindByCodeTest(string code, EmployeeRepository repository)
        {
            return repository.FindByEmployeeCode(code);
        }
      

        static void Main(string[] args)
        {
            ConfigurationHelper configurationHelper = ConfigurationHelper.Instance();
            var dbConnectionString = configurationHelper.GetProperty<string>("DbConnectionString");

            using (RecruitmentDbContext context = new RecruitmentDbContext(dbConnectionString))
            {
                EmployeeRepository repository = new EmployeeRepository(context);
                AnnualSalaryRepository annualRepository = new AnnualSalaryRepository(context);
                MonthlySalaryRepository monthlySalaryRepository = new MonthlySalaryRepository(context);
                SkillRepository skillRepository = new SkillRepository(context);

                try
                {
                    Console.Write("Enter employee code: ");
                    string inputEmployee = Console.ReadLine();
                    Console.Clear();
                    Employee employee = FindByCodeTest(inputEmployee, repository);
                    Employee position = repository.EmployeePosition(employee);
                    Console.WriteLine($"Employee code: {employee.CEmployeeCode}");
                    Console.WriteLine($"First Name: {employee.VFirstName}");
                    Console.WriteLine($"LastName: {employee.VLastName}");
                    Console.WriteLine($"Position: {position.CCurrentPosition}");

                    IEnumerable<AnnualSalary> annualSalary = annualRepository.FindAll(inputEmployee);
                    Console.WriteLine("\nEmployee Annual Salaries: ");
                    foreach (var salary in annualSalary)
                    {
                        Console.WriteLine($"Year: { salary.SiYear}, Salary: { salary.MAnnualSalary}");
                    }

                    IEnumerable<MonthlySalary> monthlySalary = monthlySalaryRepository.FindAll(inputEmployee);
                    Console.WriteLine("\nEmployee Monthly Salaries: ");
                    foreach (var salary in monthlySalary)
                    {
                        Console.WriteLine($"Salary: { salary.MMonthlySalary},Referral Bonus: {salary.MReferralBonus}");
                    }

                    IEnumerable<dynamic> dynamics = skillRepository.FindAll(inputEmployee);
                    Console.WriteLine("\nEmployee Skills: ");
                    foreach (var skill in dynamics)
                    {
                        Console.WriteLine($"{skill.CSkillCode}");
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            
        }
    }
}
