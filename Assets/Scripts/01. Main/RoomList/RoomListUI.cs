using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListUI : MonoBehaviourPunCallbacks
{
    public GameObject roomListSlotPrefab;
    public Transform parent;
    public RoomListSlot roomListSlot;
    public GameObject contents;
    public GameObject roomList;

    public void AddRoom(int maxPlayers, string roomNameInput)
    {
        roomListSlot.SetRoom(maxPlayers, roomNameInput);
        RoomListSlot[] slots = contents.GetComponentsInChildren<RoomListSlot>();
        roomList = PhotonNetwork.Instantiate(roomListSlotPrefab.name, Vector3.zero, Quaternion.identity);
        roomList.transform.SetParent(parent);
    }
}
