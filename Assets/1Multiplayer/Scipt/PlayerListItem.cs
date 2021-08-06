using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text text;
    Player player;
    public void SetUp(Player _player)
    {
        player = _player;
    }
    public override void OnPlayerLeftRoom(Player ortherPlayer)
    {
        if(player == ortherPlayer)
        {
            Destroy(gameObject);

        }
    }
    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }

}
