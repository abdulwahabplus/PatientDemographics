using Models;
using PatientDemographics.Core.Model;
using Repositories.Repositories;
using Repositories.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientDemographics.Core.Common;
using PatientDemographics.Logger;

namespace PatientDemographics.Core
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IPatientRepository patientsRepository, IUnitOfWork unitOfWork)
        {
            this._patientsRepository = patientsRepository;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get list patients
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PatientInfo> GetPatients()
        {
            var patientList = new List<PatientInfo>();
            try
            {
                var patients = _patientsRepository.GetAll();
                if (patients != null)
                {
                    foreach (Patient item in patients)
                    {
                        PatientInfo patientData = new PatientInfo();
                        XMLHelper xml = new XMLHelper();
                        patientData = (PatientInfo)xml.CreateObject(item.MetaData, patientData);
                        if (patientData != null)
                        {
                            patientList.Add(patientData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Logging exception
                LogHelper.Log(ex.Message);
            }
            return patientList;
        }

        /// <summary>
        /// Create a patient
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public PatientInfo CreatePatient(PatientInfo patient)
        {
            try
            {
                XMLHelper xml = new XMLHelper();
                string xmlString = xml.CreateXML(patient);
                Patient patientDataToSave = new Patient();
                patientDataToSave.MetaData = xmlString;
                var newData = _patientsRepository.Add(patientDataToSave);
                SavePatient();
                patient.ID = newData.PatientID;
            }
            catch (Exception ex)
            {
                //Logging exception
                LogHelper.Log(ex.Message);
            }
            return patient;
        }
 
        public void SavePatient()
        {
            _unitOfWork.Commit();
        }
 
    }
}
