using System;

namespace DomesticManagement.Common.Auditable
{
    public class Auditable : IAuditable
    {
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }

    }
}
