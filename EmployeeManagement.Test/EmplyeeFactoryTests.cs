using EmployeeManagement.Business;
using EmployeeManagement.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test
    {
    public class EmplyeeFactoryTests
        {
        [Fact]//Unit test
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500()
            {

            //Arrange
            var EmployeeFactory = new EmployeeFactory();
            //Act
            var employee = (InternalEmployee)EmployeeFactory.CreateEmployee("Kevin", "Dockx");
            //Assert
            Assert.Equal(2500, employee.Salary);//check for a specific outcome
            }

        [Fact]//Unit test
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500()
            {

            //Arrange
            var EmployeeFactory = new EmployeeFactory();
            //Act
            var employee = (InternalEmployee)EmployeeFactory.CreateEmployee("Kevin", "Dockx");
            //Assert
            Assert.True(employee.Salary >= 3000 && employee.Salary <= 3500, "Salary not in acceptable range");//check for a specific outcome
            }

        [Fact]
        
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500_Alternative()
            {

            //Arrange
            var EmployeeFactory = new EmployeeFactory();
            //Act
            var employee = (InternalEmployee)EmployeeFactory.CreateEmployee("Kevin", "Dockx");
            //Assert
            Assert.True(employee.Salary >= 2500);
            Assert.True(employee.Salary <= 3500);
            }

        [Fact]
        
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBeBetween2500And3500_AlternativeWithInRange()
            {

            //Arrange
            var EmployeeFactory = new EmployeeFactory();
            //Act
            var employee = (InternalEmployee)EmployeeFactory.CreateEmployee("Kevin", "Dockx");
            //Assert
            Assert.InRange(employee.Salary, 2500, 3500);
            }

        [Fact]//Unit test
        public void CreateEmployee_ConstructInternalEmployee_SalaryMustBe2500_PercisionExample()
            {

            //Arrange
            var EmployeeFactory = new EmployeeFactory();
            //Act
            var employee = (InternalEmployee)EmployeeFactory.CreateEmployee("Kevin", "Dockx");
            employee.Salary = 2500.123m;
            //Assert
            Assert.Equal(2500, employee.Salary, 0);//check for a specific outcome
            }
        [Fact]

        public void CreateEmployee_IsExternalIsTrue_ReturnTypeMustBeExternalEmployee()
            
            {
            //Arrange
            var factory = new EmployeeFactory();

            //Act
            var employee = factory.CreateEmployee("Kevin", "Dockx", "Marvin", false);

            //Assert
            Assert.IsType<ExternalEmployee>(employee);
            }
       
        }
    }
