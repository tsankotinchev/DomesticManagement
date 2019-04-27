using DomesticManagement.Common.Enum;
using System.Collections.Generic;

namespace DomesticManagement.Common.Dto
{
    public class PageParametersDto
    {
        string _searchTerm = string.Empty;

        public PageParametersDto()
        {
            FilterColumns = new List<FilterColumn>();
        }

        public int Skip { get; set; }
        public int Take { get; set; }
        public string Sort { get; set; }
        public bool? FilterIsActive { get; set; }
        public string SearchTerm
        {
            get
            {
                return _searchTerm;
            }
            set
            {
                if (value?.Length > 50)
                {
                    value = value.Substring(0, 50);
                }

                _searchTerm = value;
            }
        }

        public string ParentTable { get; set; }
        public string ParentKey { get; set; }
        public string ParentValue { get; set; }
        public FilterOperation? ParentOperation { get; set; }
        public string ApiControlerRoute { get; set; }
        public string Token { get; set; }
        public List<FilterColumn> FilterColumns { get; set; }
    }
}
