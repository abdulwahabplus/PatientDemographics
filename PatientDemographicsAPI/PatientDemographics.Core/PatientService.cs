using Models;
using PatientDemographics.Core.Model;
using Repositories.Repositories;
using Repositories.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace PatientDemographics.Core
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository patientsRepository;
        private readonly IUnitOfWork unitOfWork;

        public PatientService(IPatientRepository patientsRepository, IUnitOfWork unitOfWork)
        {
            this.patientsRepository = patientsRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<PatientInfo> GetPatients()
        {
            var patientList = new List<PatientInfo>();
            try
            {
                var patients = patientsRepository.GetAll();
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
                //Log exception here
            }
            return patientList;
        }

        public PatientInfo CreatePatient(PatientInfo patient)
        {
            try
            {
                XMLHelper xml = new XMLHelper();
                string xmlString = xml.CreateXML(patient);
                Patient patientDataToSave = new Patient();
                patientDataToSave.MetaData = xmlString;
                var newData = patientsRepository.Add(patientDataToSave);
                unitOfWork.Commit();
                patient.ID = newData.PatientID;
            }
            catch (Exception)
            {
                //Log exception here
            }
            return patient;
        }
 
        public void SavePatient()
        {
            unitOfWork.Commit();
        }
 
    }
}
