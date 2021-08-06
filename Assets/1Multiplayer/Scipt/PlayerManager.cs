using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    PhotonView PV;
        void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }

    }


    // Update is called once per frame

    void CreateController()
    {
        //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Zombie"),new Vector3(-75, -1, 27) , Quaternion.identity);
        
        
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
        //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Zombie"), Vector3.zero, Quaternion.identity);
        //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Capsule"), Vector3.zero, Quaternion.identity);
        Debug.Log("Instantiated Player Controller");
        
    }
}
