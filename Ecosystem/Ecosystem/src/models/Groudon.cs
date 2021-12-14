using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Ecosystem
{
    public class Groudon : Carnivore
    {
        private static int timePerFrame = 300;
        private static int widthFrame = 60;
        private static int heightFrame = 60;

        public Groudon(ContentManager content, GraphicsDevice device, AnimalSex sex, int visionZoneRadius, int contactZoneRadius, float speed, float damage, Vector2 destination) :
            base(sex, visionZoneRadius, contactZoneRadius, speed, damage, destination)
        {
            this.Sprite = new Sprite(content, device, timePerFrame, widthFrame, heightFrame);
            this.MaximumChild = 5;
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
            return (animal is Groudon) && (base.CanReproduce(animal));
        }

        public override void Load()
        {
            Sprite.AddTexture("Pictures/Groudon/Groudon0");
            Sprite.AddTexture("Pictures/Groudon/Groudon1");

            base.Load();
        }
    }
}
