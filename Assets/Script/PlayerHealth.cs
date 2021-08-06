using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth singleton;
    [SerializeField] SpawnPoint[] spawnPoints;
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth;
    public bool isPlayerDead = false;

    private void Awake()
    {
        singleton = this;
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    public void PlayerDamage(float damage)
    {
        if (currentHealth >0)
        {
            currentHealth -= damage;
        }
        else
        {
            Dead();
        }
    }
    void SpawnAtNewSpawnPoint()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[spawnIndex].transform.position;
        transform.rotation = spawnPoints[spawnIndex].transform.rotation;
        isPlayerDead = false;
        currentHealth = 50;

    }

    void Dead()
    {
        currentHealth = 0;
        isPlayerDead = true;
        SpawnAtNewSpawnPoint();
    }
}
