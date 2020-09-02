using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class DataTableParameters
    {
        public ICollection<DataTableColumn> Columns { get; set; }
        public int Draw { get; set; }
        public int Length { get; set; }
        public ICollection<DataOrder> Order { get; set; }
        public Search Search { get; set; }
        public int Start { get; set; }
    }
    public class Search
    {
        public bool Regex { get; set; }
        public string Value { get; set; }
    }
    public class DataTableColumn
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public Search Search { get; set; }

    }
    public class DataOrder
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }
}