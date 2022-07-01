using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using QBert.Classes;

namespace QBert
{
    public class Game1 : Game
    {
        public static List<List<Cube>> cubes = new List<List<Cube>>();
        public static List<List<Cell>> Cells = new List<List<Cell>>();

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int cube_coord_x;
        private int cube_coord_y;
        private int cube_width = 100;
        private int cube_height = 100;
        private List<RedCircle> redCircles = new List<RedCircle>();
        private PurpleCircle purpleCircle;
        private CoolEnemy coolEnemy;
        private List<GreenCircle> greenCircles = new List<GreenCircle>();
        private Snake snake;
        private Player player;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 900;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            cube_coord_x = (_graphics.PreferredBackBufferWidth / 2 - cube_width / 2 - cube_width * 3) - cube_width - 8;
            cube_coord_y = _graphics.PreferredBackBufferHeight - 150;


            int amoutCellsInLine = 9;
            for (int i = 0; amoutCellsInLine >= i; i++)
            {
                Cells.Add(new List<Cell>());
                if (i != 0 && i != 9 && i != 8)
                {
                    cubes.Add(new List<Cube>());
                }

                for (int j = 0; j <= amoutCellsInLine - i; j++)
                {
                    Cell cell = new Cell(
                        new Rectangle(cube_coord_x + (cube_width / 2 - 2) * (i) + (cube_width - amoutCellsInLine) * j, cube_coord_y - (i) * (cube_height - 27), 100, 100),
                         new Rectangle(cube_coord_x + (cube_width / 2 - 2) * (i) + (cube_width - amoutCellsInLine) * j - 2, cube_coord_y - (i) * (cube_height - 27), 95, 50)
                    );
                    Cells[i].Add(cell);

                    Cube cube;
                    if (j > 0 && j < amoutCellsInLine - i && i != 0 && i != 9 && i != 8)
                    {
                        cube = new Cube(
                         new Rectangle(Cells[i][j].X - 2, Cells[i][j].Y, 95, 50),
                         new Rectangle(Cells[i][j].X + 45, Cells[i][j].Y + 25, 47, 73),
                         new Rectangle(Cells[i][j].X - 3, Cells[i][j].Y + 25, 50, 73))
                        { Top_colors = new List<Color>() { Color.Blue, Color.Red }, Left_color = Color.Brown, Right_color = Color.Orange };
                        cubes[i - 1].Add(cube);
                        cell.objectStatechanged(cube);
                    }
                }
            }

            redCircles.Add(new RedCircle());
            greenCircles.Add(new GreenCircle());
            purpleCircle = new PurpleCircle();
            snake = new Snake();
            coolEnemy = new CoolEnemy();

            player = new Player(new Vector2(cubes[6][0].Rect_top.X + 25, cubes[6][0].Rect_top.Y - 20), 1, 7, _graphics.PreferredBackBufferHeight);
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


            foreach (RedCircle circle in redCircles) circle.LoadContent(Content);
            foreach (GreenCircle circle in greenCircles) circle.LoadContent(Content);
            purpleCircle.LoadContent(Content);
            snake.LoadContent(Content);
            coolEnemy.LoadContent(Content);

            player.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here


            player.Update(gameTime);

            foreach (RedCircle circle in redCircles) circle.Update(gameTime);
            foreach (GreenCircle circle in greenCircles) circle.Update(gameTime);
            purpleCircle.Update(gameTime);
            snake.Update(new Vector2(player.IndexX, player.IndexY), gameTime);
            coolEnemy.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            if (!player.IsPlayerLive)
            {
                if (player.playerJump.IsFall)
                {
                    player.Draw(_spriteBatch);
                    DrawCubes();
                    foreach (GreenCircle circle in greenCircles) circle.Draw(_spriteBatch);
                }
                else
                {
                    DrawCubes();
                    foreach (GreenCircle circle in greenCircles) circle.Draw(_spriteBatch);
                    player.Draw(_spriteBatch);
                }
                
            }
            else
            {
                DrawCubes();
                foreach (GreenCircle circle in greenCircles) circle.Draw(_spriteBatch);
                player.Draw(_spriteBatch);
            }

            foreach (RedCircle circle in redCircles) circle.Draw(_spriteBatch);
            purpleCircle.Draw(_spriteBatch);
            snake.Draw(_spriteBatch);
            coolEnemy.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        private void DrawCubes()
        {
            foreach (List<Cell> l in Cells)
            {
                foreach (Cell cell in l) cell.Draw(_spriteBatch);
            }

            foreach (List<Cube> l in cubes)
            {
                foreach (Cube cube in l) cube.Draw(_spriteBatch);
            }
        }
    }
}