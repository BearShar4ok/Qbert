using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace QBert.Classes
{
    public class Celle
    {
        private Rectangle rect_cube;
        private Rectangle rect_top;
        private Texture2D tempDebug;

        public int X { get; private set; }
        public int Y { get; private set; }

        public Rectangle Rect_top { get { return rect_top; } set { rect_top = value; } }

        public Celle(Rectangle rect_cube, Rectangle rect_top)
        {
            this.rect_top = rect_top;
            this.rect_cube = rect_cube;
            X = rect_cube.X;
            Y = rect_cube.Y;
        }
        public void LoadContent(ContentManager Content)
        {
            tempDebug = Content.Load<Texture2D>("cube");
        }
        public void Draw(SpriteBatch brush)
        {
            brush.Draw(tempDebug, rect_cube, Color.White);

        }
    }
}
