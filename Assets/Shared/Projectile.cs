using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float timeToLive;
    [SerializeField] float damage;

    //blood ffect
    public GameObject bloodEffect;
    public GameObject bulletImpact;

    Vector3 hitPosition;
    Vector3 hitnormal;
    void Start()
    {
        Destroy(gameObject, timeToLive);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        RaycastHit hit;
        //Vector3 distance = new Vector3(10f, 0f, 10f);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
        {
            CheckDestructable(hit.transform);

            if (hit.transform.tag == "Enemy")
            {
                // Instantiate(bloodEffect, hit.point, Quaternion.identity);
            }
            else
            {
                Instantiate(bulletImpact, hit.point, Quaternion.identity);
            }
        }


    }
    //void BulletImpact(Vector3 hitPosition, Vector3 hitnormal)
    //{
    //    Instantiate(bulletImpact, hitPosition, Quaternion.LookRotation(hitnormal, Vector3.up));
    //}
    void CheckDestructable(Transform other)
    {
        var destructable = other.GetComponent<Destructable>();
        if (destructable == null)
            return;

        destructable.TakeDamage(damage);
        
    }
}
