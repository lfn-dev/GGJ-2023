using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : CharacterStats
{
    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
