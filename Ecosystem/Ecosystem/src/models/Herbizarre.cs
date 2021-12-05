﻿using System;
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

        public Herbizarre(ContentManager content, GraphicsDevice device, AnimalSex sex, int visionZoneRadius, int contactZoneRadius, float speed) : 
            base (sex, visionZoneRadius, contactZoneRadius, speed)
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
            Sprite.AddTexture("Pictures/herbizarre_0");
            Sprite.AddTexture("Pictures/herbizarre_1");
            Sprite.AddTexture("Pictures/herbizarre_2");

            base.Load();
        }
    }
}
