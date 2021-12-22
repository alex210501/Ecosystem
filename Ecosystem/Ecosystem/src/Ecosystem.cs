using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ecosystem
{
    public class Ecosystem : EcosystemFunction
    {
        private Texture2D background;

        public Ecosystem() : base() { }

        protected override void Initialize()
        {
            // Change the screen size
            Graphics.PreferredBackBufferWidth = ScreenWidth;
            Graphics.PreferredBackBufferHeight = ScreenHeight;
            Graphics.IsFullScreen = false;
            Graphics.ApplyChanges();

            GenerateLife(5, 20, 5);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("Pictures/Background/Background");

            foreach (Entity entity in Entities)
                entity.Load();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Run(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Rectangle destination = new Rectangle(0, 0, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);
            Rectangle source = new Rectangle(0, 0, background.Width, background.Height);

            SpriteBatch.Begin();
            SpriteBatch.Draw(background, destination, source, Color.White);
            SpriteBatch.End();

            foreach (Entity entity in Entities)
                entity.Sprite.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
