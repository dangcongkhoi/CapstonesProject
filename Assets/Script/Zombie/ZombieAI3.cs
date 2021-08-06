using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI3 : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    Animator anim;
    bool isDead = false;
    public bool canAttack = true;
    public float distance_enemy = 10f;
    [SerializeField] float turnSpped = 5f;
    [SerializeField] float attackTime = 2f;
    [SerializeField] float damageAmount;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (target != null)
        {
            if (distance > distance_enemy && !isDead)
            {
                Idle();
            }

            else if (distance < distance_enemy && !isDead)
            {
                ChasePlayer();
            }

            else if (canAttack && !PlayerHealth.singleton.isPlayerDead)
            {
                AttackPlayer();
            }
            else if (PlayerHealth.singleton.isPlayerDead)
            {
                canAttack = false;
            }
        }
    }

    public void EnemyDeathAnim()
    {
        isDead = true;
        anim.SetTrigger("isDead");
    }

    void ChasePlayer()
    {
        agent.updateRotation = true;
        agent.updatePosition = true;
        agent.SetDestination(target.position);
        anim.SetBool("isIdle", false);
        anim.SetBool("isWalking", true);
        anim.SetBool("isAttacking", false);
    }

    void AttackPlayer()
    {
        agent.updateRotation = false;
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpped * Time.deltaTime);
        agent.updatePosition = false;
        anim.SetBool("isIdle", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", true);
        StartCoroutine(AttackTime());
    }

    void Idle()
    {
        agent.updatePosition = false;
        agent.updateRotation = false;
        agent.SetDestination(target.position);
        anim.SetBool("isIdle", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", false);
    }

    //void DisableEnemy()
    //{
    //    canAttack = false;
    //    anim.SetBool("isAttacking", false);
    //}


    IEnumerator AttackTime()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        PlayerHealth.singleton.PlayerDamage(damageAmount);
        yield return new WaitForSeconds(attackTime);
        canAttack = true;
    }
}
