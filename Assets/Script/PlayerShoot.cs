using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerShoot : MonoBehaviour
{
    [SerializeField] float weaponSwitchTime;

    Shooter[] weapons;
    Shooter activeWeapon;

    int currentWeaponIndex;
    bool canFire;
    Transform weaponHolster;
    //ParticleSystem muzzleFireParticleSystem;
    PhotonView PV;
    public event System.Action<Shooter> OnWeaponSwitch;

    public Shooter ActiveWeapon
    {
        get
        {
            return activeWeapon;
        }
    }

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        canFire = true;
        weaponHolster = transform.Find("Weapon");
        weapons = weaponHolster.GetComponentsInChildren<Shooter>();

        //DeactiveWeapons();
        if (weapons.Length > 0)
            Equip(0);

    }

    void DeactiveWeapons()
    {
        for(int i =0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
            weapons[i].transform.SetParent(weaponHolster);
        }
    }

    public void SwitchWeapon(int direction)
    {
        canFire = false;
        currentWeaponIndex += direction;

        if (currentWeaponIndex > weapons.Length - 1)
            currentWeaponIndex = 0;
       
        if (currentWeaponIndex < 0)
            currentWeaponIndex = weapons.Length - 1;

        //if (muzzleFireParticleSystem == null)
        //    muzzleFireParticleSystem.Stop();

        GameManager.Instance.Timer.Add(() =>
        {
            Equip(currentWeaponIndex);
        }, weaponSwitchTime);
        
    }

    public void Equip(int index)
    {
        DeactiveWeapons();
        canFire = true;
        activeWeapon = weapons[index];
        activeWeapon.Equip();
        weapons[index].gameObject.SetActive(true);

        if (OnWeaponSwitch != null)
            OnWeaponSwitch(activeWeapon);
    }          

    void Update()
    {
        if (!PV.IsMine) return;
        if (GameManager.Instance.InputController.MouseWheelDown)
            SwitchWeapon(1);

        if (GameManager.Instance.InputController.MouseWheelUp)
            SwitchWeapon(-1);

        //if (GameManager.Instance.InputController.ChangeGunDown)
        //    SwitchWeapon(1);

        //if (GameManager.Instance.InputController.ChangeGunUp)
        //    SwitchWeapon(-1);

        if (!canFire)
            return;

        if (GameManager.Instance.InputController.Fire1)
        {
            activeWeapon.Fire();
        }
    }
}
