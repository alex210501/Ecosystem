using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Ecosystem
{
    public class OrganicWaste : NonLifeForm, IEatable
    {
        private int frameWidth = 20;
        private int frameHeight = 20;
        
        private float eatingEnergy = 0;
        private float satiationPoint = 0;
        private bool hasBeenEaten = false;

        public OrganicWaste(ContentManager content, GraphicsDevice device, float eatingEnergy, float satiationPoint) : base()
        {
            this.eatingEnergy = eatingEnergy;
            this.satiationPoint = satiationPoint;
            this.Sprite = new Sprite(content, device, 1000, frameWidth, frameHeight);
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

        public bool HasBeenEaten
        {
            get { return hasBeenEaten; }
            set { hasBeenEaten = value; }
        }

        public override void Load()
        {
            Sprite.AddTexture("Pictures/Poop/Poop0");

            base.Load();
        }
    }
}
