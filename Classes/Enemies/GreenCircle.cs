﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace QBert.Classes.Enemies
{
    class GreenCircle : Enemy
    {
        public GreenCircle() : base()
        {
            position = CountPositionByIndex();
            sprite_width = 33;
            sprite_height = 20;
            spriteIndex = 1;
            textureName = "greenCircle";
        }
        public override void Draw(SpriteBatch brush)
        {
            brush.Draw(texture, position, sourceRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }

        public override Vector2 CountPositionByIndex()
        {
            return new Vector2(Game1.Cells[indexY][indexX].Rect_top.X + 35, Game1.Cells[indexY][indexX].Rect_top.Y + 20);
        }
    }
}
