using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;

public class RoomListSlot : MonoBehaviour, IPointerDownHandler
{
    public Image image;
    public TextMeshProUGUI playerCurrentCountText;
    public TextMeshProUGUI playerMaxCountText;
    public TextMeshProUGUI roomNameText;
    public TextMeshProUGUI roomStateText;

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
    public void SetRoom(int maxPlayers, string roomNameInput) // ���Կ� �ֱ� ���� ����
    {
        playerCurrentCountText.text = "1";
        playerMaxCountText.text = "/ " + maxPlayers.ToString();
        roomNameText.text = roomNameInput;
        roomStateText.text = "�غ� ��";
    }

}
