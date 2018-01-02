﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionCommittee.Domain.Entities
{
    public class Address
    {
        public int EnrolleeID { get; set; }

        public virtual Enrollee Enrollee { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        [Required(ErrorMessage = "Please enter a city")]
        public string City { get; set; }

        public string Street { get; set; }

        public int BuildingNumber { get; set; }

        public int ApartmentNumber { get; set; }

        public int PostalCode { get; set; }
    }
}