using Models;
using PatientDemographics.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDemographics.Core
{
    public interface IPatientService
    {
        IEnumerable<PatientInfo> GetPatients();
        PatientInfo CreatePatient(PatientInfo patient);
        void SavePatient();
    }
}
