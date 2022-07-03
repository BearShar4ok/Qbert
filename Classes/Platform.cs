using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace QBert.Classes
{
    class Platform : IDrawableOur
    {
        private Vector2 position;
        private Vector2 startPosition;
        private Vector2 targetPosition;
        private Texture2D texture;
        private int indexX = 0;
        private int indexY = 4;
        private Rectangle sourceRectangle;
        private int sprite_width = 47;
        private int sprite_height = 30;
        private int spriteIndex = 0;
        private float spinTimer = 100f;
        private float nowTimer = 0;
        private float maxMoveTime;

        public bool HasGone { get; set; } = false;
        public bool IsGoing { get; set; }
        public float MaxMoveTime { get { return maxMoveTime; } }

        public Platform(int indexX, int indexY)
        {
            this.indexX = indexX;
            this.indexY = indexY;
            maxMoveTime = (9 - indexY) * 0.9f;
            sourceRectangle = new Rectangle(spriteIndex <= 2 ? sprite_width * spriteIndex : sprite_width * spriteIndex + 1, 0, spriteIndex == 2 ? sprite_width + 1 : sprite_width, sprite_height);
            position = new Vector2(Game1.Cells[indexY][indexX].Rect_top.X + 20, Game1.Cells[indexY][indexX].Rect_top.Y + 20);
            startPosition = position;
            targetPosition = new Vector2(Game1.Cells[9][0].Rect_top.X + 20, Game1.Cells[9][0].Rect_top.Y + 20);
        }

        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("platform");
            position = new Vector2(Game1.Cells[indexY][indexX].Rect_top.X + 20, Game1.Cells[indexY][indexX].Rect_top.Y + 20);
            Game1.Cells[indexY][indexX].objectStatechanged(this);
        }

        public void Update(GameTime gameTime)
        {
            spinTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (spinTimer <= 0)
            {
                spinTimer = 100f;
                spriteIndex = (spriteIndex == 3) ? 0 : spriteIndex + 1;
            }
            sourceRectangle = new Rectangle((spriteIndex <= 2) ? sprite_width * spriteIndex : sprite_width * spriteIndex + 1, 0, (spriteIndex == 2) ? sprite_width + 1 : sprite_width, sprite_height);

            if (IsGoing)
            {
                MoveSlowly(gameTime);
                if (position.Y <= targetPosition.Y)
                {
                    IsGoing = false;
                    HasGone = true;
                    Game1.PlayerDroppedFromPlatform();
                }
            }
        }

        public void Draw(SpriteBatch brush)
        {
            brush.Draw(texture, position, sourceRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }

        private void MoveSlowly(GameTime gameTime)
        {
            nowTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X = startPosition.X + ((targetPosition.X - startPosition.X) * nowTimer / maxMoveTime);
            position.Y = startPosition.Y + ((targetPosition.Y - startPosition.Y) * nowTimer / maxMoveTime);
        }
    }
}
