using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;


namespace Catalog.Entities
{
    public record billAcceptor
    {
        public int moneyId { get; init;}
        public string accept {get; init;}
        public string reject {get; init;}
    }
}