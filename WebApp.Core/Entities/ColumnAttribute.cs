using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Entities
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public string ColumnType { get; set; } = null;

        public ColumnAttribute(string value = null)
        {
            ColumnType = value;
        }
    }
}
