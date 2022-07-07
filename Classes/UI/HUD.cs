using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace QBert.Classes.UI
{
    static class HUD
    {
        private static int leftBorderX;
        private static int topBorderY;
        private static int score;
        private static int level;
        private static int round;
        private static List<Label> labels;
        private static Texture2D square;

        public static List<Label> Labels { get { return labels; } set { labels = value; } }

        public static int LeftBorderX { get { return leftBorderX; } set { leftBorderX = value; } }
        public static int TopBorderY { get { return topBorderY; } set { topBorderY = value; } }
        public static int Level { get { return level; } set { level = value; } }
        public static int Round { get { return round; } set { round = value; } }
        public static Color Color { get; set; }

        public static void Init()
        {
            score = 0;
            level = 1;
            round = 1;
            labels = new List<Label>()
            {
            new Label("PLAYER", new Vector2(120 + leftBorderX, topBorderY), Color.Purple, "font32"),
            new Label("0", new Vector2(120 + leftBorderX, 50 + topBorderY), Color.DarkOrange, "font28"),
            new Label("LEVEL:", new Vector2(680 + leftBorderX,  topBorderY), Color.Green, "font28"),
            new Label("1", new Vector2(830 + leftBorderX,  topBorderY), Color.DarkOrange, "font28"),
            new Label("ROUND:", new Vector2(680 + leftBorderX, 50 + topBorderY), Color.DeepPink, "font28"),
            new Label("1", new Vector2(830 + leftBorderX, 50 + topBorderY), Color.DarkOrange, "font28"),
            new Label("Lives:", new Vector2(120 + leftBorderX, 100 + topBorderY), Color.Green, "font28"),
            new Label("3", new Vector2(250 + leftBorderX, 100 + topBorderY), Color.Green, "font28"),
            new Label("Change to:", new Vector2(120 + leftBorderX, 40), Color.Red, "font28")
            };
        }

        public static void Update(int playerScore, int playerHealth)
        {
            //if (score == playerScore) return;

            score = playerScore;
            labels[1].Text = score.ToString();
            labels[3].Text = level.ToString();
            labels[5].Text = round.ToString();
            labels[7].Text = playerHealth > -1 ? playerHealth.ToString() : "0";
        }

        public static void LoadContent(ContentManager manager)
        {
            foreach (Label label in labels) label.LoadContent(manager);
            square = manager.Load<Texture2D>("square");
        }

        public static void Draw(SpriteBatch brush)
        {
            foreach (Label label in labels) label.Draw(brush);
            brush.Draw(square, new Rectangle(leftBorderX + 360, 35, 50, 50), Color);
        }
    }
}
