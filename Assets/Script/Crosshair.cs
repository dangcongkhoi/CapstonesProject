using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Crosshair : MonoBehaviour
{
    [SerializeField] Texture2D image;
    [SerializeField] int size;

    PhotonView PV;
    private void OnGUI()
    {

        
        

        
            if (GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMING ||
               GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMDFIRING)
            {
                
                {
                    Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
                    screenPosition.y = Screen.height - screenPosition.y;
                    GUI.DrawTexture(new Rect(screenPosition.x - size / 2, screenPosition.y - size / 4, size, size), image);
                }
              }
        
    }
}
