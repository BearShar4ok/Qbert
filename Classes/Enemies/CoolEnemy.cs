using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace QBert.Classes.Enemies
{
    class CoolEnemy : Enemy
    {
        public CoolEnemy() : base()
        {
            position = CountPositionByIndex();
            sprite_width = 29;
            sprite_height = 30;
            spriteIndex = 4;
            textureName = "coolEnemy1";
        }
        public override Vector2 CountPositionByIndex()
        {
            return new Vector2(Game1.Cells[indexY][indexX].Rect_top.X + 35, Game1.Cells[indexY][indexX].Rect_top.Y + 10);
        }

        public override void Update(GameTime gametime,Vector2 playerIndexes)
        {
            if (circleJump != null && circleJump.NowJumpState == JumpStates.inJump)
            {
                circleJump.Update(gametime);
                position = circleJump.position;

                if (circleJump.NowTime >= 0.13f && spriteIndex % 4 == 0) spriteIndex++;
            }

            if (circleJump.NowJumpState == JumpStates.readyToJump)
            {
                jumpTimer--;
                if (jumpTimer == 0)
                {
                    if (indexY == 1) return;
                    indexY--;
                    int direction = random.Next(0, 2);
                    spriteIndex = direction == 0 ? 3 : 5;
                    indexX += direction;
                    circleJump.UpdateTargetPosition(CountPositionByIndex(), position, JumpStates.inJump);
                    jumpTimer = 20;
                }
            }
            sourceRectangle = new Rectangle(sprite_width * spriteIndex, 0, sprite_width, sprite_height);
        }
    }
}
