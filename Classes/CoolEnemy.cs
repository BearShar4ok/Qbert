using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace QBert.Classes
{
    class CoolEnemy : IEnemy
    {
        public CoolEnemy()
        {
            position = CountPositionByIndex();
            sprite_width = 29;
            sprite_height = 30;
            spriteIndex = 1;
        }
        public override void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("coolEnemy1");
        }
        public override Vector2 CountPositionByIndex()
        {
            return new Vector2(Game1.cubes[indexY][indexX].Rect_top.X + 25, Game1.cubes[indexY][indexX].Rect_top.Y + 5);
        }
    }
}
