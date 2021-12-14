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
        List<Plants> Plants_to_add = new List<Plants>();

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private List<LifeForm> lifeFormList;
        private List<NonLifeForm> nonLifeFormList;

        public EcosystemFunction()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

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

                Carnivore carnivore = new Groudon(Content, GraphicsDevice, sex, 100, 50, 50f, 20f, destination);

                // Place the carnivores in the screen randomly
                carnivore.Sprite.PositionX = rnd.Next(carnivore.Sprite.FrameWidth, graphics.PreferredBackBufferWidth - carnivore.Sprite.FrameWidth);
                carnivore.Sprite.PositionY = rnd.Next(carnivore.Sprite.FrameHeight, graphics.PreferredBackBufferHeight - carnivore.Sprite.FrameHeight);
                carnivore.Sprite.Scale = 0.7f;

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

                lifeFormList.Add(herbivore);
            }

            for (int i = 0; i < plantsNumber; i++)
            {
                Plants plant = new SunPlant(Content, GraphicsDevice);

                // Place the herbivore in the screen randomly
                plant.Sprite.PositionX = rnd.Next(plant.Sprite.FrameWidth, graphics.PreferredBackBufferWidth - plant.Sprite.FrameWidth);
                plant.Sprite.PositionY = rnd.Next(plant.Sprite.FrameHeight, graphics.PreferredBackBufferHeight - plant.Sprite.FrameHeight);
                plant.Sprite.Scale = 0.7f;

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
            LifeFormList.RemoveAll(life => life.IsAlive == false);

            foreach (LifeForm lifeForm in LifeFormList)
            {

                if (lifeForm is Animal)
                {
                    Animal animal = (lifeForm as Animal);
                    List<LifeForm> visionList = new List<LifeForm>(lifeFormList.FindAll(animal.IsInVisionZone));
                    if (animal is Herbivore)
                        RunHerbivore(visionList, animal as Herbivore);
                    animal.Walk();
                }
                // Console.WriteLine(lifeForm.Sprite.PositionX1
                else
                {
                    Reproduction(lifeForm as Plants);
                }
                
                lifeForm.Routine(gameTime);

                //if (lifeForm is Animal)
                    //WalkRandom(lifeForm as Animal);
            }
            LifeFormList.AddRange(Plants_to_add);
            Plants_to_add.Clear();
        }

        public void RunHerbivore(List<LifeForm> visionList, Herbivore herbi)
        {
            List<LifeForm> PlantsInVision = new List<LifeForm>(visionList.FindAll(delegate(LifeForm visionLife) { return visionLife is Plants; }));
            List<LifeForm> PlantsInContact = new List<LifeForm>(PlantsInVision.FindAll(herbi.IsInContactZone));

            if (PlantsInVision.Count == 0)
                return;

            if (PlantsInContact.Count != 0)
            {
                herbi.Eat(PlantsInContact[0] as Plants);
                PlantsInContact[0].IsAlive = false;
                return;
            }
            else
            {
                    herbi.DestinationX = PlantsInVision[0].Sprite.PositionX;
                    herbi.DestinationY = PlantsInVision[0].Sprite.PositionY;
            }

        }

        public void Reproduction(Plants flowers)
        {
            if (flowers.WantsExpands())
            {
                Random rnd = new Random();
                int Seedingzoneradius = flowers.SEEDINGZoneRadius;
                //change hardcoded values
                int the_x_position = rnd.Next((int)flowers.Sprite.PositionX - Seedingzoneradius,(int)flowers.Sprite.PositionX  + Seedingzoneradius);
                int the_y_position = rnd.Next((int)flowers.Sprite.PositionY - Seedingzoneradius, (int)flowers.Sprite.PositionY + Seedingzoneradius);

                Plants plant = new SunPlant(Content, GraphicsDevice);

                plant.Sprite.PositionX = the_x_position;
                plant.Sprite.PositionY = the_y_position;
                plant.Sprite.Scale = 0.7f;
                plant.Load();
                Plants_to_add.Add(plant);
                flowers.Expands();
            }
        }

    }
}
