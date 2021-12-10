using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Ecosystem
{
    public class Meat : NonLifeForm, IEatable
    {
        private float eatingEnergy = 0;
        private float satiationPoint = 0;
        
        public Meat(ContentManager content, GraphicsDevice device, float eatingEnergy, float satiationPoint) : base() 
        {
            this.eatingEnergy = eatingEnergy;
            this.satiationPoint = satiationPoint;
            this.Sprite = new Sprite(content, device, 0, 0, 0);
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
