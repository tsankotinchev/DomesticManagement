using DomesticManagement.Common.Enum;

namespace DomesticManagement.Common.Dto
{
    public class FilterColumn
    {
        public string Table { get; set; }
        public string Name { get; set; }
        public FilterOperation Operation { get; set; }
        public string Value { get; set; }
        public FilterValueType ValueType { get; set; }
    }
}
