namespace QBert.Classes
{
    public enum JumpStates { readyToJump, inJump, freeFall }
    public enum CellStates { cube, air, enemy, player, platform }
    public enum PlayerStates { onPlatform, notOnPlatform }
    public enum SpawnableEnemies { redBall, greenBall, purpleBall, coolEnemy }
}