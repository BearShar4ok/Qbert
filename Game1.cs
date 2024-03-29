﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using QBert.Classes;
using QBert.Classes.Enemies;
using QBert.Classes.UI;
using System;

namespace QBert
{
    public class Game1 : Game
    {
        public static List<List<Cube>> cubes = new List<List<Cube>>();
        public static List<List<Cell>> Cells = new List<List<Cell>>();

        public static Action PlayerSteppedOnPlatform = MakePlatformMove;
        public static Action PlayerDroppedFromPlatform = MakePlatformEndJourney;
        public static Action PlayerLostLife = () => { playerFreezesAll = 2000f;  };
        public static Action StunAll = () => { stunAllEnemies = 5000f; };
        public static Action<SpawnableEnemies> KillThing = enemy => 
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemy == SpawnableEnemies.coolEnemy && enemies[i] is CoolEnemy)
                {
                    Cells[enemies[i].IndexY][enemies[i].IndexX].ObjectStatechanged("cube");
                    enemies.RemoveAt(i);
                    break;
                }

                if (enemy == SpawnableEnemies.greenBall && enemies[i] is GreenCircle)
                {
                    Cells[enemies[i].IndexY][enemies[i].IndexX].ObjectStatechanged("cube");
                    enemies.RemoveAt(i);
                    break;
                }
            }
        };

        public static bool isFrozen = false;

        private static List<List<List<Color>>> colors = new List<List<List<Color>>>()
        {
            new List<List<Color>>()
            {
                new List<Color>() { new Color(255, 102, 102), new Color(222, 222, 0), new Color(86, 70, 239) },
                new List<Color>() { new Color(86, 169, 153) },
                new List<Color>() { new Color(49, 70, 70) }
            },
            new List<List<Color>>()
            {
                new List<Color>() { new Color(169, 185, 15), new Color(0, 102, 239), new Color(153, 0, 102) },
                new List<Color>() { new Color(119, 135, 135) },
                new List<Color>() { new Color(15, 15, 153) }
            },
            new List<List<Color>>()
            {
                new List<Color>() { new Color(135, 0, 119), new Color(0, 49, 153), new Color(33, 135, 206) },
                new List<Color>() { new Color(185, 185, 33) },
                new List<Color>() { new Color(185, 49, 49) }
            },
            new List<List<Color>>()
            {
                new List<Color>() { new Color(0, 169, 222), new Color(69, 102, 85), new Color(255, 85, 85) },
                new List<Color>() { new Color(185, 185, 33) },
                new List<Color>() { new Color(0, 49, 153) }
            }
        };
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Random random;
        private int level = 0;
        private int round = 0;
        private int cube_coord_x;
        private int cube_coord_y;
        private int cube_width = 100;
        private int cube_height = 100;
        private float spawnTimer = 2000f;
        private static float platformFreezesAll = 0;
        private static float playerFreezesAll = 0;
        private static float stunAllEnemies = 0;
        //private List<RedCircle> redCircles = new List<RedCircle>();
        //private PurpleCircle purpleCircle;
        //private CoolEnemy coolEnemy;
        //private List<GreenCircle> greenCircles = new List<GreenCircle>();
        // private Snake snake;
        private static Qbert player;
        private Texture2D arcadeBackground;
        private Texture2D arcadeBackgroundFooter;
        private static List<Platform> platforms;
        private static List<Enemy> enemies;
        private Texture2D arcadeBackgroundSide;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.IsFullScreen = true;

            // 1000 w  900 h
            HUD.LeftBorderX = _graphics.PreferredBackBufferWidth / 2 - 500;
            HUD.TopBorderY = _graphics.PreferredBackBufferHeight / 2 - 450;
            HUD.Init();
            HUD.Color = colors[round][0][2];
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            cube_coord_x = (_graphics.PreferredBackBufferWidth / 2 - cube_width / 2 - cube_width * 3) - cube_width - 18;
            cube_coord_y = _graphics.PreferredBackBufferHeight - 350;

            random = new Random();


            int amountCellsInLine = 9;
            for (int i = 0; amountCellsInLine >= i; i++)
            {
                Cells.Add(new List<Cell>());
                if (i != 0 && i != 9 && i != 8)
                {
                    cubes.Add(new List<Cube>());
                }

                for (int j = 0; j <= amountCellsInLine - i; j++)
                {
                    Cell cell = new Cell(
                        new Rectangle(cube_coord_x + (cube_width / 2 - 2) * (i) + (cube_width - amountCellsInLine) * j, cube_coord_y - (i) * (cube_height - 27), 100, 100),
                         new Rectangle(cube_coord_x + (cube_width / 2 - 2) * (i) + (cube_width - amountCellsInLine) * j - 2, cube_coord_y - (i) * (cube_height - 27), 95, 50)
                    );
                    Cells[i].Add(cell);

                    Cube cube;
                    if (j > 0 && j < amountCellsInLine - i && i != 0 && i != 9 && i != 8)
                    {
                        cube = new Cube(
                         new Rectangle(Cells[i][j].X - 2, Cells[i][j].Y, 95, 50),
                         new Rectangle(Cells[i][j].X + 45, Cells[i][j].Y + 25, 47, 73),
                         new Rectangle(Cells[i][j].X - 3, Cells[i][j].Y + 25, 50, 73))
                        { Top_colors = colors[round][0], Left_color = colors[round][1][0], Right_color = colors[round][2][0] };
                        cubes[i - 1].Add(cube);
                        cell.ObjectStatechanged(cube);
                    }
                }
            }
            enemies = new List<Enemy>();
            player = new Qbert(1, 7, _graphics.PreferredBackBufferHeight); // 952 399
            int x = random.Next(1, 7);
            platforms = new List<Platform>() { new Platform(0, random.Next(1, 7)), new Platform(x, 9 - x) };

            //enemies.Add(new RedCircle());
            //enemies.Add(new GreenCircle());
            //enemies.Add(new PurpleCircle());
            //enemies.Add(new Snake());
            //enemies.Add(new CoolEnemy());

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            foreach (List<Cube> l in cubes)
            {
                foreach (Cube cube in l) cube.LoadContent(Content);
            }

            foreach (List<Cell> l in Cells)
            {
                foreach (Cell cell in l) cell.LoadContent(Content);
            }


            foreach (Enemy enemy in enemies)
            {
                enemy.LoadContent(Content);
            }

            foreach (Platform platform in platforms) platform.LoadContent(Content);

            player.LoadContent(Content);
            arcadeBackground = Content.Load<Texture2D>("ArcadeBackground");
            arcadeBackgroundFooter = Content.Load<Texture2D>("ArcadeBackgroundFooter");
            arcadeBackgroundSide = Content.Load<Texture2D>("ArcadeBackgroundSide");
            HUD.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)//game1
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            

            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].IsAlive && enemies[i].enemyJumpManager.NowJumpState == JumpStates.readyToJump)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < platforms.Count; i++)
            {
                platforms[i].Update(gameTime);
            }

            if (platformFreezesAll > 0)
            {
                isFrozen = true;
                platformFreezesAll -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                
                if (platformFreezesAll <= 0)
                {
                    isFrozen = false;
                    platformFreezesAll = 0;
                    for (int i = 0; i < platforms.Count; i++)
                    {
                        if (platforms[i].HasGone)
                        {
                            platforms.Remove(platforms[i]);
                            break;
                        }
                    }
                    player.StartFalling();
                }
                else
                {
                    return;
                }
            }

            if (playerFreezesAll > 0)
            {
                if (player.Lives < 0) Exit();
                isFrozen = true;
                playerFreezesAll -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (playerFreezesAll <= 0)
                {
                    isFrozen = false;
                    playerFreezesAll = 0;
                    enemies.Clear();
                    foreach (List<Cell> c in Cells)
                    {
                        foreach (Cell cell in c)
                        {
                            if (cell.CellState == CellStates.enemy) cell.ObjectStatechanged("cube");
                        }
                    }
                    player.ReturnPosition();
                }
                else
                {
                    return;
                }
            }

            player.Update(gameTime);

            if (stunAllEnemies > 0)
            {
                isFrozen = true;
                stunAllEnemies -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (stunAllEnemies <= 0)
                {
                    isFrozen = false;
                    stunAllEnemies = 0;
                    return;
                }
                else
                {
                    return;
                }
            }
            spawnTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (spawnTimer <= 0)
            {
                SpawnEnemy();
                spawnTimer = 2000f;
            }

            UpgradePurpleCircleToSnake();

            foreach (Enemy enemy in enemies)
            {
                if (enemy is Snake snake)
                {
                    snake.Update(gameTime, new Vector2(player.IndexX, player.IndexY));
                }
                else if (enemy is CoolEnemy cool)
                {
                    cool.Update(gameTime, new Vector2(player.IndexX, player.IndexY));
                }
                else
                {
                    enemy.Update(gameTime);
                }
            }

            if (AllCubesColored()) 
                StartNewRound();
            HUD.Update(player.Score, player.Lives);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            List<IDrawableOur> drawList = new List<IDrawableOur>();
            foreach (var cube in cubes)
            {
                drawList.AddRange(cube);
            }
            foreach (Enemy enemy in enemies)
                if (enemy is GreenCircle)
                    drawList.Add(enemy);

            Snake snake = null;
            foreach (Enemy enemy in enemies)
                if (enemy is Snake)
                    snake = (Snake)enemy;

            for (int i = 0; i < platforms.Count; i++)
            {
                platforms[i].Draw(_spriteBatch);
            }
            if (!player.IsPlayerLive && player.playerJump.IsFall && !player.IsDyingDown)
            {
                drawList.Insert(0, player);
            }
            else
            {
                drawList.Add(player);
            }

            if (snake != null)
            {
                if (snake.IsAlive || (!snake.IsAlive && !snake.enemyJumpManager.IsFall && !snake.IsDyingDown) || (!snake.IsAlive && snake.IsDyingDown))
                {
                    drawList.Add(snake);
                }
                else
                {
                    drawList.Insert(0, snake);
                }
            }
            foreach (var item in drawList)
            {
                item.Draw(_spriteBatch);
            }


            foreach (Enemy enemy in enemies)
                if (!(enemy is GreenCircle) && !(enemy is Snake))
                    enemy.Draw(_spriteBatch);

            HUD.Draw(_spriteBatch);

             _spriteBatch.Draw(arcadeBackgroundSide, new Rectangle(0, 0, arcadeBackgroundSide.Width, arcadeBackgroundSide.Height), Color.White);
            _spriteBatch.Draw(arcadeBackgroundSide, new Rectangle(_graphics.PreferredBackBufferWidth - arcadeBackgroundSide.Width, _graphics.PreferredBackBufferHeight - arcadeBackgroundSide.Height, arcadeBackgroundSide.Width, arcadeBackgroundSide.Height), Color.White);

            _spriteBatch.Draw(arcadeBackgroundFooter, new Rectangle(_graphics.PreferredBackBufferWidth / 2 - arcadeBackgroundFooter.Width / 2,
                _graphics.PreferredBackBufferHeight - arcadeBackgroundFooter.Height, arcadeBackgroundFooter.Width, arcadeBackgroundFooter.Height), Color.White);

            _spriteBatch.Draw(arcadeBackground, new Rectangle(_graphics.PreferredBackBufferWidth / 2 - arcadeBackground.Width / 2, 0, arcadeBackground.Width, arcadeBackground.Height), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private bool AllCubesColored()
        {
            foreach (List<Cube> c in cubes)
            {
                foreach (Cube cube in c)
                {
                    if (cube.Top_color_index != cube.Top_colors.Count - 1) return false;
                }
            }
            return true;
        }

        private void StartNewRound()
        {
            level += round == 3 ? 1 : 0;
            round = round == 3 ? 0 : round + 1;
            HUD.Level = level + 1;
            HUD.Round = round + 1;
            HUD.Color = colors[round][0][2];
            cubes.Clear();
            Cells.Clear();
            enemies.Clear();
            platforms.Clear();

            int amountCellsInLine = 9;
            for (int i = 0; amountCellsInLine >= i; i++)
            {
                Cells.Add(new List<Cell>());
                if (i != 0 && i != 9 && i != 8)
                {
                    cubes.Add(new List<Cube>());
                }

                for (int j = 0; j <= amountCellsInLine - i; j++)
                {
                    Cell cell = new Cell(
                        new Rectangle(cube_coord_x + (cube_width / 2 - 2) * (i) + (cube_width - amountCellsInLine) * j, cube_coord_y - (i) * (cube_height - 27), 100, 100),
                         new Rectangle(cube_coord_x + (cube_width / 2 - 2) * (i) + (cube_width - amountCellsInLine) * j - 2, cube_coord_y - (i) * (cube_height - 27), 95, 50)
                    );
                    Cells[i].Add(cell);

                    Cube cube;
                    if (j > 0 && j < amountCellsInLine - i && i != 0 && i != 9 && i != 8)
                    {
                        cube = new Cube(
                         new Rectangle(Cells[i][j].X - 2, Cells[i][j].Y, 95, 50),
                         new Rectangle(Cells[i][j].X + 45, Cells[i][j].Y + 25, 47, 73),
                         new Rectangle(Cells[i][j].X - 3, Cells[i][j].Y + 25, 50, 73))
                        { Top_colors = colors[round][0], Left_color = colors[round][1][0], Right_color = colors[round][2][0] };
                        cubes[i - 1].Add(cube);
                        cell.ObjectStatechanged(cube);
                    }
                }
            }

            foreach (var c in cubes)
            {
                foreach (var cube in c)
                {
                    cube.LoadContent(Content);
                }
            }


            player.IndexX = 1;
            player.IndexY = 7;
            player.Position = new Vector2(Cells[7][1].Rect_top.X + 15, Cells[7][1].Rect_top.Y - 15);

            int x = random.Next(1, 7);
            platforms = new List<Platform>() { new Platform(0, random.Next(1, 7)), new Platform(x, 9 - x) };
        }

        private static void MakePlatformMove()
        {
            (Cells[player.IndexY][player.IndexX].objectContains as Platform).IsGoing = true;
            player.MaxMoveTime = (Cells[player.IndexY][player.IndexX].objectContains as Platform).MaxMoveTime;
        }

        private static void MakePlatformEndJourney()
        {
            foreach (Enemy enemy in enemies)
            {
                if (!(enemy is Snake)) enemy.IsAlive = false;
            }

            platformFreezesAll = 1000f;
        }

        private void SpawnEnemy()
        {
            List<SpawnableEnemies> spawnableEnemies = new List<SpawnableEnemies>() 
            { 
                SpawnableEnemies.redBall,
                SpawnableEnemies.greenBall,
                SpawnableEnemies.purpleBall,
                SpawnableEnemies.coolEnemy
            };
            foreach (Enemy enemy in enemies)
            {
                if ((enemy is Snake || enemy is PurpleCircle) && spawnableEnemies.Contains(SpawnableEnemies.purpleBall))
                {
                    spawnableEnemies.Remove(SpawnableEnemies.purpleBall);
                }
                if (enemy is GreenCircle && spawnableEnemies.Contains(SpawnableEnemies.greenBall))
                {
                    spawnableEnemies.Remove(SpawnableEnemies.greenBall);
                }
                if (enemy is CoolEnemy && spawnableEnemies.Contains(SpawnableEnemies.coolEnemy))
                {
                    spawnableEnemies.Remove(SpawnableEnemies.coolEnemy);
                }
            }

            SpawnableEnemies enemy1 = spawnableEnemies[random.Next(spawnableEnemies.Count)];

            switch (enemy1)
            {
                case SpawnableEnemies.redBall:
                    RedCircle red = new RedCircle();
                    red.LoadContent(Content);
                    enemies.Add(red);
                    break;
                case SpawnableEnemies.greenBall:
                    GreenCircle green = new GreenCircle();
                    green.LoadContent(Content);
                    enemies.Add(green);
                    break;
                case SpawnableEnemies.purpleBall:
                    PurpleCircle purple = new PurpleCircle();
                    purple.LoadContent(Content);
                    enemies.Add(purple);
                    break;
                case SpawnableEnemies.coolEnemy:
                    CoolEnemy cool = new CoolEnemy();
                    cool.LoadContent(Content);
                    enemies.Add(cool);
                    break;
            }
        }

        private void UpgradePurpleCircleToSnake()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] is PurpleCircle purple && purple.HasReachedBottom)
                {
                    Snake snake = new Snake(enemies[i].PrevIndexX, enemies[i].IndexY + 1);
                    snake.LoadContent(Content);
                    enemies.Add(snake);

                    enemies.RemoveAt(i);

                    break;
                }
            }
        }
    }
}