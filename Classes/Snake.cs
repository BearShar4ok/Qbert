using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace QBert.Classes
{
    class Snake
    {
        private Vector2 position;
        private Texture2D texture;
        private int indexX = 0;
        private int indexY = 1;
        private Rectangle sourceRectangle;
        private int sprite_width = 50;
        private int sprite_height = 100;
        private int spriteIndex = 6;

        private int jumpTimer = 60;

        public int IndexX { get { return indexX; } }
        public int IndexY { get { return indexY; } }

        public Snake()
        {
            position = new Vector2(Game1.cubes[indexY][indexX].Rect_top.X + 20, Game1.cubes[indexY][indexX].Rect_top.Y - 70);
        }
        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("purpleSnake");
        }
        public void Draw(SpriteBatch brush)
        {
            brush.Draw(texture, position, sourceRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
        public void Update(Vector2 playerIndexes)
        {
            jumpTimer--;
            if (jumpTimer == 0 && !(playerIndexes.X == indexX && playerIndexes.Y == indexY))
            {
                Follow(playerIndexes);
                jumpTimer = 60;
            }
            position = new Vector2(Game1.cubes[indexY][indexX].Rect_top.X + 20, Game1.cubes[indexY][indexX].Rect_top.Y - 70);
            sourceRectangle = new Rectangle(sprite_width * spriteIndex, 0, sprite_width, sprite_height);
        }
        public void Follow(Vector2 playerIndexes)
        {
            if ((Math.Abs(indexY - playerIndexes.Y) == 1 && indexX == playerIndexes.X) || 
                (indexY - playerIndexes.Y == -1 && indexX - playerIndexes.X == 1) ||
                (indexY - playerIndexes.Y == 1 && indexX - playerIndexes.X == -1))
            {
                indexX = (int)playerIndexes.X;
                indexY = (int)playerIndexes.Y;
                return;
            }
            indexY += (playerIndexes.Y >= indexY) ? 1 : -1;
            indexX += (playerIndexes.Y == indexY) ? 0 : (playerIndexes.Y < indexY ? (indexX >= playerIndexes.X ? 0 : 1) : 0);
        }
    }
}
