using EmployeeManagement.Business;
using EmployeeManagement.Business.EventArguments;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.DataAccess.Entities;
using EmployeeManagement.Services.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test
    {
    public class EmployeeServiceTests
        {

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse_WithObject()
            {
            //Arrange
            var employeeManagementTestDataRepository =
                new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(employeeManagementTestDataRepository, 
                new EmployeeFactory());
          
            //Act
            var internalEmployee = employeeService.CreateInternalEmployee("Brooklyn", "Cannon");

            //Assert 
            Assert.Contains(internalEmployee.AttendedCourses, course => course.Id == Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));

            }

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_MustHaveAttendedFirstObligatoryCourse_WithPridicate()
            {
            //Arrange
            var employeeManagementTestDataRepository =
                new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(employeeManagementTestDataRepository,
                new EmployeeFactory());

            //Act
            var internalEmployee = employeeService.CreateInternalEmployee("Brooklyn", "Cannon");

            //Assert 
            Assert.Contains(internalEmployee.AttendedCourses, course => course.Id == Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"));

            }

        [Fact]
        public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustMatchObligatoryCourses()
            {
            // Arrange 
            var employeeManagementTestDataRepository =
               new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(employeeManagementTestDataRepository,
                new EmployeeFactory());
            var obligatoryCourses = employeeManagementTestDataRepository.GetCourses
                ( Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                  Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

            //   var obligatoryCourses = _employeeServiceFixture
            //    .EmployeeManagementTestDataRepository
            //    .GetCourses(
            //        Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
            //        Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

            // Act
            var internalEmployee = employeeService.CreateInternalEmployee("Brooklyn", "Cannon");

            // Assert
            Assert.Equal(obligatoryCourses, internalEmployee.AttendedCourses);
            }

        [Fact]  
        public void CreateInternalEmployee_InternalEmployeeCreated_AttendedCourseMustNotBeNew()
            {
            // Arrange 
            var employeeManagementTestDataRepository =
               new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(employeeManagementTestDataRepository,
                new EmployeeFactory());

            //Act

            var internalEmmployee = employeeService.CreateInternalEmployee("Brooklyn", "Cannon");
            //internalEmmployee.AttendedCourses[0].IsNew = true;
            //Assert
            Assert.All(internalEmmployee.AttendedCourses, course => Assert.False(course.IsNew));
            }


        [Fact]
        public async Task CreateInternalEmployee_InternalEmployeeCreated_AttendedCoursesMustMatchObligatoryCourses_Async()
            {
            // Arrange 
            var employeeManagementTestDataRepository =
               new EmployeeManagementTestDataRepository();
            var employeeService = new EmployeeService(employeeManagementTestDataRepository,
                new EmployeeFactory());
            var obligatoryCourses = await employeeManagementTestDataRepository.GetCoursesAsync
                (Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
                  Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

            //   var obligatoryCourses = _employeeServiceFixture
            //    .EmployeeManagementTestDataRepository
            //    .GetCourses(
            //        Guid.Parse("37e03ca7-c730-4351-834c-b66f280cdb01"),
            //        Guid.Parse("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"));

            // Act
            var internalEmployee = await employeeService.CreateInternalEmployeeAsync("Brooklyn", "Cannon");

            // Assert
            Assert.Equal(obligatoryCourses, internalEmployee.AttendedCourses);
            }

        [Fact]
        public async Task GiveRaise_RaiseBelowMinimumGiven_EmployeeInvalidRaiseExceptionMustBeThrown()
            {
            //Arrange
          
            var employeeService = new EmployeeService(new EmployeeManagementTestDataRepository(),
                new EmployeeFactory());
            var internalEmployee = new InternalEmployee("Brooklyn", "Cannon", 5, 3000, false, 1);

            //Act
           await Assert.ThrowsAsync<EmployeeInvalidRaiseException>(
                async () => 
                await employeeService.GiveRaiseAsync(internalEmployee, 50));
          

            }


        [Fact]
        public void NotifyOfAbsense_EmployeeIsAbsent_OnAbsentMustBeTriggered()
            {
            //Arrange

            var employeeService = new EmployeeService(new EmployeeManagementTestDataRepository(),
                new EmployeeFactory());
            var internalEmployee = new InternalEmployee("Brooklyn", "Cannon", 5, 3000, false, 1);

            //Act and Assert
           Assert.Raises<EmployeeIsAbsentEventArgs>
                (
               handler => employeeService.EmployeeIsAbsent += handler,
               handler => employeeService.EmployeeIsAbsent -= handler,
               () => employeeService.NotifyOfAbsence(internalEmployee));   

            }

        }
    }
