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
                Carnivore carnivore = new Groudon(Content, GraphicsDevice, sex, 100, 50, 100f, 20f);

                carnivore.Sprite.PositionX = rnd.Next(0, graphics.PreferredBackBufferWidth);
                carnivore.Sprite.PositionY = rnd.Next(0, graphics.PreferredBackBufferHeight);
                carnivore.Sprite.Scale = 0.7f;

                lifeFormList.Add(carnivore);
            }

            for (int i = 0; i < herbivoresNumber; i++)
            {
                AnimalSex sex = (AnimalSex)rnd.Next(0, 1);
                Herbivore herbivore= new Herbizarre(Content, GraphicsDevice, sex, 100, 50, 100f);

                herbivore.Sprite.PositionX = rnd.Next(0, graphics.PreferredBackBufferWidth);
                herbivore.Sprite.PositionY = rnd.Next(0, graphics.PreferredBackBufferHeight);
                herbivore.Sprite.Scale = 0.7f;

                lifeFormList.Add(herbivore);
            }
        }
    }
}
