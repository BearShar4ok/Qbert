using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace QBert.Classes
{
    class Platform
    {
        protected Random random = new Random();
        protected Vector2 position;
        protected Texture2D texture;
        protected int indexX = 0;
        protected int indexY = 5;
        protected Rectangle sourceRectangle;
        protected int sprite_width;
        protected int sprite_height;
        protected int spriteIndex;
        protected int jumpTimer = 20;
    }
}
