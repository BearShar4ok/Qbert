using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace QBert.Classes
{
    class Player
    {
        private Vector2 position;
        private Texture2D texture;
        private Rectangle rect;
        private int indexX;
        private int indexY;
        private KeyboardState prevState = new KeyboardState();

        public Vector2 Position { get { return position; } set { position = value; } }
        public int IndexX { get { return indexX; } }
        public int IndexY { get { return indexY; } }

        public Player(Vector2 position, int indexX, int indexY)
        {
            this.position = position;
            this.indexX = indexX;
            this.indexY = indexY;
        }

        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("Qbert");
            rect = new Rectangle((int)position.X, (int)position.Y, 50, 50);
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                indexY++;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                indexY--;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                indexY--;
                indexX++;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                indexY++;
                indexX++;
            }
            rect = new Rectangle((int)position.X, (int)position.Y, 50, 50);
        }

        public void Draw(SpriteBatch brush)
        {
            brush.Draw(texture, rect, Color.White);
        }
    }
}
