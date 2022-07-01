using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace QBert.Classes
{
    public class Cell
    {
        private Rectangle rect_cube;
        private Rectangle rect_top;
        private Texture2D tempDebug;
        
        public object objectContains { get; private set; }
        public CellStates CellState { get; private set; } = CellStates.air;
        public int X { get; private set; }
        public int Y { get; private set; }

        public Action<object> objectStatechanged;

        public Rectangle Rect_top { get { return rect_top; } set { rect_top = value; } }

        public Cell(Rectangle rect_cube, Rectangle rect_top) : base()
        {
            this.rect_top = rect_top;
            this.rect_cube = rect_cube;
            X = rect_cube.X;
            Y = rect_cube.Y;
            objectContains = null;
            objectStatechanged = (obj) => { 
                objectContains = obj;
                if (obj is Cube)
                {
                    CellState = CellStates.cube;
                }
                
            }; 
        }
        public void LoadContent(ContentManager Content)
        {
            tempDebug = Content.Load<Texture2D>("cube");
        }
        public void Draw(SpriteBatch brush)
        {
            //brush.Draw(tempDebug, rect_cube, Color.White);

        }
    }
}
