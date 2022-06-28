using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace QBert.Interfaces
{
    abstract class IEnemy
    {
        private Vector2 position;
        private Texture2D texture;
        private int indexX;
        private int indexY;
        private Rectangle rectangle;

        public int IndexX;
        public int IndexY;
        public abstract void LoadContent(ContentManager manager);
        public abstract void Update();
        public abstract void Draw(SpriteBatch bruah);
    }
}
