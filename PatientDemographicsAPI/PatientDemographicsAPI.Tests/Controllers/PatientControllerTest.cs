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
        public void GetPatients()
        {
            List<PatientInfo> patientList = new List<PatientInfo>();
            var newPhones = new List<Phones>() 
            { 
                new Phones {Type = "Mobile", Number = "6456546"}
            };
            PatientInfo newPatient = new PatientInfo { Forenames = "John", Surname = "Wick", Gender = "Male", DOB = DateTime.Now, Phone = newPhones };
            patientList.Add(newPatient);

            Mock<IPatientService> mockPatientService = new Mock<IPatientService>();
            mockPatientService.Setup(x => x.GetPatients()).Returns(patientList);
            
            // Arrange
            var controller = new PatientController(mockPatientService.Object);

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            
            // Act
            var response = controller.GetPatients();

            // Assert
            Assert.AreEqual(true, patientList.Any());
        }
        
        [TestMethod]
        public void Add_ForenamesEmpty()
        {
            var newPhones = new List<Phones>() 
            { 
                new Phones {Type = "Mobile", Number = "6456546"}
            };
            PatientInfo newPatient = new PatientInfo {Forenames = "", Surname = "Wick", Gender = "Male", DOB = DateTime.Now, Phone = newPhones };

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

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();


            // Act
            var response = controller.AddPatient(newPatient);

            // Assert
            Assert.AreNotEqual(0, newPatient.ID);
        }

        [TestMethod]
        public void Add_Surname1Char()
        {
            var newPhones = new List<Phones>() 
            { 
                new Phones {Type = "Mobile", Number = "6456546"}
            };
            PatientInfo newPatient = new PatientInfo { Forenames = "John", Surname = "W", Gender = "Male", DOB = DateTime.Now, Phone = newPhones };

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

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();


            // Act
            var response = controller.AddPatient(newPatient);

            // Assert
            Assert.AreNotEqual(0, newPatient.ID);
        }


        [TestMethod]
        public void Add_NoGender()
        {
            var newPhones = new List<Phones>() 
            { 
                new Phones {Type = "Mobile", Number = "6456546"}
            };
            PatientInfo newPatient = new PatientInfo { Forenames = "John", Surname = "Wick", Gender = null, DOB = DateTime.Now, Phone = newPhones };

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

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();


            // Act
            var response = controller.AddPatient(newPatient);

            // Assert
            Assert.AreNotEqual(0, newPatient.ID);
        }

        [TestMethod]
        public void Add_ValidInput()
        {
            var newPhones = new List<Phones>() 
            { 
                new Phones {Type = "Mobile", Number = "6456546"}
            };
            PatientInfo newPatient = new PatientInfo { Forenames = "John", Surname = "Wick", Gender = "Male", DOB = DateTime.Now, Phone = newPhones };

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

            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.AddPatient(newPatient);

            // Assert
            Assert.AreNotEqual(0, newPatient.ID);
        }
    }
}

