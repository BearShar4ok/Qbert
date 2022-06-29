using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace QBert.Classes
{
    class RedCircle : IEnemy
    {
        private Random random = new Random();
        private Vector2 position;
        private Texture2D texture;
        private int indexX = 0;
        private int indexY = 3;
        private Rectangle sourceRectangle;
        private int sprite_width = 44;
        private int sprite_height = 30;
        private int spriteIndex = 0;
        private int jumpTimer = 20;

        private JumpManager circleJump;

        public int IndexX { get { return indexX; } }
        public int IndexY { get { return indexY; } }
        public RedCircle()
        {
            position = new Vector2(Game1.cubes[indexY][indexX].Rect_top.X + 25, Game1.cubes[indexY][indexX].Rect_top.Y + 5);
            circleJump = new JumpManager();
        }
        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("redCircle");
        }
        public override void Draw(SpriteBatch brush)
        {
            brush.Draw(texture, position, sourceRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
        public override void Update(GameTime gametime)
        {
            if (circleJump != null && circleJump.NowJumpState == JumpStates.inJump)
            {
                circleJump.Update(gametime);
                position = circleJump.position;
            }

            if (circleJump.NowJumpState == JumpStates.readyToJump)
            {
                spriteIndex = 0;
                jumpTimer--;
                if (jumpTimer == 0)
                {
                    if (indexY == 0) return;
                    indexY--;
                    indexX += random.Next(0, 2);
                    circleJump.UpdateTargetPosition(new Vector2(Game1.cubes[indexY][indexX].Rect_top.X + 25, Game1.cubes[indexY][indexX].Rect_top.Y + 10), position);
                    spriteIndex = 1;
                    jumpTimer = 20;
                }
            }
            sourceRectangle = new Rectangle(sprite_width * spriteIndex, 0, sprite_width, sprite_height);
        }
    }
}
