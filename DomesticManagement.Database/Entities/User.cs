using DomesticManagement.Common.Auditable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomesticManagement.Database.Entities
{
    public class User : Auditable
    {
        public User()
        {
            DomesticResponsibilityOccurances = new HashSet<DomesticResponsibilityOccurance>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<DomesticResponsibilityOccurance> DomesticResponsibilityOccurances { get; }
    }
}
