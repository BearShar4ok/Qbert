﻿using System;
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
        private Rectangle rectangleOfPlayer;
        private KeyboardState prevState = new KeyboardState();
        private int indexX;
        private int indexY;
        private int spriteIndex = 7;
        private int sprite_width = 49;
        private int sprite_height = 50;
        private int screenHeight;
        private bool hasJumped = false;
        public Vector2 Position { get { return position; } set { position = value; } }
        public int IndexX { get { return indexX; } }
        public int IndexY { get { return indexY; } }
        public bool IsPlayerLive { get; private set; } = true;
        public JumpManager playerJump { get; private set; }

        public Player(Vector2 position, int indexX, int indexY, int screenHeight)
        {
            this.position = position;
            this.indexX = indexX;
            this.indexY = indexY;
            playerJump = new JumpManager();
            this.screenHeight = screenHeight;
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

            if (playerJump.NowJumpState == JumpStates.readyToJump)
            {
                if (indexX != 0 && indexY != 0 && indexX + indexY != 9 && Game1.cubes[IndexY - 1][IndexX - 1].Top_color_index != Game1.cubes[IndexY - 1][IndexX - 1].Top_colors.Count - 1 && hasJumped) Game1.cubes[IndexY - 1][IndexX - 1].ChangeTopColor(true);
                spriteIndex -= spriteIndex % 2;
            }


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
                    targetPosition = ChechFallTraecktory(false);
                    playerJump.UpdateTargetPosition(targetPosition, position, "Player");
                    if (!hasJumped) hasJumped = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Z))
                {
                    spriteIndex = 7;
                    indexY--;
                    targetPosition = ChechFallTraecktory(false);
                    playerJump.UpdateTargetPosition(targetPosition, position, "Player");
                    if (!hasJumped) hasJumped = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.C))
                {
                    spriteIndex = 5;
                    indexY--;
                    indexX++;
                    targetPosition = ChechFallTraecktory(true);
                    playerJump.UpdateTargetPosition(targetPosition, position, "Player");
                    if (!hasJumped) hasJumped = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.E))
                {
                    spriteIndex = 1;
                    indexY++;
                    targetPosition = ChechFallTraecktory(true);
                    playerJump.UpdateTargetPosition(targetPosition, position, "Player");
                    if (!hasJumped) hasJumped = true;
                }
            }
        }
        public void Draw(SpriteBatch brush)
        {
            brush.Draw(texture, position, rectangleOfPlayer, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
        private Vector2 ChechFallTraecktory(bool isRight)
        {
            int x = -100;
            if (isRight)
            {
                x = 100;
            }
            Vector2 targetPos;
            if (Game1.Cells[IndexY][IndexX].CellState != CellStates.air)
            {
                targetPos = new Vector2(Game1.Cells[IndexY][IndexX].Rect_top.X + 25, Game1.Cells[IndexY][IndexX].Rect_top.Y - 20);
            }
            else
            {
                playerJump.TimeToEnd = 1.2f;
                targetPos = new Vector2(Game1.Cells[IndexY][IndexX].Rect_top.X + x, screenHeight + texture.Height);
                IsPlayerLive = false;
            }
            return targetPos;
        }
    }
}