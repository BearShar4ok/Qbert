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
        private Vector2 targetPosition;
        private Texture2D texture;
        private JumpManager playerJump;
        private Rectangle rectangleOfPlayer;
        private KeyboardState prevState = new KeyboardState();
        private int indexX;
        private int indexY;
        private int spriteIndex = 7;
        private int sprite_width = 49;
        private int sprite_height = 50;

        public Vector2 Position { get { return position; } set { position = value; } }
        public int IndexX { get { return indexX; } }
        public int IndexY { get { return indexY; } }

        public Player(Vector2 position, int indexX, int indexY)
        {
            this.position = position;
            this.indexX = indexX;
            this.indexY = indexY;
            playerJump = new JumpManager();
        }

        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("Qbert");
            rectangleOfPlayer = new Rectangle((int)position.X, (int)position.Y, sprite_width, sprite_height);
        }

        public void Update(GameTime gametime)
        {
            if (playerJump != null && playerJump.NowJumpState == JumpStates.inJump)
            {
                playerJump.Update(gametime);
                position = playerJump.position;
            }

            rectangleOfPlayer = new Rectangle(spriteIndex != 7 ? spriteIndex * sprite_width : 349, 0, spriteIndex != 1 ? sprite_width : 48, sprite_height);

            if (playerJump.NowJumpState == JumpStates.readyToJump) spriteIndex -= spriteIndex % 2;

            if (Keyboard.GetState() == prevState)
                return;
            else
            {
                prevState = Keyboard.GetState();
            }
            if (playerJump.NowJumpState == JumpStates.readyToJump)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Q))
                {
                    spriteIndex = 3;
                    indexY++;
                    indexX--;
                    targetPosition = new Vector2(Game1.cubes[IndexY][IndexX].Rect_top.X + 25, Game1.cubes[IndexY][IndexX].Rect_top.Y - 20);
                    playerJump.UpdateTargetPosition(targetPosition, position);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    spriteIndex = 7;
                    indexY--;
                    targetPosition = new Vector2(Game1.cubes[IndexY][IndexX].Rect_top.X + 25, Game1.cubes[IndexY][IndexX].Rect_top.Y - 20);
                    playerJump.UpdateTargetPosition(targetPosition, position);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    spriteIndex = 5;
                    indexY--;
                    indexX++;
                    targetPosition = new Vector2(Game1.cubes[IndexY][IndexX].Rect_top.X + 25, Game1.cubes[IndexY][IndexX].Rect_top.Y - 20);
                    playerJump.UpdateTargetPosition(targetPosition, position);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.E))
                {
                    spriteIndex = 1;
                    indexY++;
                    targetPosition = new Vector2(Game1.cubes[IndexY][IndexX].Rect_top.X + 25, Game1.cubes[IndexY][IndexX].Rect_top.Y - 20);
                    playerJump.UpdateTargetPosition(targetPosition, position);
                }
            }
        }
        public void Draw(SpriteBatch brush)
        {
            brush.Draw(texture, position, rectangleOfPlayer, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
    }
}