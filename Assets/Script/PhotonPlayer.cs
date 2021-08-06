using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(MoveController))]
[RequireComponent(typeof(PlayerState))]

public class PhotonPlayer : MonoBehaviour
{
    [System.Serializable]
    public class MouseInput
    {
        public Vector2 Damping;
        public Vector2 Sensitivity;
        public bool LockMouse;
    }
    [SerializeField] float walkSpeed;
    //[SerializeField] float runSpeed;
    [SerializeField] float crouchSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] MouseInput MouseControl;
    [SerializeField] AudioController footSteps;
    [SerializeField] float minimumMoveTreshold;
    [SerializeField] PhotonView PV;
    public PlayerAim playerAim;
    Vector3 previousPosition;

    private MoveController m_MoveController;
    public MoveController MoveController
    {
        get
        {
            if (m_MoveController == null)
            {
                m_MoveController = GetComponent<MoveController>();
            }               
            return m_MoveController;
        }
    }

    private PlayerShoot m_PlayerShoot;
    public PlayerShoot PlayerShoot {
        get {
            if (m_PlayerShoot == null)
                m_PlayerShoot = GetComponent<PlayerShoot>();
            return m_PlayerShoot;
            }
    }
    //private Crosshair m_Crosshair;
    //public Crosshair Crosshair
    //{
    //    get
    //    {
    //        if (m_Crosshair == null)
    //            m_Crosshair = GetComponentInChildren<Crosshair>();
    //        return m_Crosshair;
    //    }
    //}

    private PlayerState m_PlayerState;
    public PlayerState PlayerState
    {
        get
        {
            if (m_PlayerState == null)
                m_PlayerState = GetComponentInChildren<PlayerState>();
            return m_PlayerState;
        }
    }

    InputController playerInput;
    Vector2 mouseInput;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            playerInput = GameManager.Instance.InputController;
            GameManager.Instance.LocalPlayer = this;
            
            if (MouseControl.LockMouse)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

    }
    private void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            //transform.Find("AimingPivot").gameObject.SetActive(false);
            Destroy(GetComponentInChildren<Crosshair>().gameObject);

        }

    }

    void Update()
    {
        Move();
        LookAround();
    }

    void Move()
    {
        //walk
        float moveSpeed = walkSpeed;

        //run
        if (playerInput.IsSprinting)
            moveSpeed = sprintSpeed;
        //sit walk
        if (playerInput.IsCrouched)
            moveSpeed = crouchSpeed;

        Vector2 direction = new Vector2(playerInput.Vertical * moveSpeed, playerInput.Horizontal * moveSpeed);       
        MoveController.Move(direction);

        if (Vector3.Distance(transform.position, previousPosition) > minimumMoveTreshold)
            footSteps.Play();

        previousPosition = transform.position;
    }

    void LookAround()
    {
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / MouseControl.Damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.MouseInput.y, 1f / MouseControl.Damping.y);
        transform.Rotate(Vector3.up * mouseInput.x * MouseControl.Sensitivity.x);

        var crosshair = GetComponentInChildren<Crosshair>();

        //Crosshair.LookHeight(mouseInput.y * MouseControl.Sensitivity.y);
        playerAim.SetRotation(mouseInput.y * MouseControl.Sensitivity.y);
    }
}
