using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyController
{   
    public void Patrolling();
    public void FollowPlayer();
    public void Attack();
    public void TakeDamage(int damage);
    public void Death();
}
