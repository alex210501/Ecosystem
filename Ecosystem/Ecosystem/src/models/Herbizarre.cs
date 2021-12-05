using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Ecosystem
{
    public class Herbizarre : Herbivore
    {
        private static int timePerFrame = 100;
        private static int widthFrame = 40;
        private static int heightFrame = 30;

        private Sprite sprite;

        public Herbizarre(ContentManager content, GraphicsDevice device, AnimalSex sex, int visionZoneRadius, int contactZoneRadius, float speed) : 
            base (sex, visionZoneRadius, contactZoneRadius, speed)
        {
            this.sprite = new Sprite(content, device, timePerFrame, widthFrame, heightFrame);
        }

        public Sprite Sprite
        {
            get { return sprite; }
        }

        public void Load()
        {
            sprite.Load();

            sprite.AddTexture("Pictures/herbizarre_0");
            sprite.AddTexture("Pictures/herbizarre_1");
            sprite.AddTexture("Pictures/herbizarre_2");
        }
    }
}
