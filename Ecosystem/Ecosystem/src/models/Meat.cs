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
        private readonly int frameWidth = 20;
        private readonly int frameHeight = 10;
        private readonly int maximumRotTime = 10;

        private float eatingEnergy = 0;
        private float satiationPoint = 0;
        private float timeToRot = 0;
        private bool hasBeenEaten = false;
        private bool isRot = false;
        
        public Meat(ContentManager content, GraphicsDevice device, float eatingEnergy, float satiationPoint) : base() 
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

        public bool IsRot
        {
            get { return isRot; }
        }

        public override void Load()
        {
            Sprite.AddTexture("Pictures/Meat/Meat0");

            base.Load();
        }

        public override void Routine(GameTime gameTime)
        {
            timeToRot += ((float)gameTime.ElapsedGameTime.Milliseconds / 1000);

            if (timeToRot >= maximumRotTime)
            {
                StillExists = false;
                isRot = true;
            }

            base.Routine(gameTime);
        }
    }
}
