using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosystem
{
    public class Meat : NonLifeForm, IEatable
    {
        private float eatingEnergy = 0;
        private float satiationPoint = 0;
        
        public Meat() : base() { }

        public float EatingEnergy
        {
            get { return eatingEnergy; }
            set { eatingEnergy = value; }
        }

        public float SatiationPoint
        {
            get { return satiationPoint; }
            set { satiationPoint = value; }
        }
    }
}
