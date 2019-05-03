using DomesticManagement.Common.Auditable;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DomesticManagement.Database.Entities
{
    public class User : IdentityUser<Guid>, IAuditable
    {
        public User()
        {
            DomesticResponsibilityOccurances = new HashSet<DomesticResponsibilityOccurance>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public bool IsPasswordForChange { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
        public virtual ICollection<DomesticResponsibilityOccurance> DomesticResponsibilityOccurances { get; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
