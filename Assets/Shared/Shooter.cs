using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] float rateOfFire;
    [SerializeField] Projectile projectile; 
    [SerializeField] Transform hand;
    [SerializeField] AudioController audioReload;
    [SerializeField] AudioController audioFire;
    [SerializeField] Transform aimTarget;

    //public PlayerShoot switchWeapon;
    public WeaponReloader reloader;
    public ParticleSystem muzzleFireParticleSystem;

    float nextFireAllowed;
    Transform muzzle;

    public bool canFire;

    public void Equip()
    {
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    private void Awake()
    {
        muzzle = transform.Find("Model/Muzzle");
        reloader = GetComponent<WeaponReloader>();        
    }
    
    public void Reload()
    {
        if (reloader == null)
            return;
        reloader.Reload();
        audioReload.Play();
    }

    public void FireEffect()
    {
        if (muzzleFireParticleSystem == null)
            return;
        muzzleFireParticleSystem.Play();
    }

    //public void SwitchWeaponEffect()
    //{
    //    if (muzzleFireParticleSystem == null)
    //        switchWeapon.SwitchWeapon(int direction);
    //    muzzleFireParticleSystem.Stop();
    //}
    public virtual void Fire()
    {
        
        canFire = false;

        if (Time.time < nextFireAllowed)
            return;

        if(reloader != null)
        {
            if (reloader.IsReloading)
                return;
            if (reloader.RoundsRemainingInClip == 0)
                return;
            reloader.TakeFromClip(1);
        }

        nextFireAllowed = Time.time + rateOfFire;

        muzzle.LookAt(aimTarget);
        FireEffect();
        //SwitchWeaponEffect();

        //instantiate the projectile
        Instantiate(projectile, muzzle.position, muzzle.rotation);
        audioFire.Play();
        canFire = true;
    }
}
