using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace QBert.Classes
{
    public abstract class IEnemy
    {
        public abstract void Draw(SpriteBatch brush);
        public abstract void Update();
        public abstract void MoveDown();
    }
}
