using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    PhotonView PV;
    private PlayerAim m_PlayerAim;
    private PlayerAim PlayerAim
    {
        get
        {
            if (m_PlayerAim == null)
                m_PlayerAim = GameManager.Instance.LocalPlayer.playerAim;
            return m_PlayerAim;
        }
    }
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine) return;
        anim.SetFloat("Vertical", GameManager.Instance.InputController.Vertical);
        anim.SetFloat("Horizontal", GameManager.Instance.InputController.Horizontal);

        anim.SetBool("IsSprinting", GameManager.Instance.InputController.IsSprinting);
        anim.SetBool("IsCrouched", GameManager.Instance.InputController.IsCrouched);
        anim.SetBool("IsReloading", GameManager.Instance.InputController.Reload);

        anim.SetFloat("AimAngle", PlayerAim.GetAngle());
    }
}
