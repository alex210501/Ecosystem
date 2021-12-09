using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Ecosystem
{
    public enum AnimalSex
    {
        Male = 0,
        Female
    }

    public abstract class Animal : LifeForm
    {
        private static readonly float poopThreshold = 70;
        private int maximumChild = 12;
        private int minimumChild = 0;
        private int maximumPregnancyTime = 0;
        protected static readonly float poopDesirePerSecond = 10;

        private readonly AnimalSex sex;
        private int visionZoneRadius;
        private int contactZoneRadius;
        private bool isPregnant = false;
        private readonly float speed = 100; // Pixel per second
        private float poopDesire = 0;
        private int numberChild = 0;
        private float pregnancyTime = 0;
        private Vector2 destination;

        protected Animal(AnimalSex sex, int visionZoneRadius, int contactZoneRadius, float speed, Vector2 destination) :
            base()
        {
            this.sex = sex;
            this.visionZoneRadius = visionZoneRadius;
            this.contactZoneRadius = contactZoneRadius;
            this.speed = speed;
            this.destination = destination;
        }

        public AnimalSex Sex
        {
            get { return sex; }
        }

        public int MaximumChild
        {
            get { return maximumChild; }
            private set { maximumChild = value; }
        }

        public int MinimumChild
        {
            get { return minimumChild; }
            private set { minimumChild = value; }
        }

        public int MaximumPregnancyTime
        {
            get { return maximumPregnancyTime; }
            set { maximumPregnancyTime = value; }
        }

        public int VisionZoneRadius
        {
            get { return visionZoneRadius; }
        }

        public int ContactZoneRadius
        {
            get { return contactZoneRadius; }
        }

        public bool IsPregnant
        {
            get { return isPregnant; }
            set { isPregnant = value; }
        }

        public float Speed
        {
            get { return (speed * (Health / 100) * (Energy / 100)); }
        }

        public float PoopDesire
        {
            get { return poopDesire; }
            private set { poopDesire = value; }
        }

        public int NumberChild
        {
            get { return numberChild; }
            private set { numberChild = value; }
        }

        public float PregnancyTime
        {
            get { return pregnancyTime; }
            set { pregnancyTime = value; }
        }

        public float DestinationX
        {
            get { return destination.X; }
            set { destination.X = value; }
        }

        public float DestinationY
        {
            get { return destination.Y; }
            set { destination.Y = value; }
        }

        public void Reproduction(Animal animal)
        {
            if (Sex != animal.Sex)
            {
                Random random = new Random();
                int numberChild = random.Next(minimumChild, maximumChild);

                if (Sex == AnimalSex.Male) animal.SetPregnant(numberChild);
                else SetPregnant(numberChild);
            }
        }

        public bool CanGiveBirth()
        {
            if (isPregnant == false)
                return false;

            return (pregnancyTime >= maximumPregnancyTime);
        }

        public void SetPregnant(int numberChild)
        {
            if ((numberChild == 0) || IsPregnant)
                return;

            IsPregnant = true;
            pregnancyTime = 0;
            this.numberChild = numberChild;
        }

        public bool WantReproduce()
        {
            return (ReproductionDesire >= reproductionDesireThreshold);
        }

        public bool WantPoop()
        {
            return (poopDesire >= poopThreshold);
        }
        
        public void Walk()
        {
            WalkTo(destination.X, destination.Y);
        }

        public void WalkTo(float x , float y)
        {
            // Move X
            if (Sprite.PositionX > x)
                Sprite.PositionX -= (float)(0.01 * Speed);
            else if (Sprite.PositionX < x)
                Sprite.PositionX += (float)(0.01 * Speed);

            // Move Y
            if (Sprite.PositionY > y)
                Sprite.PositionY -= (float)(0.01 * Speed);
            else if (Sprite.PositionY < y)
                Sprite.PositionY += (float)(0.01 * Speed);
        }

        public bool IsDestinationReached()
        {
            return ((Sprite.PositionX == destination.X) && (Sprite.PositionY == destination.Y));
        }

        public bool IsInVisionZone(LifeForm lifeForm)
        {
            // If it's itself, return false
            if (lifeForm == this)
                return false;

            // First, check if the Lifeform is not to far away
            if ((lifeForm.Sprite.LeftEdge > (Sprite.RightEdge + visionZoneRadius)) || (lifeForm.Sprite.RightEdge < (Sprite.LeftEdge - visionZoneRadius)))
                return false;

            if ((lifeForm.Sprite.TopEdge > ( Sprite.BottompEdge + visionZoneRadius)) || (lifeForm.Sprite.BottompEdge < (Sprite.TopEdge - visionZoneRadius)))
                return false;
     
            return (Math.Pow(visionZoneRadius, 2) >= (Math.Pow(Sprite.PositionX - lifeForm.Sprite.PositionX, 2) + Math.Pow(Sprite.PositionY - lifeForm.Sprite.PositionY, 2)));
        }

        public bool IsInContactZone(LifeForm lifeForm)
        {
            // If it's itself, return false
            if (lifeForm == this)
                return false;

            // First, check if the Lifeform is not to far away
            if ((lifeForm.Sprite.LeftEdge > (Sprite.RightEdge + contactZoneRadius)) || (lifeForm.Sprite.RightEdge < (Sprite.LeftEdge - contactZoneRadius)))
                return false;

            if ((lifeForm.Sprite.TopEdge > (Sprite.BottompEdge + contactZoneRadius)) || (lifeForm.Sprite.BottompEdge < (Sprite.TopEdge - contactZoneRadius)))
                return false;

            return (Math.Pow(contactZoneRadius, 2) >= (Math.Pow(Sprite.PositionX - lifeForm.Sprite.PositionX, 2) + Math.Pow(Sprite.PositionY - lifeForm.Sprite.PositionY, 2)));
        }

        public override void Routine(GameTime gameTime)
        {
            float timeElapsed = (gameTime.ElapsedGameTime.Milliseconds / 1000);

            pregnancyTime += timeElapsed;
            PoopDesire += (poopDesirePerSecond * timeElapsed);
            base.Routine(gameTime);
        }
    }
}