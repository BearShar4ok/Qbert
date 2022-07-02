using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace QBert.Classes.Enemies
{
    class Snake
    {
        private Vector2 position;
        private Texture2D texture;
        private int indexX = 1;
        private int indexY = 2;
        private Rectangle sourceRectangle;
        private int sprite_width = 50;
        private int sprite_height = 100;
        private int spriteIndex = 5;

        private int jumpTimer = 20;

        private JumpManager snakeJump;

        public int IndexX { get { return indexX; } }
        public int IndexY { get { return indexY; } }

        public Snake()
        {
            position = new Vector2(Game1.Cells[indexY][indexX].Rect_top.X + 20, Game1.Cells[indexY][indexX].Rect_top.Y - 70);
            snakeJump = new JumpManager();
        }
        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("purpleSnake");
        }
        public void Draw(SpriteBatch brush)
        {
            brush.Draw(texture, position, sourceRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
        public void Update(Vector2 playerIndexes, GameTime gametime)
        {
            if (snakeJump != null && snakeJump.NowJumpState == JumpStates.inJump)
            {
                snakeJump.Update(gametime);
                position = snakeJump.position;
            }

            if (snakeJump.NowJumpState == JumpStates.readyToJump)
            {
                jumpTimer--;
                spriteIndex -= spriteIndex % 2;
                if (jumpTimer == 0 && !(playerIndexes.X == indexX && playerIndexes.Y == indexY))
                {
                    Follow(playerIndexes);
                    snakeJump.UpdateTargetPosition(new Vector2(Game1.Cells[indexY][indexX].Rect_top.X + 20, Game1.Cells[indexY][indexX].Rect_top.Y - 70), position, JumpStates.inJump);
                    jumpTimer = 20;
                }
            }
            
            sourceRectangle = new Rectangle(sprite_width * spriteIndex, 0, sprite_width, sprite_height);
        }
        public void Follow(Vector2 playerIndexes)
        {
            /*if ((Math.Abs(indexY - playerIndexes.Y) == 1 && indexX == playerIndexes.X) || 
                (indexY - playerIndexes.Y == -1 && indexX - playerIndexes.X == 1) ||
                (indexY - playerIndexes.Y == 1 && indexX - playerIndexes.X == -1))
            {
                
                return;
            }*/

            if (indexX == (int)playerIndexes.X && indexY == (int)playerIndexes.Y) return;


            int n = (int)playerIndexes.X - indexX;
            int k = (int)playerIndexes.Y - indexY + n;

            if (Math.Abs(k) >= Math.Abs(n)) indexY += k > 0 ? 1 : -1;
            else
            {
                indexX += n > 0 ? 1 : -1;
                indexY += n > 0 ? -1 : 1;
            }

            if (Math.Abs(k) >= Math.Abs(n) && k > 0) spriteIndex = 1;
            if (Math.Abs(k) >= Math.Abs(n) && k <= 0) spriteIndex = 3;
            if (Math.Abs(k) < Math.Abs(n) && n > 0) spriteIndex = 5;
            if (Math.Abs(k) < Math.Abs(n) && n <= 0) spriteIndex = 7;
        }
    }
}
