using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace QBert.Classes
{
    public class Cube
    {
        private Texture2D texture_cube;
        private Texture2D texture_top;
        private Texture2D texture_right;
        private Texture2D texture_left;
        private Rectangle rect_top;
        private Rectangle rect_right;
        private Rectangle rect_left;
        private List<Color> top_colors;
        private Color left_color;
        private Color right_color;
        private int top_color_index = 0;

        public int Top_color_index { get { return top_color_index; } }
        public List<Color> Top_colors { get { return top_colors; } set { top_colors = value; } }
        public Color Left_color { get { return left_color; } set { left_color = value; } }
        public Color Right_color { get { return right_color; } set { right_color = value; } }
        public Rectangle Rect_top { get { return rect_top; } set { rect_top = value; } }

        public Cube(Rectangle rect_top, Rectangle rect_right, Rectangle rect_left)
        {
            this.rect_top = rect_top;
            this.rect_left = rect_left;
            this.rect_right = rect_right;
        }

        public void LoadContent(ContentManager Content)
        {
            texture_cube = Content.Load<Texture2D>("cube");
            texture_top = Content.Load<Texture2D>("top");
            texture_right = Content.Load<Texture2D>("right");
            texture_left = Content.Load<Texture2D>("left");
        }

        public void Draw(SpriteBatch brush)
        {
            //brush.Draw(texture_cube, rect_cube, Color.White);
            brush.Draw(texture_top, rect_top, top_colors[top_color_index]);
            brush.Draw(texture_left, rect_left, Left_color);
            brush.Draw(texture_right, rect_right, Right_color);
        }

        public void ChangeTopColor(bool toNext)
        {
            top_color_index = !toNext || top_color_index == 2 ? 0 : top_color_index + 1;
        }
    }
}
