using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth2 : Destructable
{
    public static PlayerHealth2 singleton;
    [SerializeField] SpawnPoint[] spawnPoints;
    public bool isPlayerDead = false;


    private void Awake()
    {
        singleton = this;
    }

    public override void Die()
    {
        base.Die();

        //Destroy(gameObject, 10);
        //Destroy(gameObject.GetComponent<CapsuleCollider>());
        //GameManager.Instance.Respawner.Despawn(gameObject, inSeconds);
        
    }

    //void OnEnable()
    //{
    //    Reset();
    //}
    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
       // SpawnAtNewSpawnPoint();
    }
    void SpawnAtNewSpawnPoint()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[spawnIndex].transform.position;
        transform.rotation = spawnPoints[spawnIndex].transform.rotation;
        isPlayerDead = false;

    }
}