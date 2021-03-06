using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Ecosystem
{
    public class Sprite
    {
        private ContentManager content;
        private GraphicsDevice device;
        private SpriteBatch spriteBatch;
        private float timePerFrame = 100;
        private float timeFrameElapsed = 0;
        private int totalFrame = 0;
        private int currentFrame = 0;
        private int frameWidth = 100;
        private int frameHeight = 100;
        private float positionX = 0;
        private float positionY = 0;
        private float scale = 1;
        private List<Texture2D> frame;

        public Sprite(ContentManager content, GraphicsDevice device, float timePerFrame, int frameWidth, int frameHeight)
        {
            this.content = content;
            this.device = device;
            this.timePerFrame = timePerFrame;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;

            frame = new List<Texture2D>();
        }

        public ContentManager Content
        {
            get { return content; }
        }

        public GraphicsDevice Device
        {
            get { return device; }
        }

        public int FrameWidth
        {
            get { return frameWidth; }
            protected set { frameWidth = value; }
        }

        public int FrameHeight
        {
            get { return frameHeight; }
            protected set { frameHeight = value; }
        }

        public float PositionX
        {
            get { return positionX + (frameWidth / 2); }
            set { positionX = value - (frameWidth / 2); }
        }

        public float PositionY
        {
            get { return positionY + (frameHeight / 2); }
            set { positionY = value - (frameHeight / 2); }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public int LeftEdge
        {
            get { return (int)(PositionX - frameWidth); }
        }

        public int RightEdge
        {
            get { return (int)(PositionX + frameWidth); }
        }

        public int TopEdge
        {
            get { return (int)(PositionY - frameHeight); }
        }

        public int BottompEdge
        {
            get { return (int)(PositionY + frameHeight); }
        }

        public void Load()
        {
            spriteBatch = new SpriteBatch(this.device);
        }

        public void AddTexture(string texturePath)
        {
            frame.Add(content.Load<Texture2D>(texturePath));
            totalFrame++;
        }

        public void Update(GameTime gameTime)
        {
            timeFrameElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timeFrameElapsed >= timePerFrame)
            {
                timeFrameElapsed = 0;
                currentFrame = (currentFrame == (totalFrame - 1)) ? 0 : (currentFrame + 1);
            }
        }

        public void Draw(GameTime GameTime)
        {
            if (totalFrame > 0)
            {
                Rectangle destination = new Rectangle((int)positionX, (int)positionY, (int)(frameWidth * scale), (int)(frameHeight * scale));
                Rectangle source = new Rectangle(0, 0, frame[currentFrame].Width, frame[currentFrame].Height);

                spriteBatch.Begin();
                spriteBatch.Draw(frame[currentFrame], destination, source, Color.White);
                spriteBatch.End();
            }
        }
    }
}
