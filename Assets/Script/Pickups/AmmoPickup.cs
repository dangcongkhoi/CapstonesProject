using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : PickupItem
{
    [SerializeField] EWeaponType weaponType;
    [SerializeField] float respawnTime;
    [SerializeField] int amount;
    PlayerShoot playerShoot;
    PhotonPlayer player;
    public override void OnPickUp(Transform item)
    {
       var playerIventory = item.GetComponentInChildren<Container>();       
        GameManager.Instance.Respawner.Despawn(gameObject, respawnTime);
        playerIventory.Put(weaponType.ToString(), amount);
        item.GetComponent<PhotonPlayer>().PlayerShoot.ActiveWeapon.reloader.HandleOnAmmoChanged();
    }
}
