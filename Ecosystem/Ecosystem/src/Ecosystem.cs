using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ecosystem
{
    public class Ecosystem : EcosystemFunction
    {
        public Ecosystem() : base() { }

        protected override void Initialize()
        {
            // Change the screen size
            Graphics.PreferredBackBufferWidth = ScreenWidth;
            Graphics.PreferredBackBufferHeight = ScreenHeight;
            Graphics.IsFullScreen = false;
            Graphics.ApplyChanges();

            GenerateLife(10, 10, 10);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

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

            foreach (Entity entity in Entities)
                entity.Sprite.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
