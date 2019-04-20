using DomesticManagement.Common.Auditable;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomesticManagement.Database.Entities
{
    public class DomesticResponsibilityOccurance : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid DomesticResponsibilityId { get; set; }
        public DateTime OccuranceDateTime { get; set; }
        public string Note { get; set; }
        public virtual DomesticResponsibility DomesticResponsibility { get; set; }
        public virtual User User { get; set; }
    }
}
