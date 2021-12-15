using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ecosystem
{
    public abstract class Entity
    {
        private Sprite sprite;

        public Entity() { }

        public Sprite Sprite
        {
            get { return sprite; }
            protected set { sprite = value; }
        }

        public virtual void Load()
        {
            if (sprite != null)
                sprite.Load();
        }

        public bool IsInZone(Entity entity, int radius)
        {
            // If it's itself, return false
            if (entity == this)
                return false;

            // First, check if the Lifeform is not to far away
            if ((entity.Sprite.LeftEdge > (Sprite.RightEdge + radius)) || (entity.Sprite.RightEdge < (Sprite.LeftEdge - radius)))
                return false;

            if ((entity.Sprite.TopEdge > (Sprite.BottompEdge + radius)) || (entity.Sprite.BottompEdge < (Sprite.TopEdge - radius)))
                return false;
            
            return (Math.Pow(radius, 2) >= (Math.Pow(Sprite.PositionX - entity.Sprite.PositionX, 2) + Math.Pow(Sprite.PositionY - entity.Sprite.PositionY, 2)));
        }

        public virtual void Routine(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }
    }
}
