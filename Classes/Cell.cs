using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using QBert.Classes.Enemies;

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

        public Action<object> ObjectStatechanged;

        public Rectangle Rect_top { get { return rect_top; } set { rect_top = value; } }

        public Cell(Rectangle rect_cube, Rectangle rect_top)
        {
            this.rect_top = rect_top;
            this.rect_cube = rect_cube;
            X = rect_cube.X;
            Y = rect_cube.Y;
            objectContains = null;
            ObjectStatechanged = (obj) => {
                CellState = CellStates.air;
                objectContains = obj;
                if (obj is Cube)
                {
                    CellState = CellStates.cube;
                }
                if (obj is Platform)
                {
                    CellState = CellStates.platform;
                }
                if (obj is Enemy)
                {
                    CellState = CellStates.enemy;
                }
                if (obj is GreenCircle)
                {
                    CellState = CellStates.greenCircle;
                }
                if (obj is CoolEnemy)
                {
                    CellState = CellStates.coolEnemy;
                }
                if (obj.ToString() == "cube")
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
