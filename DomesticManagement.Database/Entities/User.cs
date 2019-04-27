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
        public DateTime? CreatedOn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? ModifiedOn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Guid CreatedBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Guid ModifiedBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
