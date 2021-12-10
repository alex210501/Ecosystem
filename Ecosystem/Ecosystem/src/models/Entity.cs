using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
