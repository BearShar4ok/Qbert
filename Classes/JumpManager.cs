using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace QBert.Classes
{
    class JumpManager
    {
        private Vector2 targetPos;//
        private Vector2 startPos;//
        public Vector2 position;
        private const float g = 200f;

        private float timeToEnd = 1.5f;
        private float nowTime;

        private bool isJumpGoing;

        public JumpManager(Vector2 targetPos, Vector2 startPos)
        {
            this.targetPos = targetPos;

            this.startPos = startPos;
            position = startPos;
            isJumpGoing = true;
        }
        public void Update(GameTime gametime)
        {
            if (isJumpGoing)
            {
                Jump(gametime);
            }
        }
        private void Jump(GameTime gametime)
        {
            nowTime += (float)gametime.ElapsedGameTime.TotalSeconds;


            position.X = startPos.X + (targetPos.X - startPos.X) * nowTime / timeToEnd;
            if (nowTime <= timeToEnd)
            {
                position.Y = startPos.Y + (((targetPos.Y - startPos.Y - timeToEnd * timeToEnd * g / 2) / timeToEnd) * nowTime) + g * nowTime * nowTime / 2;
            }
            else
            {
                isJumpGoing = false;
            }
        }
    }
}