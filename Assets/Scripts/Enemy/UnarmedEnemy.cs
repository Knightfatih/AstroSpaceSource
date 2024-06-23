using UnityEngine;

public class UnarmedEnemy : EnemyAI
{
    protected override void InitializeEnemy()
    {
        patrollingSpeed = 3f;
        chasingSpeed = 5f;
    }
}
