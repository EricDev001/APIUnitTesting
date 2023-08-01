﻿using EmployeeManagement.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagement.Test
    {
    public class CourseTest
        {
        [Fact]
        public void CourseConstructor_ConstructCourse_IsNewMustBeTrue()
            {
            //Arrange
            //nothing to see here

            //Act
            var course = new Course("Disaster Management 101");

            //Assert
            Assert.True(course.IsNew);
            }
        }
    }