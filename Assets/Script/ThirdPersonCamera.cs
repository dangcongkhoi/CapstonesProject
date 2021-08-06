using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ThirdPersonCamera : MonoBehaviour
{
    [System.Serializable]
    public class CameraRig
    {
        public Vector3 CameraOffset;
        //public float CrouchHeight;
        public float Damping;
    }

    //1.8, 1, -5
    // 10

    [SerializeField] CameraRig defaultCamera;
    [SerializeField] CameraRig aimCamera;
    [SerializeField] PhotonView PV;
    Transform cameraLookTarget;
    PhotonPlayer localPlayer;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        /*if (PV.IsMine)
        {
            
            GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;
        }*/

        
        

        GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;
    }

    void HandleOnLocalPlayerJoined (PhotonPlayer player)
    {
        localPlayer = player;
        cameraLookTarget = localPlayer.transform.Find("cameraLookTarget");

        if(cameraLookTarget == null)
        {
            cameraLookTarget = localPlayer.transform;
        }
    }

    void Update()
    {
        
            if (localPlayer == null)
                return;

            CameraRig cameraRig = defaultCamera;

            if (localPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMING || localPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMDFIRING)
                cameraRig = aimCamera;

            Vector3 targetPosition = cameraLookTarget.position + localPlayer.transform.forward * cameraRig.CameraOffset.z +
                                     localPlayer.transform.up * (cameraRig.CameraOffset.y) +
                                     localPlayer.transform.right * cameraRig.CameraOffset.x;

            Quaternion targetRotation = Quaternion.LookRotation(cameraLookTarget.position - targetPosition, Vector3.up);

            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraRig.Damping * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, cameraRig.Damping * Time.deltaTime);
        
    }
}
