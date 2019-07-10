using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatientDemographics.Core.Model
{
    public class PatientInfo
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Forenames { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Surname { get; set; }
        [DataType("dd-MM-yyyy")]
        public DateTime? DOB { get; set; }
        [Required]
        public string Gender { get; set; }
        public List<Phones> Phone { get; set; }
    }

    public class Phones
    {
        public string Type { get; set; }
        public string Number { get; set; }
    }
}