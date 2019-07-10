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
        private IPatientService patientService;
        public PatientController(IPatientService patientService)
        {
            this.patientService = patientService;
        }
        // GET api/patients
        [Route("api/patients")]
        [HttpGet]
        public HttpResponseMessage GetPatients()
        {
            try
            {
                var patients = patientService.GetPatients();
                if (patients != null && patients.Count() > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, patients);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, patients);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        
        // POST api/patient
        [Route("api/patient")]
        [HttpPost]
        public HttpResponseMessage AddPatient([FromBody]PatientInfo input)
        {
            try
            {
                Validate<PatientInfo>(input);
                if (ModelState.IsValid)
                {
                    var patient = patientService.CreatePatient(input);
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
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
