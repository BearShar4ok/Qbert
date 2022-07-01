using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text;

namespace QBert.Classes.UI
{
    class Label
    {
        private SpriteFont spriteFont;
        private Color defaultColor;
        public Vector2 Position { get; set; }
        public string Text { get; set; }
        public Color Color { get; set; }

        public Label()
        {
            Position = new Vector2(0, 0);
            Text = "label";
            Color = Color.Orange;
            defaultColor = Color;
        }
        public Label(string text, Vector2 pos, Color color)
        {
            Position = pos;
            Text = text;
            Color = color;
            defaultColor = Color;
        }
        public void ResetColor()
        {
            Color = defaultColor;
        }
        public void LoadContent(ContentManager manager)
        {
            spriteFont = manager.Load<SpriteFont>("GameFont");
        }
        public void Draw(SpriteBatch brush)
        {
            brush.DrawString(spriteFont, Text, Position, Color);
        }
    }
}
