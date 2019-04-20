using DomesticManagement.Common.Auditable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomesticManagement.Database.Entities
{
    public class DomesticResponsibility : Auditable
    {
        public DomesticResponsibility()
        {
            DomesticResponsibilityOccurances = new HashSet<DomesticResponsibilityOccurance>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string ResponsibilityName { get; set; }
        public string ResponsibilityDescription { get; set; }
        public virtual ICollection<DomesticResponsibilityOccurance> DomesticResponsibilityOccurances { get; }
    }
}
