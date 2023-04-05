using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareData
{
    public  enum OperationType { Add = 1, Sub, Mult, Div }
    [Serializable]
    public  class Request
    {
        public double A { get; set; }
        public double B { get; set; }
        public OperationType OperationType { get; set; }
    }
}
