using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class Interview
    {
        public Guid InterviewID { get; set; }

        public int InterviewNo { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public Guid EducationId { get; set; }

        public Guid DepartmentId { get; set; }

        public Guid DesignationId { get; set; }

        public string Education { get; set; }

        public string Department { get; set; }

        public string Designation { get; set; }

        public decimal? CurrentSalary { get; set; }

        public decimal? ExpectedSalary { get; set; }

        public int ExperienceYear { get; set; }

        public int ExperienceMonth { get; set; }

        public bool IsJoinDays { get; set; }

        public int JoinAfterDaysOrMonth { get; set; }

        public string PersonalDetail { get; set; }

        public int InterviewStatusId { get; set; }

        public string Status { get; set; }

        public DateTime? InterviewDate { get; set; }

        public string InterviewTime { get; set; }

        public DateTime? JoinDate { get; set; }

        public string Reason { get; set; }

        public List<InterviewAttachmentModel> ListOfInterviewAttachment { get; set; }

    }

    public class InterviewAttachmentModel
    {
        public Guid InterviewAttechmentMapID { get; set; }
        public string AttachmentName { get; set; }

        public int? AttachmentType { get; set; }

        public bool IsDelete { get; set; }

    }
}
