using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace QBert.Classes
{
    public abstract class Enemy
    {
        protected Random random = new Random();
        protected Vector2 position;
        protected Texture2D texture;
        protected int indexX = 1;
        protected int indexY = 6;
        protected Rectangle sourceRectangle;
        protected int sprite_width;
        protected int sprite_height;
        protected int spriteIndex;
        protected int jumpTimer = 20;

        protected JumpManager circleJump = new JumpManager();

        public virtual int IndexX { get { return indexX; } }
        public virtual int IndexY { get { return indexY; } }

        public Enemy()
        {
            indexX = random.Next(1, 3);
        }
        public abstract void LoadContent(ContentManager manager);
        public virtual void Draw(SpriteBatch brush)
        {
            brush.Draw(texture, position, sourceRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
        public virtual void Update(GameTime gametime)
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
                    if (indexY == 1) return;
                    indexY--;
                    indexX += random.Next(0, 2);
                    circleJump.UpdateTargetPosition(CountPositionByIndex(), position);
                    spriteIndex = 1;
                    jumpTimer = 20;
                }
            }
            sourceRectangle = new Rectangle(sprite_width * spriteIndex, 0, sprite_width, sprite_height);
        }

        public virtual Vector2 CountPositionByIndex()
        {
            return Vector2.Zero;
        }
    }
}
