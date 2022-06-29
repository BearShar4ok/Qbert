using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace QBert.Classes
{
    class PurpleCircle : IEnemy
    {
        public PurpleCircle()
        {
            position = CountPositionByIndex();
            sprite_width = 39;
            sprite_height = 32;
            spriteIndex = 1;
        }
        public override void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("futureSnake");
        }

        public override Vector2 CountPositionByIndex()
        {
            return new Vector2(Game1.cubes[indexY][indexX].Rect_top.X + 25, Game1.cubes[indexY][indexX].Rect_top.Y + 5);
        }
    }
}
