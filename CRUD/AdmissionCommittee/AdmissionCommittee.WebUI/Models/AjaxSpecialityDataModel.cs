using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdmissionCommittee.WebUI.Models
{
	public class AjaxSpecialityDataModel
	{
        public int? GroupId { get; set; }

        public int? EducationPlaceId { get; set; }

        public int? FinancingTypeId { get; set; }

        public int? SpecialityId { get; set; }

        public int? EducationFormId { get; set; }

        public int? EducationDurationId { get; set; }

        public int[] SelectedSpecialities { get; set; }
    }
}