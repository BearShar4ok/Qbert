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
        private bool isRight;
        public bool IsDyingDown { get; private set; } = false;

        public Snake()
        {
            position = new Vector2(Game1.Cells[indexY][indexX].Rect_top.X + 20, Game1.Cells[indexY][indexX].Rect_top.Y - 70);
            enemyJump = new JumpManager();
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
        public override void Update(GameTime gametime, Vector2 playerIndexes)
        {
            if (enemyJump != null && enemyJump.NowJumpState == JumpStates.inJump)
            {
                enemyJump.Update(gametime);
                position = enemyJump.position;
            }

            if (hasJumped && enemyJump.NowJumpState == JumpStates.readyToJump)
            {
                hasJumped = false;
                Game1.Cells[indexY][indexX].objectStatechanged(this);
            }

            if (enemyJump.NowJumpState == JumpStates.readyToJump)
            {
                jumpTimer--;
                spriteIndex -= spriteIndex % 2;
                if (jumpTimer == 0 && !(playerIndexes.X == indexX && playerIndexes.Y == indexY))
                {
                    hasJumped = true;
                    Game1.Cells[indexY][indexX].objectStatechanged("cube");
                    Follow(playerIndexes);
                    if (Game1.Cells[indexY][indexX].CellState == CellStates.air || Game1.Cells[indexY][indexX].CellState == CellStates.platform)
                    {
                        IsAlive = false;
                        enemyJump.TimeToEnd = 1.2f;
                        int x = 100;
                        if (!isRight)
                            x = -100;
                        enemyJump.UpdateTargetPosition(new Vector2(Game1.Cells[indexY][indexX].Rect_top.X + 20 + x, 1300), position, JumpStates.inJump);
                        IsDyingDown = IsDyingDown;
                    }
                    else
                        enemyJump.UpdateTargetPosition(new Vector2(Game1.Cells[indexY][indexX].Rect_top.X + 20, Game1.Cells[indexY][indexX].Rect_top.Y - 70), position, JumpStates.inJump);
                    jumpTimer = 20;
                }
            }

            sourceRectangle = new Rectangle(sprite_width * spriteIndex, 0, sprite_width, sprite_height);
        }
        public void Follow(Vector2 playerIndexes)
        {
            if (indexX == (int)playerIndexes.X && indexY == (int)playerIndexes.Y) return;


            int n = (int)playerIndexes.X - indexX;
            int k = (int)playerIndexes.Y - indexY + n;

            int changeX;
            int changeY;

            if (Math.Abs(k) >= Math.Abs(n))
            {
                changeY = k > 0 ? 1 : -1;
                indexY += changeY;
            }
            else
            {
                changeX = n > 0 ? 1 : -1;
                changeY = n > 0 ? -1 : 1;

                isRight = changeX >= 0;
                indexX += changeX;
                indexY += changeY;
            }
            IsDyingDown = changeY < 0;
            if (Math.Abs(k) >= Math.Abs(n) && k > 0) spriteIndex = 1;
            if (Math.Abs(k) >= Math.Abs(n) && k <= 0) spriteIndex = 3;
            if (Math.Abs(k) < Math.Abs(n) && n > 0) spriteIndex = 5;
            if (Math.Abs(k) < Math.Abs(n) && n <= 0) spriteIndex = 7;
        }


    }
}
