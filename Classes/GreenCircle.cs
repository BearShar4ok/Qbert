using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace QBert.Classes
{
    class GreenCircle : IEnemy
    {
        public GreenCircle()
        {
            position = CountPositionByIndex();
            sprite_width = 49;
            sprite_height = 30;
            spriteIndex = 1;
        }
        public override void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("greenCircle");
        }
        public override void Draw(SpriteBatch brush)
        {
            brush.Draw(texture, position, sourceRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }

        public override Vector2 CountPositionByIndex()
        {
            return new Vector2(Game1.cubes[indexY][indexX].Rect_top.X + 20, Game1.cubes[indexY][indexX].Rect_top.Y + 5);
        }
    }
}
