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
        private Vector2 targetPos;
        private Vector2 startPos;
        private float timeToEnd = 1f;
        private float nowTime;

        private const float g = 400f;

        public JumpStates NowJumpState { get; set; } = JumpStates.readyToJump;
        public Vector2 position;

        public void Update(GameTime gametime)
        {
            switch (NowJumpState)
            {
                case JumpStates.readyToJump:
                    break;
                case JumpStates.inJump:
                    Jump(gametime);
                    break;
                default:
                    break;
            }
        }
        public void UpdateTargetPosition(Vector2 targetPos, Vector2 startPos)
        {
            this.targetPos = targetPos;

            this.startPos = startPos;
            position = startPos;
            NowJumpState = JumpStates.inJump;
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
                nowTime = 0;
                NowJumpState = JumpStates.readyToJump;
            }
        }
    }
}