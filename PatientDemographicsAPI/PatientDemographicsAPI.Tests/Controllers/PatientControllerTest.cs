using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientDemographicsAPI;
using PatientDemographicsAPI.Controllers;
using PatientDemographics.Core.Model;
using PatientDemographics.Core;
using Moq;

namespace PatientDemographicsAPI.Tests.Controllers
{
    [TestClass]
    public class PatientControllerTest
    {
        [TestMethod]
        public void Add()
        {
            var newPhones = new List<Phones>() 
            { 
                new Phones {Type = "Mobile", Number = 6456546}
            };
            PatientInfo newPatient = new PatientInfo {Forenames = "John", Surname = "Wick", Gender = "Male", DOB = DateTime.Now, Phone = newPhones };

            Mock<IPatientService> mockPatientService = new Mock<IPatientService>();
            mockPatientService.Setup(x => x.CreatePatient(newPatient)).Returns(
                (PatientInfo target) =>
                {
                    DateTime now = DateTime.Now;

                    if (target.ID.Equals(default(int)))
                    {
                        target.ID = 1;
                    }
                    return target;
                });


  
            // Arrange
            var controller = new PatientController(mockPatientService.Object);
            //controller.Validate(newPatient);

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();


            // Act
            var response = controller.AddPatient(newPatient);

            // Assert
            Assert.AreNotEqual(0, newPatient.ID);
        }
    }
}

