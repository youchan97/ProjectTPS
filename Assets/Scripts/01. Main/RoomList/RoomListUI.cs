using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class RoomListUI : MonoBehaviour
{
    public GameObject roomListSlotPrefab;
    public Transform parent;
    public RoomListSlot roomListSlot;
    public GameObject contents;

    public void AddRoom(int maxPlayers, string roomNameInput)
    {
        roomListSlot.SetRoom(maxPlayers, roomNameInput);
        RoomListSlot[] slots = contents.GetComponentsInChildren<RoomListSlot>();
        roomListSlot = Instantiate(roomListSlotPrefab, parent).GetComponent<RoomListSlot>();
    }
}
