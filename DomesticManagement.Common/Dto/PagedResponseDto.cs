using System.Collections.Generic;

namespace DomesticManagement.Common.Dto
{
    public class PagedResponseDto<T>
    {
        public int Count { get; set; }
        public IList<T> Data { get; set; }
    }
}
