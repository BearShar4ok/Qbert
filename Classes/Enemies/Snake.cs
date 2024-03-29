﻿using System;
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

        public Snake(int indexX, int indexY)
        {
            position = new Vector2(Game1.Cells[indexY][indexX].Rect_top.X + 20, Game1.Cells[indexY][indexX].Rect_top.Y - 70);
            enemyJumpManager = new JumpManager();
            textureName = "purpleSnake";
            sprite_width = 50;
            sprite_height = 100;
            spriteIndex = 5;
            this.indexX = indexX;
            this.indexY = indexY;
        }
        public override void Draw(SpriteBatch brush)
        {
            brush.Draw(texture, position, sourceRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
        public override void Update(GameTime gametime, Vector2 playerIndexes)
        {
            if (enemyJumpManager != null && enemyJumpManager.NowJumpState == JumpStates.inJump)
            {
                enemyJumpManager.Update(gametime);
                position = enemyJumpManager.position;
            }
            else if (enemyJumpManager.NowJumpState == JumpStates.readyToJump)
            {
                jumpTimer--;
                spriteIndex -= spriteIndex % 2;
                if (jumpTimer == 0 && !(playerIndexes.X == indexX && playerIndexes.Y == indexY))
                {
                    hasJumped = true;
                    Game1.Cells[indexY][indexX].ObjectStatechanged("cube");
                    Follow(playerIndexes);
                    if (Game1.Cells[indexY][indexX].CellState == CellStates.air || Game1.Cells[indexY][indexX].CellState == CellStates.platform)
                    {
                        IsAlive = false;
                        int x = 100;
                        if (!isRight)
                            x = -100;
                        enemyJumpManager.UpdateTargetPosition(new Vector2(Game1.Cells[indexY][indexX].Rect_top.X + 20 + x, 1300), position, JumpStates.inJump);
                        enemyJumpManager.TimeToEnd = 1.2f;
                        IsDyingDown = IsDyingDown;
                    }
                    else
                        enemyJumpManager.UpdateTargetPosition(new Vector2(Game1.Cells[indexY][indexX].Rect_top.X + 20, Game1.Cells[indexY][indexX].Rect_top.Y - 70), position, JumpStates.inJump);
                    jumpTimer = 30;
                }
            }
            if (enemyJumpManager.NowJumpState == JumpStates.readyToJump)
            {
                hasJumped = false;
                Game1.Cells[indexY][indexX].ObjectStatechanged(this);
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
