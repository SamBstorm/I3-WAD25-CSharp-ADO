using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Ado_01
{
    internal class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
