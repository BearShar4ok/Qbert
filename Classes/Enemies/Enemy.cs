using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace QBert.Classes.Enemies
{
    public abstract class Enemy : IDrawableOur
    {
        protected Random random = new Random();
        protected Vector2 position;
        protected Texture2D texture;
        protected int indexX = 1;
        protected int prevIndexX;
        protected int indexY = 6;
        protected Rectangle sourceRectangle;
        protected int sprite_width;
        protected int sprite_height;
        protected int spriteIndex;
        protected int jumpTimer = 20;
        protected string textureName;
        protected bool hasJumped = false;
        public bool IsAlive { get;  set; } = true;

        public JumpManager enemyJumpManager { get; set; } = new JumpManager();

        public virtual int IndexX { get { return indexX; } }
        public virtual int IndexY { get { return indexY; } }
        public virtual int PrevIndexX { get { return prevIndexX; } }

        public Enemy()
        {
            SpawnEnemy();
        }
        public virtual void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>(textureName);
        }
        public virtual void Draw(SpriteBatch brush)
        {
            brush.Draw(texture, position, sourceRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
        public virtual void Update(GameTime gametime, Vector2 playerIndexes = default)
        {
            if (enemyJumpManager != null && enemyJumpManager.NowJumpState == JumpStates.inJump)
            {
                enemyJumpManager.Update(gametime);
                position = enemyJumpManager.position;
            }
            if (enemyJumpManager.NowJumpState == JumpStates.freeFall)
            {
                enemyJumpManager.Update(gametime);
                position = enemyJumpManager.position;
            }

            if (hasJumped && enemyJumpManager.NowJumpState == JumpStates.readyToJump)
            {
                hasJumped = false;
                Game1.Cells[indexY][indexX].objectStatechanged(this);
            }

           
            if (enemyJumpManager.NowJumpState == JumpStates.readyToJump)
            {
                spriteIndex = 0;
                jumpTimer--;
                if (jumpTimer == 0)
                {
                    hasJumped = true;
                    Game1.Cells[indexY][indexX].objectStatechanged("cube");
                    indexY--;
                    if (indexY < 0 && enemyJumpManager.NowJumpState == JumpStates.readyToJump)
                    {
                        IsAlive = false;
                        return;
                    }
                    else
                    {
                        prevIndexX = indexX;
                        indexX += random.Next(0, 2);
                        if (indexY == 0 && this is PurpleCircle)
                        {
                            (this as PurpleCircle).HasReachedBottom = true;
                            return;
                        }
                        else if (indexY == 0 && !(this is PurpleCircle))
                        {
                            enemyJumpManager.TimeToEnd = 0.8f;
                            enemyJumpManager.UpdateTargetPosition(new Vector2(Game1.Cells[IndexY][IndexX].Rect_top.X, 1080 + texture.Height), position, JumpStates.inJump);
                            spriteIndex = 1;
                            jumpTimer = 20;
                        }
                        else
                        {
                            enemyJumpManager.UpdateTargetPosition(CountPositionByIndex(), position, JumpStates.inJump);
                            spriteIndex = 1;
                            jumpTimer = 20;
                        }
                    }
                    Game1.Cells[indexY][indexX].objectStatechanged(this);
                }
            }
            sourceRectangle = new Rectangle(sprite_width * spriteIndex, 0, sprite_width, sprite_height);
        }
        public virtual void SpawnEnemy()
        {
            indexX = random.Next(1, 3);
            Vector2 tpos = CountPositionByIndex();
            Vector2 npos = new Vector2(CountPositionByIndex().X, -200);
            //enemyJumpManager.TimeToEnd = 1f;
            enemyJumpManager.UpdateTargetPosition(tpos, npos, JumpStates.freeFall);
        }
        public virtual Vector2 CountPositionByIndex()
        {
            return Vector2.Zero;
        }
    }
}
