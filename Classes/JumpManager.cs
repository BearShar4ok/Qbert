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

        public JumpStates NowJumpState { get; set; } = JumpStates.readyToJump;

        // bool isJumpGoing;
        //public Action<bool> JumpStateChanged;
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
            //if (position == targetPos)
            //{
            //    NowJumpState = JumpStates.readyToJump;
            //}
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