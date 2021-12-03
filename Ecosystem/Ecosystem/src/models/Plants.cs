using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosystem.src.models
{
    public class Plants : LifeForm, IEatable
    {
        private readonly int RootZoneRadius;//allow a value
        private readonly int SeedingZoneRadius;//allow a value
        /*we also should put a Health to the plants 
         *Same for the Energy 
         *Something that decrease much lower than the animal's health and energy
         *When a plant dies she becomes something (poop isn't a good choise) maybe Carbon or coil or whatever just
         *a thing usefull for the other plants
        */
        private float eatingEnergy = 0;
        private float satiationPoint = 0;
        public Plants(int RootZoneRadius, int SeedingZoneRadius) : base()
        {
            this.RootZoneRadius = RootZoneRadius;
            this.SeedingZoneRadius = SeedingZoneRadius;
        }
        public override void Eat(IEatable food)
        {
            //TODO: implement this class
            return;
        }

        public bool WantsExpends()
        {
            //TODO: implement this class
            return true;
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