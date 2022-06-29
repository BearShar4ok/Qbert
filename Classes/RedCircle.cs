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
        private int spriteIndex = 1;

        private int jumpTimer = 60;

        public int IndexX { get { return indexX; } }
        public int IndexY { get { return indexY; } }
        public RedCircle()
        {
            position = new Vector2(Game1.cubes[indexY][indexX].Rect_top.X + 25, Game1.cubes[indexY][indexX].Rect_top.Y + 5);
        }
        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("redCircle");
        }
        public override void Draw(SpriteBatch brush)
        {
            brush.Draw(texture, position, sourceRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
        public override void Update()
        {
            MoveDown();
            position = new Vector2(Game1.cubes[indexY][indexX].Rect_top.X + 25, Game1.cubes[indexY][indexX].Rect_top.Y + 10);
            sourceRectangle = new Rectangle(sprite_width * spriteIndex, 0, sprite_width, sprite_height);
        }
        public override void MoveDown()
        {
            if (indexY == 0) return;
            jumpTimer--;
            if (jumpTimer == 0)
            {
                indexY--;
                indexX += random.Next(0, 2);
                jumpTimer = 60;
            }
        }
    }
}
