using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ecosystem
{
    public class EcosystemFunction : Game
    {
        protected readonly int ScreenWidth = 1280;
        protected readonly int ScreenHeight = 700;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //TODO: Test for the moment
        private List<Entity> entities;
        private List<Entity> entitiesToAdd;

        public EcosystemFunction()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            entities = new List<Entity>();
            entitiesToAdd = new List<Entity>();
        }

        protected GraphicsDeviceManager Graphics
        {
            get { return graphics; }
        }

        protected SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            set { spriteBatch = value; }
        }

        protected List<Entity> Entities
        {
            get { return entities; }
        }

        // TODO: Check the collisions
        public void GenerateLife(int carnivoresNumber, int herbivoresNumber, int plantsNumber)
        {
            Random rnd = new Random();

            for (int i = 0; i < carnivoresNumber; i++)
            {
                AnimalSex sex = (AnimalSex)rnd.Next(0, 2);

                Carnivore carnivore = new Groudon(Content, GraphicsDevice, sex, 50f, 20f);

                // Place the carnivores in the screen randomly
                carnivore.DestinationX = rnd.Next(0, ScreenWidth);
                carnivore.DestinationY = rnd.Next(0, ScreenHeight);
                carnivore.Sprite.PositionX = rnd.Next(carnivore.Sprite.FrameWidth, graphics.PreferredBackBufferWidth - carnivore.Sprite.FrameWidth);
                carnivore.Sprite.PositionY = rnd.Next(carnivore.Sprite.FrameHeight, graphics.PreferredBackBufferHeight - carnivore.Sprite.FrameHeight);
                carnivore.Sprite.Scale = 0.7f;

                entities.Add(carnivore);
            }

            for (int i = 0; i < herbivoresNumber; i++)
            {
                AnimalSex sex = (AnimalSex)rnd.Next(0, 2);

                Herbivore herbivore= new Herbizarre(Content, GraphicsDevice, sex, 30f);

                // Place the herbivore in the screen randomly
                herbivore.DestinationX = rnd.Next(0, ScreenWidth);
                herbivore.DestinationY = rnd.Next(0, ScreenHeight);
                herbivore.Sprite.PositionX = rnd.Next(herbivore.Sprite.FrameWidth, graphics.PreferredBackBufferWidth - herbivore.Sprite.FrameWidth);
                herbivore.Sprite.PositionY = rnd.Next(herbivore.Sprite.FrameHeight, graphics.PreferredBackBufferHeight - herbivore.Sprite.FrameHeight);
                herbivore.Sprite.Scale = 0.7f;

                entities.Add(herbivore);
            }

            for (int i = 0; i < plantsNumber; i++)
            {
                Plants plant = new SunPlant(Content, GraphicsDevice);

                // Place the herbivore in the screen randomly
                plant.Sprite.PositionX = rnd.Next(plant.Sprite.FrameWidth, graphics.PreferredBackBufferWidth - plant.Sprite.FrameWidth);
                plant.Sprite.PositionY = rnd.Next(plant.Sprite.FrameHeight, graphics.PreferredBackBufferHeight - plant.Sprite.FrameHeight);
                plant.Sprite.Scale = 0.7f;

                entities.Add(plant);
            }
        }

        public void WalkRandom(Animal animal)
        {
            if (animal.IsDestinationReached())
            {
                Random rnd = new Random();

                animal.DestinationX = rnd.Next(0, ScreenWidth);
                animal.DestinationY = rnd.Next(0, ScreenHeight);
            }

            animal.Walk();
        }
        
        public void Run(GameTime gameTime)
        {
            CheckEntityDeath();

            foreach (Entity entity in entities)
            {
                if (entity is LifeForm)
                {
                    LifeForm lifeForm = (entity as LifeForm);

                    if (lifeForm is Animal)
                    {
                        Animal animal = (entity as Animal);
                        List<Entity> visionList = new List<Entity>(entities.FindAll(animal.IsInVisionZone));

                        EatAnimal(visionList, animal);
                        Reproduction(visionList, animal);
                        GiveBirth(animal);
                        Poop(animal);
                        WalkRandom(animal);

                    }
                    else
                    {
                        Plants plant = (entity as Plants);

                        List<Entity> rootList = new List<Entity>(entities.FindAll(plant.IsInRootZone));
                        EatPlants(rootList, plant);
                        PlantExpands(plant);
                    }
                }

                entity.Routine(gameTime);
            }

            entities.AddRange(entitiesToAdd);
            entitiesToAdd.Clear();

        }

        void EatAnimal(List<Entity> visionList, Animal lifeForm)
        {
            if (lifeForm.IsHungry() == false)
                return;

            List<Entity> eatableInVision = new List<Entity>(visionList.FindAll(delegate (Entity visionLife) { return lifeForm.CanEat(visionLife as IEatable); }));
            List<Entity> eatableInContact = new List<Entity>(eatableInVision.FindAll(lifeForm.IsInContactZone));

            if (eatableInContact.Count != 0)
                Eat(lifeForm, eatableInContact[0] as IEatable);
            else if (eatableInVision.Count != 0)
            {
                lifeForm.DestinationX = eatableInVision[0].Sprite.PositionX;
                lifeForm.DestinationY = eatableInVision[0].Sprite.PositionY;
            }
            else if (lifeForm is Carnivore)
                Attack(lifeForm as Carnivore, visionList);
        }

        void EatPlants(List<Entity> rootList, Plants plant)
        {
            if (plant.IsHungry() == false)
                return;

            List<Entity> eatList = new List<Entity>(rootList.FindAll(delegate (Entity entity) { return plant.CanEat(entity as IEatable); }));

            if (eatList.Count != 0)
                Eat(plant, eatList[0] as IEatable);
        }

        void Eat(LifeForm eater, IEatable food)
        {
            eater.Eat(food);
            food.HasBeenEaten = true;
            if (food is LifeForm)
                (food as LifeForm).IsAlive = false;
            else 
                (food as NonLifeForm).StillExists = false;
        }

        void Attack(Carnivore carnivore, List<Entity> visionList)
        {
            // Get all the Animal that the carnivore can attack
            List<Entity> attackList = new List<Entity>(visionList.FindAll(delegate (Entity entity) { if (entity is Animal) return carnivore.CanAttack(entity as Animal); else return false; }));
            List<Entity> attackContactList = new List<Entity>(attackList.FindAll(carnivore.IsInContactZone));

            if (attackList.Count == 0)
                return;

            // Check if the carnivora can directly attack, or run after its prey
            if (attackContactList.Count != 0)
                carnivore.Attack(attackContactList[0] as Animal);
            else
            {
                carnivore.DestinationX = attackList[0].Sprite.PositionX;
                carnivore.DestinationY = attackList[0].Sprite.PositionY;
            }
        }

        void CheckEntityDeath()
        {
            List<Entity> entityToRemove = new List<Entity>();
            List<Entity> entityToAdd = new List<Entity>();

            foreach (Entity entity in entities)
            {
                // Transform a death Animal into meat
                if ((entity is LifeForm) && ((entity as LifeForm).IsAlive == false))
                {
                    if (((entity is Plants) && ((entity as Plants).HasBeenEaten)) == false)
                        entityToAdd.Add(LifeFormToWaste(entity as LifeForm));
                    entityToRemove.Add(entity);
                }
                // Delete the meat which has been eat
                else if ((entity is NonLifeForm) && ((entity as NonLifeForm).StillExists == false))
                {
                    // Convert Meat to OrganicWaste if rot
                    if ((entity is Meat) && (entity as Meat).IsRot)
                        entityToAdd.Add(MeatToWaste(entity as Meat));
                    entityToRemove.Add(entity);
                }
            }

            foreach (Entity entity in entityToRemove)
                entities.Remove(entity);
            entities.AddRange(entityToAdd);
        }

        public NonLifeForm LifeFormToWaste(LifeForm lifeForm)
        {
            NonLifeForm waste;

            if (lifeForm is Animal)
                waste = new Meat(Content, GraphicsDevice, 100, 100);
            else
                waste = new OrganicWaste(Content, GraphicsDevice, 100, 100);

            waste.Sprite.PositionX = lifeForm.Sprite.PositionX;
            waste.Sprite.PositionY = lifeForm.Sprite.PositionY;
            waste.Load();

            return waste;
        }

        public OrganicWaste MeatToWaste(Meat meat)
        {
            OrganicWaste waste = new OrganicWaste(Content, GraphicsDevice, 100, 100);

            waste.Sprite.PositionX = meat.Sprite.PositionX;
            waste.Sprite.PositionY = meat.Sprite.PositionY;
            waste.Load();

            return waste;
        }

        public void PlantExpands(Plants flowers)
        {
            if (flowers.WantsExpands())
            {
                Random rnd = new Random();
                int Seedingzoneradius = flowers.SEEDINGZoneRadius;
                //change hardcoded values
                int the_x_position = rnd.Next((int)flowers.Sprite.PositionX - Seedingzoneradius,(int)flowers.Sprite.PositionX  + Seedingzoneradius);
                int the_y_position = rnd.Next((int)flowers.Sprite.PositionY - Seedingzoneradius, (int)flowers.Sprite.PositionY + Seedingzoneradius);

                Plants plant = new SunPlant(Content, GraphicsDevice);


                flowers.Expands();
                plant.Sprite.PositionX = the_x_position;
                plant.Sprite.PositionY = the_y_position;
                plant.Sprite.Scale = 0.7f;
                plant.Load();
                entitiesToAdd.Add(plant);
            }
        }

        public void Reproduction(List<Entity> visionZone, Animal animal)
        {
            if (animal.WantReproduce() == false)
                return;

            List<Entity> canReproduce = new List<Entity>(visionZone.FindAll(delegate (Entity entity) { return (entity is Animal) && animal.CanReproduce(entity as Animal); }));
            List<Entity> canReproduceContact = new List<Entity>(canReproduce.FindAll(animal.IsInVisionZone));

            if (canReproduce.Count == 0)
                return;

            if (canReproduceContact.Count > 0)
                animal.Reproduction(canReproduceContact[0] as Animal);
            else
            {
                animal.DestinationX = canReproduce[0].Sprite.PositionX;
                animal.DestinationY = canReproduce[0].Sprite.PositionY;
            }
        }

        public void GiveBirth(Animal animal)
        {
            if (animal.CanGiveBirth() == false)
                return;

            Random rnd = new Random();
            int childNumber = animal.GiveBirht();

            for (int i = 0; i < childNumber; i++)
            {
                Animal animalInstance = animal.GetAnimalInstance();

                animalInstance.Sprite.PositionX = (float)rnd.Next((int)animal.Sprite.PositionX - animal.VisionZoneRadius,
                                                   (int)animal.Sprite.PositionX + animal.VisionZoneRadius);
                animalInstance.Sprite.PositionY = (float)rnd.Next((int)animal.Sprite.PositionY - animal.VisionZoneRadius,
                                                   (int)animal.Sprite.PositionY + animal.VisionZoneRadius);
                animalInstance.DestinationX = rnd.Next(0, ScreenWidth);
                animalInstance.DestinationY = rnd.Next(0, ScreenHeight);
                animalInstance.Sprite.Scale = 0.7f;
                animalInstance.Load();

                entitiesToAdd.Add(animalInstance);
            }
        }

        public void Poop(Animal animal)
        {
            NonLifeForm waste;
            if (animal.WantPoop())
            {
                waste = new OrganicWaste(Content, GraphicsDevice, 100, 100);

                waste.Sprite.PositionX = animal.Sprite.PositionX;
                waste.Sprite.PositionY = animal.Sprite.PositionY;
                waste.Load();
                animal.Poop();
                entitiesToAdd.Add(waste);
            }
        }
    }
}
