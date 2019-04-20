using System;

namespace DomesticManagement.Common.Auditable
{
    public interface IAuditable
    {
        DateTime? CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }

        Guid CreatedBy { get; set; }

        Guid ModifiedBy { get; set; }
    }
}
