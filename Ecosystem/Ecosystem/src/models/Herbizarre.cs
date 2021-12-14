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

        public Herbizarre(ContentManager content, GraphicsDevice device, AnimalSex sex, int visionZoneRadius, int contactZoneRadius, float speed, Vector2 destination) : 
            base (sex, visionZoneRadius, contactZoneRadius, speed, destination)
        {
            this.Sprite = new Sprite(content, device, timePerFrame, widthFrame, heightFrame);
        }

        public static int TimePerFrame
        {
            get { return timePerFrame; }
        }

        public static int WidthFrame
        {
            get { return widthFrame; }
        }

        public static int HeightFrame
        {
            get { return heightFrame; }
        }

        public override bool CanReproduce(Animal animal)
        {
            return (animal is Herbizarre) && (base.CanReproduce(animal));
        }
        public override void Load()
        {
            Sprite.AddTexture("Pictures/Herbizarre/Herbizarre0");
            Sprite.AddTexture("Pictures/Herbizarre/Herbizarre1");
            Sprite.AddTexture("Pictures/Herbizarre/Herbizarre2");

            base.Load();
        }
    }
}
