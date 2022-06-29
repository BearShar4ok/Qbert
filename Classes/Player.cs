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
        private Vector2 newPosition;
        private Texture2D texture;
        private Rectangle rectangleOfPlayer;
        private int indexX;
        private int indexY;
        private int spriteIndex = 0;
        private int sprite_width = 50;
        private int sprite_height = 50;
        private KeyboardState prevState = new KeyboardState();

        private JumpManager playerJump;
        private bool isJumpStart = false;


        public Action<bool> JumpstateChanged;
        public Vector2 Position { get { return position; } set { position = value; } }
        public int IndexX { get { return indexX; } }
        public int IndexY { get { return indexY; } }

        public Player(Vector2 position, int indexX, int indexY)
        {
            this.position = position;
            this.indexX = indexX;
            this.indexY = indexY;
        }

        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("Qbert");
            rectangleOfPlayer = new Rectangle((int)position.X, (int)position.Y, sprite_width, sprite_height);
        }

        public void Update(GameTime gametime)
        {
            if (playerJump != null)
            {
                position = playerJump.position;
            }
            if (isJumpStart)
            {
                playerJump.Update(gametime);
            }


            rectangleOfPlayer = new Rectangle(spriteIndex * sprite_width, 0, sprite_width, sprite_height);

            if (Keyboard.GetState() == prevState)
                return;
            else
            {
                playerJump = new JumpManager(newPosition, position);

                isJumpStart = true;
                prevState = Keyboard.GetState();
            }
            if (true)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Q))
                {
                    indexY++;
                    indexX--;
                    newPosition = new Vector2(Game1.cubes[IndexY][IndexX].Rect_top.X + 25, Game1.cubes[IndexY][IndexX].Rect_top.Y - 20);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    indexY--;
                    newPosition = new Vector2(Game1.cubes[IndexY][IndexX].Rect_top.X + 25, Game1.cubes[IndexY][IndexX].Rect_top.Y - 20);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    indexY--;
                    indexX++;
                    newPosition = new Vector2(Game1.cubes[IndexY][IndexX].Rect_top.X + 25, Game1.cubes[IndexY][IndexX].Rect_top.Y - 20);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.E))
                {
                    indexY++;
                    newPosition = new Vector2(Game1.cubes[IndexY][IndexX].Rect_top.X + 25, Game1.cubes[IndexY][IndexX].Rect_top.Y - 20);
                }
            }
        }

        public void Draw(SpriteBatch brush)
        {
            brush.Draw(texture, position, rectangleOfPlayer, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
    }
}