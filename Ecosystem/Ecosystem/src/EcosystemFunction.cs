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
        private List<LifeForm> lifeFormList;
        private List<NonLifeForm> nonLifeFormList;

        public EcosystemFunction()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            entities = new List<Entity>();
            lifeFormList = new List<LifeForm>();
            nonLifeFormList= new List<NonLifeForm>();
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

        protected List<LifeForm> LifeFormList
        {
            get { return lifeFormList; }
        }

        protected List<NonLifeForm> NonLifeFormList
        {
            get { return nonLifeFormList; }
        }

        // TODO: Check the collisions
        public void GenerateLife(int carnivoresNumber, int herbivoresNumber, int plantsNumber)
        {
            Random rnd = new Random();

            for (int i = 0; i < carnivoresNumber; i++)
            {
                AnimalSex sex = (AnimalSex)rnd.Next(0, 1);
                Vector2 destination = new Vector2(rnd.Next(0, ScreenWidth), rnd.Next(0, ScreenHeight));

                Carnivore carnivore = new Groudon(Content, GraphicsDevice, sex, 100, 40, 50f, 20f, destination);

                // Place the carnivores in the screen randomly
                carnivore.Sprite.PositionX = rnd.Next(carnivore.Sprite.FrameWidth, graphics.PreferredBackBufferWidth - carnivore.Sprite.FrameWidth);
                carnivore.Sprite.PositionY = rnd.Next(carnivore.Sprite.FrameHeight, graphics.PreferredBackBufferHeight - carnivore.Sprite.FrameHeight);
                carnivore.Sprite.Scale = 0.7f;

                entities.Add(carnivore);
                lifeFormList.Add(carnivore);
            }

            for (int i = 0; i < herbivoresNumber; i++)
            {
                AnimalSex sex = (AnimalSex)rnd.Next(0, 1);
                Vector2 destination = new Vector2(rnd.Next(0, ScreenWidth), rnd.Next(0, ScreenHeight));

                Herbivore herbivore= new Herbizarre(Content, GraphicsDevice, sex, 100, 30, 30f, destination);

                // Place the herbivore in the screen randomly
                herbivore.Sprite.PositionX = rnd.Next(herbivore.Sprite.FrameWidth, graphics.PreferredBackBufferWidth - herbivore.Sprite.FrameWidth);
                herbivore.Sprite.PositionY = rnd.Next(herbivore.Sprite.FrameHeight, graphics.PreferredBackBufferHeight - herbivore.Sprite.FrameHeight);
                herbivore.Sprite.Scale = 0.7f;

                entities.Add(herbivore);
                lifeFormList.Add(herbivore);
            }

            for (int i = 0; i < plantsNumber; i++)
            {
                Plants plant = new SunPlant(Content, GraphicsDevice, 100, 50);

                // Place the herbivore in the screen randomly
                plant.Sprite.PositionX = rnd.Next(plant.Sprite.FrameWidth, graphics.PreferredBackBufferWidth - plant.Sprite.FrameWidth);
                plant.Sprite.PositionY = rnd.Next(plant.Sprite.FrameHeight, graphics.PreferredBackBufferHeight - plant.Sprite.FrameHeight);
                plant.Sprite.Scale = 0.7f;

                entities.Add(plant);
                lifeFormList.Add(plant);
            }
        }

        public void WalkRandom(Animal animal)
        {
            if (animal.IsDestinationReached())
            {
                Random rnd = new Random();

                animal.DestinationX = rnd.Next(0, ScreenWidth);
                animal.DestinationY = rnd.Next(0, ScreenHeight );
            }

            animal.Walk();
        }
        
        public void Run(GameTime gameTime)
        {
            // entities.RemoveAll(life => (life as LifeForm).IsAlive == false);
            CheckEntityDeath();

            /*
            foreach (LifeForm lifeForm in LifeFormList)
            {

                if (lifeForm is Animal)
                {
                    Animal animal = (lifeForm as Animal);
                    List<Entity> visionList = new List<Entity>(lifeFormList.FindAll(animal.IsInVisionZone));
                    if (animal is Herbivore)
                        RunHerbivore(visionList, animal as Herbivore); 
                    animal.Walk();
                }
                // Console.WriteLine(lifeForm.Sprite.PositionX1

                lifeForm.Routine(gameTime);

                //if (lifeForm is Animal)
                    //WalkRandom(lifeForm as Animal);
            }
            */
            foreach (Entity entity in entities)
            {
                if (entity is LifeForm)
                {
                    LifeForm lifeForm = (entity as LifeForm);

                    if (lifeForm is Animal)
                    {
                        Animal animal = (entity as Animal);
                        List<Entity> visionList = new List<Entity>(entities.FindAll(animal.IsInVisionZone));

                        Eat(visionList, animal);
                        animal.Walk();
                    }

                    // Console.WriteLine(lifeForm.Sprite.PositionX1

                    lifeForm.Routine(gameTime);
                }

                //if (lifeForm is Animal)
                //WalkRandom(lifeForm as Animal);
            }
        }

        public void RunHerbivore(List<Entity> visionList, Herbivore herbi)
        {
            List<Entity> PlantsInVision = new List<Entity>(visionList.FindAll(delegate(Entity visionLife) { return herbi.CanEat(visionLife as IEatable); }));
            List<Entity> PlantsInContact = new List<Entity>(PlantsInVision.FindAll(herbi.IsInContactZone));

            if (PlantsInVision.Count == 0)
                return;

            if (PlantsInContact.Count != 0)
            {
                herbi.Eat(PlantsInContact[0] as Plants);
                (PlantsInContact[0] as LifeForm).IsAlive = false;
                return;
            }
            else
            {
                herbi.DestinationX = PlantsInVision[0].Sprite.PositionX;
                herbi.DestinationY = PlantsInVision[0].Sprite.PositionY;
            }
        }

        void Eat(List<Entity> visionList, Animal lifeForm)
        {
            List<Entity> eatableInVision = new List<Entity>(visionList.FindAll(delegate (Entity visionLife) { return lifeForm.CanEat(visionLife as IEatable); }));
            List<Entity> eatableInContact = new List<Entity>(eatableInVision.FindAll(lifeForm.IsInContactZone));

            if (eatableInContact.Count != 0)
            {
                lifeForm.Eat(eatableInContact[0] as IEatable);

                (eatableInContact[0] as IEatable).HasBeenEaten = true;
                if (eatableInContact[0] is LifeForm)
                    (eatableInContact[0] as LifeForm).IsAlive = false;
                else
                    (eatableInContact[0] as NonLifeForm).StillExists = false;
            }
            else if (eatableInVision.Count != 0)
            {
                (lifeForm as Animal).DestinationX = eatableInVision[0].Sprite.PositionX;
                (lifeForm as Animal).DestinationY = eatableInVision[0].Sprite.PositionY;
            }
            else if (lifeForm is Carnivore)
                Attack(lifeForm as Carnivore, visionList);
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
                    entityToRemove.Add(entity);
            }

            foreach (Entity entity in entityToRemove)
                entities.Remove(entity);
            entities.AddRange(entityToAdd);

            /*foreach (Entity entity in entities)
            {
                if (entity is Groudon)
                {
                    foreach (Entity eatable in entities)
                    {
                        if (eatable is Meat)
                            Console.WriteLine((entity as Groudon).CanEat(eatable as IEatable));
                    }
                }
            }*/
        }

        public NonLifeForm LifeFormToWaste(LifeForm lifeForm)
        {
            NonLifeForm waste = null;

            if (lifeForm is Animal)
                waste = new Meat(Content, GraphicsDevice, 100, 100);
            else
                waste = new OrganicWaste(Content, GraphicsDevice, 100, 100);

            waste.Sprite.PositionX = lifeForm.Sprite.PositionX;
            waste.Sprite.PositionY = lifeForm.Sprite.PositionY;
            waste.Load();

            return waste;
        }
    }
}
