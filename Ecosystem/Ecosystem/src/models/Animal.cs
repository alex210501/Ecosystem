using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosystem
{
    public enum AnimalSex
    {
        Male = 0,
        Female
    }

    // TODO: Think for IsInVisionZone(), IsInContactZone(), Walk()
    public abstract class Animal : LifeForm
    {
        private static readonly float poopThreshold = 70;
        private int maximumChild = 12;
        private int minimumChild = 0;
        private int maximumPregnancyTime = 0;

        private readonly AnimalSex sex;
        private int visionZoneRadius;
        private int contactZoneRadius;
        private bool isPregnant = false;
        private readonly float speed = 100; // Pixel per second
        private float hunger = 0;
        private float poopDesire = 0;
        private int numberChild = 0;
        private float pregnancyTime = 0;

        protected Animal(AnimalSex sex, int visionZoneRadius, int contactZoneRadius, float speed)
        {
            this.sex = sex;
            this.visionZoneRadius = visionZoneRadius;
            this.contactZoneRadius = contactZoneRadius;
            this.speed = speed;
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

        public float Hunger
        {
            get { return hunger; }
            protected set { hunger = value; }
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

        public bool IsHungry()
        {
            return (hunger >= hungerThreshold);
        }

        public bool WantReproduce()
        {
            return (ReproductionDesire >= reproductionDesireThreshold);
        }

        public bool WantPoop()
        {
            return (poopDesire >= poopThreshold);
        }
    }
}