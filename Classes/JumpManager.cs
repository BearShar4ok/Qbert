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
    public class JumpManager
    {
        private string debugPlayer;

        private Vector2 targetPos;
        private Vector2 startPos;
        private float timeToEnd = 0.4f;
        private float nowTime;
        private float prevTime;

        private float g = 2000f;

        public JumpStates NowJumpState { get; set; } = JumpStates.readyToJump;
        public Vector2 position;
        public Vector2 prevPosition;

        public float NowTime { get { return nowTime; } }
        public float TimeToEnd { get { return timeToEnd; } set { timeToEnd = value; } }
        public bool IsFall { get; private set; } = false;

        public void Update(GameTime gametime)
        {
            switch (NowJumpState)
            {
                case JumpStates.readyToJump:
                    break;
                case JumpStates.inJump:
                    Jump(gametime);
                    break;
                case JumpStates.freeFall:
                    FreeFall(gametime);
                    break;
                default:
                    break;
            }
        }
        public void UpdateTargetPosition(Vector2 targetPos, Vector2 startPos, JumpStates NowJumpState, string debug = "q")
        {
            this.targetPos = targetPos;
            debugPlayer = debug;
            this.startPos = startPos;
            position = startPos;
            this.NowJumpState = NowJumpState;
        }
        private void FreeFall(GameTime gametime)
        {
            nowTime += (float)gametime.ElapsedGameTime.TotalSeconds;
            if (position.Y < targetPos.Y)//nowTime <= timeToEnd
            {
                float deltaY = (g * nowTime * nowTime / 2) - (g * prevTime * prevTime / 2);
                if (deltaY > targetPos.Y - position.Y)
                    position.Y = targetPos.Y;
                else
                    position.Y += deltaY;
            }
            else
            {
                timeToEnd = 0.4f;
                g = 2000f;
                nowTime = 0;
                NowJumpState = JumpStates.readyToJump;
            }
            //if (debugPlayer == "Player")
            IsFall = prevPosition.Y < position.Y;

            prevPosition = position;
            prevTime = nowTime;
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
                timeToEnd = 0.4f;
                g = 2000f;
                nowTime = 0;
                NowJumpState = JumpStates.readyToJump;
            }
            //if (debugPlayer == "Player")
            IsFall = prevPosition.Y < position.Y;
            prevPosition = position;
        }
    }
}