using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace WindowsFormsApp3
{
    class RGB
    {
        public byte[] R { set; get; }
        public byte[] G { set; get; }
        public byte[] B { set; get; }
        public RGB(byte[] r, byte[] g, byte[] b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

    }
}
