using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListUI : MonoBehaviourPun
{
    public GameObject roomListSlotPrefab;
    public Transform parent;
    public RoomListSlot roomListSlot;
    public GameObject contents;
    public PhotonView PV;

    private void Awake()
    {
        PV = photonView;
    }

    [PunRPC]
    public void AddRoom(int maxPlayers, string roomNameInput)
    {
        roomListSlot.SetRoom(maxPlayers, roomNameInput);
        RoomListSlot[] slots = contents.GetComponentsInChildren<RoomListSlot>();
        roomListSlot = Instantiate(roomListSlotPrefab, parent).GetComponent<RoomListSlot>();
    }
}
