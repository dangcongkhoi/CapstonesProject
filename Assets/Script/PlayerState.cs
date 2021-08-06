using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public enum EMoveState
    {
        //WALKING,
        RUNNING,
        CROUCHING,
        SPRINTING
    }

    public enum EWeaponState
    {
        IDEL,
        FIRING,
        AIMING,
        AIMDFIRING
    }

    public EMoveState MoveState;
    public EWeaponState WeaponState;

    private InputController m_InputController;
    public InputController InputController
    {
        get
        {
            if (m_InputController == null)
                m_InputController = GameManager.Instance.InputController;
            return m_InputController;
        }
    }

    private void Update()
    {
        SetWeaponState();
        SetMoveState();
    }

    void SetWeaponState()
    {
        WeaponState = EWeaponState.IDEL;

        if (InputController.Fire1)
            WeaponState = EWeaponState.FIRING;

        if (InputController.Fire2)
            WeaponState = EWeaponState.AIMING;

        if (InputController.Fire1 && InputController.Fire2)
            WeaponState = EWeaponState.AIMDFIRING;
    }

    void SetMoveState()
    {
        MoveState = EMoveState.RUNNING;

        if (InputController.IsSprinting)
            MoveState = EMoveState.SPRINTING;

        if (InputController.IsCrouched)
            MoveState = EMoveState.CROUCHING;
    }
}
