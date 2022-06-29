using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace QBert.Classes
{
    class Snake : IEnemy
    {
        private Vector2 position;
        private Texture2D texture;
        private int indexX = 0;
        private int indexY = 1;
        private Rectangle sourceRectangle;
        private int sprite_width = 50;
        private int sprite_height = 100;
        private int spriteIndex = 6;

        public int IndexX { get { return indexX; } }
        public int IndexY { get { return indexY; } }

        public Snake()
        {
            position = new Vector2(Game1.cubes[indexY][indexX].Rect_top.X + 20, Game1.cubes[indexY][indexX].Rect_top.Y - 70);
        }
        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("purpleSnake");
        }
        public override void Draw(SpriteBatch brush)
        {
            brush.Draw(texture, position, sourceRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
        public override void Update()
        {
            position = new Vector2(Game1.cubes[indexY][indexX].Rect_top.X + 20, Game1.cubes[indexY][indexX].Rect_top.Y - 70);
            sourceRectangle = new Rectangle(sprite_width * spriteIndex, 0, sprite_width, sprite_height);
        }
        public override void MoveDown()
        {
            throw new NotImplementedException();
        }
        public void Jump()
        {

        }
    }
}
