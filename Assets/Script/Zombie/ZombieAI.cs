using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
public class ZombieAI : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    Animator anim;
    bool isDead = false;
    public bool canAttack = true;
    [SerializeField] float chaseDistance = 1.8f;
    [SerializeField] float turnSpped = 5f;
    [SerializeField] float attackTime = 2f;
    [SerializeField] float damageAmount;
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        float distance = Vector3.Distance(transform.position, target.position);

        if(distance > chaseDistance && !isDead)
        {
            ChasePlayer();
        }
        
        else if(canAttack && !PlayerHealth.singleton.isPlayerDead)
        {
            AttackPlayer();
        }
        //else if(PlayerHealth.singleton.isPlayerDead)
        //{
        //    //DisableEnemy();
        //    canAttack = false;
        //}
        /*else if (canAttack == false)
        {
            isDead = true;
        }*/

    }

   

    void ChasePlayer()
    {
        agent.updateRotation = true;
        agent.updatePosition = true;
        agent.SetDestination(target.position);
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
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", true);
        StartCoroutine(AttackTime());
    }

    //void DisableEnemy()
    //{
    //    canAttack = false;
    //    anim.SetBool("isAttacking", false);
    //}

    public void EnemyDeathAnim()
    {
        isDead = true;
        anim.SetTrigger("isDead");
        
        agent.isStopped=true;
        
        StartCoroutine(GameObjectDestroy(3));
    }

    IEnumerator AttackTime()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        PlayerHealth.singleton.PlayerDamage(damageAmount);
        yield return new WaitForSeconds(attackTime);
        canAttack = true;
    }
    IEnumerator GameObjectDestroy(int sec)
    {
        
        yield return new WaitForSeconds(sec);
        PhotonNetwork.Destroy(gameObject);
    }

}
