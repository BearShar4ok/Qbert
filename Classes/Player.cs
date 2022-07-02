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
        private int score = 0;

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
        private bool hasChangedCubeColor = false;
        private float nowTimer = 0;
        private float maxMoveTime;
        private Vector2 startPosition;
        private Vector2 topPosition = new Vector2(959, 253);

        public Vector2 Position { get { return position; } set { position = value; } }
        public int Score { get { return score; } set { score = value; } }
        public int IndexX { get { return indexX; } set { indexX = value; } }
        public int IndexY { get { return indexY; } set { indexY = value; } }
        public float MaxMoveTime { get { return maxMoveTime; } set { maxMoveTime = value; } }
        public bool IsPlayerLive { get; private set; } = true;
        public PlayerStates PlayerState { get; set; } = PlayerStates.notOnPlatform;
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
                if (indexX != 0 && indexY != 0 && indexX + indexY != 9 && Game1.cubes[IndexY - 1][IndexX - 1].Top_color_index != Game1.cubes[IndexY - 1][IndexX - 1].Top_colors.Count - 1 && hasJumped && !hasChangedCubeColor)
                {
                    Game1.cubes[IndexY - 1][IndexX - 1].ChangeTopColor(true);
                    score += 25;
                    hasChangedCubeColor = true;
                }
                spriteIndex -= spriteIndex % 2;
            }

            if (Game1.Cells[IndexY][IndexX].CellState == CellStates.platform && playerJump.NowJumpState == JumpStates.readyToJump && PlayerState != PlayerStates.onPlatform)
            {
                Game1.PlayerSteppedOnPlatform();
                //Game1.Cells[IndexY][IndexX].objectStatechanged(7632);
                startPosition = position;
                PlayerState = PlayerStates.onPlatform;
            }

            if (PlayerState == PlayerStates.onPlatform && position.Y > topPosition.Y) MoveSlowly(gametime);


            if (Keyboard.GetState() == prevState)
                return;
            else
            {
                prevState = Keyboard.GetState();
            }
            if (playerJump.NowJumpState == JumpStates.readyToJump && PlayerState == PlayerStates.notOnPlatform)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Q))
                {
                    hasChangedCubeColor = false;
                    spriteIndex = 3;
                    indexY++;
                    indexX--;
                    playerJump.NowJumpState = JumpStates.inJump;
                    targetPosition = ChechFallTraecktory(false);
                    playerJump.UpdateTargetPosition(targetPosition, position, "Player");
                    if (!hasJumped) hasJumped = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Z))
                {
                    hasChangedCubeColor = false;
                    spriteIndex = 7;
                    indexY--;
                    playerJump.NowJumpState = JumpStates.inJump;
                    targetPosition = ChechFallTraecktory(false);
                    playerJump.UpdateTargetPosition(targetPosition, position, "Player");
                    if (!hasJumped) hasJumped = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.C))
                {
                    hasChangedCubeColor = false;
                    spriteIndex = 5;
                    indexY--;
                    indexX++;
                    playerJump.NowJumpState = JumpStates.inJump;
                    targetPosition = ChechFallTraecktory(true);
                    playerJump.UpdateTargetPosition(targetPosition, position, "Player");
                    if (!hasJumped) hasJumped = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.E))
                {
                    hasChangedCubeColor = false;
                    spriteIndex = 1;
                    indexY++;
                    playerJump.NowJumpState = JumpStates.inJump;
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


        private void MoveSlowly(GameTime gameTime)
        {
            // 934 273
            nowTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X = startPosition.X + ((topPosition.X - startPosition.X) * nowTimer / maxMoveTime);
            position.Y = startPosition.Y + ((topPosition.Y - startPosition.Y) * nowTimer / maxMoveTime);
        }
    }
}