using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthZombie2 : Destructable
{
    ZombieAI3 zombieAI3;


    //[SerializeField] float inSeconds;

    private void Start()
    {
        zombieAI3 = GetComponent<ZombieAI3>();
    }
    public override void Die()
    {
        base.Die();

        zombieAI3.EnemyDeathAnim();
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
