using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class UserModel
    {
        public Guid UserID { get; set; }
        public Guid RoleId { get; set; }
        public Guid? WorkLocationId { get; set; }
        public string RoleName { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public string Token { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
