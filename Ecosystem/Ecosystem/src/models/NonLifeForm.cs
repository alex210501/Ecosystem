using System;
using System.Collections.Generic;
using System.Text;

namespace Ecosystem
{
    public abstract class NonLifeForm : Entity
    {
        private bool stillExists = true;

        public NonLifeForm() : base() { }

        public bool StillExists
        {
            get { return stillExists; }
            set { stillExists = value; }
        }
    }
}
