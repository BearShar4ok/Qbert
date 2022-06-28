using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace QBert.Classes
{
    class Player
    {
        private Vector2 position;
        private Texture2D texture;
        private Rectangle rectangleOfPlayer;
        private int indexX;
        private int indexY;
        private KeyboardState prevState = new KeyboardState();

        private int startSpeed;

        private bool isRight;

        private int horizontalSpeed;
        private int verticallSpeed;
        private const float g = 25f;
        private float t1;
        private Vector2 targetPosition;
        private Thread jumpThread;

        public Vector2 Position { get { return position; } set { position = value; } }
        public int IndexX { get { return indexX; } }
        public int IndexY { get { return indexY; } }

        public Player(Vector2 position, int indexX, int indexY)
        {
            this.position = position;
            this.indexX = indexX;
            this.indexY = indexY;


            horizontalSpeed = 10;
            verticallSpeed = 2;
            texture = null;
            t1 = 0;
            isRight = true;
            startSpeed = 30;
        }

        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("Qbert");
            rectangleOfPlayer = new Rectangle((int)position.X, (int)position.Y, 50, 50);
        }

        public void Update(Vector2 newPosition)
        {
            t1 += 0.1f;
            if (newPosition != targetPosition)
            {
                targetPosition = newPosition;

                //jumpThread = new Thread(Jump);
                //jumpThread.Start();
            }
            if (position != targetPosition)
            {
                Jump();
            }
            rectangleOfPlayer = new Rectangle((int)position.X, (int)position.Y, 50, 50);

            if (Keyboard.GetState() == prevState)
                return;
            else
                prevState = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                indexY++;
                indexX--;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                indexY--;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                indexY--;
                indexX++;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                indexY++;
            }





        }

        public void Draw(SpriteBatch brush)
        {
            brush.Draw(texture, rectangleOfPlayer, Color.White);
        }

        private void Jump()
        {
            position.X += horizontalSpeed;
            if (!ZoroSpeed(t1))
            {
                position.Y += YJump(t1);
            }
            else
            {
                position.Y -= YFall();
            }
            //this.position = new_position;
        }
        private void Fall()
        {
            position.Y += YFall();
        }
        public bool ZoroSpeed(float t)
        {
            if (startSpeed - (g * t) <= 0)
            {
                return true;
            }
            return false;
        }
        public float YJump(float t)
        {

            if (t <= 0)
            {
                return 0;
            }
            return ((startSpeed * t) - (g * t * t / 2)) - ((startSpeed * (t - 0.1f)) - (g * (t - 0.1f) * (t - 0.1f) / 2));
        }
        private float YFall()
        {
            if (t1 <= 0)
            {
                return 0;
            }
            if (t1 > 6)
            {
                t1 = 6;
            }
            return ((g * t1 * t1 / 2)) - ((g * (t1 - 0.1f) * (t1 - 0.1f) / 2));
        }
    }
}