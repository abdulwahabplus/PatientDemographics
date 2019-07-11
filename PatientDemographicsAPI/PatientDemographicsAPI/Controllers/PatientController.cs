using Models;
using PatientDemographics.Core;
using PatientDemographics.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PatientDemographicsAPI.Controllers
{
    public class PatientController : ApiController
    {
        private IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            this._patientService = patientService;
        }
        
        /// <summary>
        /// To get all patients that saved in DB
        /// </summary>
        /// <returns name="List<PatientInfo>">List of patients</returns>
        [Route("api/patients")]
        [HttpGet]
        public HttpResponseMessage GetPatients()
        {
            try
            {
                var patients = _patientService.GetPatients();
                if (patients != null && patients.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, patients);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        
        /// <summary>
        /// To create a patient
        /// </summary>
        /// <param name="PatientInfo">Contains all basic informations of a patient</param>
        /// <returns name="PatientInfo">Contains all basic informations of a patient with a valid ID (a number > 0)</returns>
        [Route("api/patient")]
        [HttpPost]
        public HttpResponseMessage AddPatient(PatientInfo input)
        {
            try
            {
                Validate<PatientInfo>(input);
                if (ModelState.IsValid)
                {
                    var patient = _patientService.CreatePatient(input);
                    if (patient != null && patient.ID > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, patient);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
