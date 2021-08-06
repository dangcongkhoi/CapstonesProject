using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HealthZombie1 : Destructable
{
    ZombieAI zombieAI;
    PhotonView PV;

    //[SerializeField] float inSeconds;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        zombieAI = GetComponent<ZombieAI>();
    }
    public override void Die()
    {
        base.Die();

        zombieAI.EnemyDeathAnim();
        /*Destroy(gameObject, 10);
        Destroy(gameObject.GetComponent<CapsuleCollider>());*/
        //GameManager.Instance.Respawner.Despawn(gameObject, inSeconds);
    }

    //void OnEnable()
    //{
    //    Reset();
    //}
    /*public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
    }*/

    public override void TakeDamage(float damage)
    {
        PV.RPC("RPC_TakeDame", RpcTarget.All, damage);
        //base.TakeDamage(damage);
    }

    [PunRPC]
    void RPC_TakeDame(float damage)
    {
        if (!PV.IsMine) return;
        base.TakeDamage(damage);
    }
}
