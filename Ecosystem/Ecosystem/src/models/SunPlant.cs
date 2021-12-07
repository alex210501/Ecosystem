using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Ecosystem
{
    public class SunPlant : Plants
    {
        private static int timePerFrame = 300;
        private static int widthFrame = 40;
        private static int heightFrame = 50;

        public SunPlant(ContentManager content, GraphicsDevice device, int rootZoneRadius, int seedingZoneRadius) : base(rootZoneRadius, seedingZoneRadius)
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

        public override void Load()
        {
            Sprite.AddTexture("Pictures/Plants/Plant");

            base.Load();
        }
    }
}
