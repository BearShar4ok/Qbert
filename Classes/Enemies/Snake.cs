using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace QBert.Classes.Enemies
{
    class Snake : Enemy
    {
        private JumpManager snakeJump;

        public Snake()
        {
            position = new Vector2(Game1.Cells[indexY][indexX].Rect_top.X + 20, Game1.Cells[indexY][indexX].Rect_top.Y - 70);
            snakeJump = new JumpManager();
            textureName = "purpleSnake";
            indexX = 1;
            indexY = 2;
            sprite_width = 50;
            sprite_height = 100;
            spriteIndex = 5;
        }
        public override void Draw(SpriteBatch brush)
        {
            brush.Draw(texture, position, sourceRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
        public override void Update( GameTime gametime, Vector2 playerIndexes)
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
                    if (Game1.Cells[indexY][indexX].CellState == CellStates.air)
                    {
                        snakeJump.TimeToEnd = 1.2f;
                        snakeJump.UpdateTargetPosition(new Vector2(Game1.Cells[indexY][indexX].Rect_top.X + 20, 1300), position, JumpStates.inJump);
                        IsAlive = false;
                    }
                    else
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
