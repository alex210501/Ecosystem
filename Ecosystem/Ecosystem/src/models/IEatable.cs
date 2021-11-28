using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosystem
{
    public interface IEatable
    {
        public float EatingEnergy { get; set; }
        public float SatiationPoint { get; set; }
    }
}
