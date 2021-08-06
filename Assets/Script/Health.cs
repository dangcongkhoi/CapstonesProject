using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Destructable
{
    ZombieAI zombieAI;
    

    //[SerializeField] float inSeconds;

    private void Start()
    {
        zombieAI = GetComponent<ZombieAI>();
    }
    public override void Die()
    {
        base.Die();

        zombieAI.EnemyDeathAnim();
        
        Destroy(gameObject, 10);
        Destroy(gameObject.GetComponent<CapsuleCollider>());
        //GameManager.Instance.Respawner.Despawn(gameObject, inSeconds);
    }

    //void OnEnable()
    //{
    //    Reset();
    //}
    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);        
    }
}
