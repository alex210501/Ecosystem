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
        private int minimumSpeed = 50;
        private int maximumPregnancyTime = 9; // 9 seconds
        private int positionErrorReached = 2;
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

        protected Animal(AnimalSex sex, int visionZoneRadius, int contactZoneRadius, float speed) :
            base()
        {
            this.sex = sex;
            this.visionZoneRadius = visionZoneRadius;
            this.contactZoneRadius = contactZoneRadius;
            this.speed = speed;
            this.destination = new Vector2(150, 150);
        }

        public AnimalSex Sex
        {
            get { return sex; }
        }

        public int MaximumChild
        {
            get { return maximumChild; }
            protected set { maximumChild = value; }
        }

        public int MinimumChild
        {
            get { return minimumChild; }
            protected set { minimumChild = value; }
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
            get { return (minimumSpeed + ((speed - minimumSpeed)/maxEnergy)*Energy); }
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

        public virtual bool CanReproduce(Animal animal)
        {
            return (Sex != animal.Sex) && (IsPregnant == false)  && (animal.IsPregnant == false);
        }

        public virtual void Reproduction(Animal animal)
        {
            if (CanReproduce(animal))
            {
                Random random = new Random();
                int numberChild = random.Next(minimumChild, maximumChild);

                if (Sex == AnimalSex.Male) animal.SetPregnant(numberChild);
                else SetPregnant(numberChild);

                ReproductionDesire = 0;
                animal.ReproductionDesire = 0;
            }
        }

        public bool CanGiveBirth()
        {
            if (isPregnant == false)
                return false;
            
            return (pregnancyTime >= maximumPregnancyTime);
        }

        public int GiveBirth()
        {
            ReproductionDesire = 0;
            isPregnant = false;
            pregnancyTime = 0;

            return numberChild;
        }

        public void SetPregnant(int numberChild)
        {
            if ((numberChild == 0) || IsPregnant)
                return;

            IsPregnant = true;
            pregnancyTime = 0;
            this.numberChild = numberChild;
        }

        public abstract Animal GetAnimalInstance();

        public bool WantReproduce()
        {
            return (ReproductionDesire >= reproductionDesireThreshold);
        }

        public bool WantPoop()
        {
            return (poopDesire >= poopThreshold);
        }

        public void Poop()
        {
            PoopDesire = 0;
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
            return ((Sprite.PositionX - positionErrorReached <= destination.X) && (Sprite.PositionX + positionErrorReached >= destination.X) 
                    && (Sprite.PositionY - positionErrorReached <= destination.Y) && (Sprite.PositionY + positionErrorReached >= destination.Y));

        }

        public bool IsInVisionZone(Entity entity)
        {
            return IsInZone(entity, visionZoneRadius);
        }

        public bool IsInContactZone(Entity entity)
        {
            return IsInZone(entity, contactZoneRadius);
        }

        public override void Routine(GameTime gameTime)
        {
            float timeElapsed = ((float)gameTime.ElapsedGameTime.Milliseconds / 1000);

            pregnancyTime += timeElapsed;
            PoopDesire += (poopDesirePerSecond * timeElapsed);
            base.Routine(gameTime);
        }
    }
}