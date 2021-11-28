﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosystem.src.models
{
    public abstract class Herbivore : Animal
    {
        protected Herbivore(int visionZoneRadius, int contactZoneRadius, float speed) : base (visionZoneRadius, contactZoneRadius, speed) { }

        // TODO: Implement the Eat method when the Plant is created
        public override void Eat(IEatable food)
        {
            return;
        }

        // TODO: Implement the CanHeat method when the Plant is created
        public bool CanEat(IEatable food)
        {
            return false;
        }
    }
}
