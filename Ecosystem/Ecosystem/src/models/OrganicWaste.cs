using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosystem
{
    class OrganicWaste : NonLifeForm, IEatable
    {
        private float eatingEnergy = 0;
        private float satiationPoint = 0;

        public OrganicWaste(float eatingEnergy, float satiationPoint) : base()
        {
            this.eatingEnergy = eatingEnergy;
            this.satiationPoint = satiationPoint;
        }

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
