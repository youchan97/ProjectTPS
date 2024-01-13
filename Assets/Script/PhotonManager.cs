using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField nickNameInput;           // �г��� �Է�
    public TMP_InputField roomCodeInput;           // ���ڵ� �Է�

    public TMP_Dropdown setNumPlayerDropDown;      // �ִ� �ο� ����

    public Button createRoomButton;                // �游���
    public Button randomRoomButton;                // ������ ����
    public Button codeRoomButton;                  // �ڵ�� �� ����

    public Button settingButton;                   // �ɼ� ��ư
    public Button ExitButton;                      // ������ ��ư


    public static string nick;
    void Start()
    {
        Debug.Log("���� ���� �õ�.");
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }

    #region ��ư �Լ�
    public void CreateRoom()
    {
        nick = nickNameInput.text;
        if (nick.Length > 0)
        {
            PhotonNetwork.LocalPlayer.NickName = nick; // ���� �÷��̾� �г���
            // ��Ӵٿ�� ������ �ο���
            byte maxPlayers = byte.Parse(setNumPlayerDropDown.options[setNumPlayerDropDown.value].text); // ��Ӵٿ�� �� ������.

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = maxPlayers;
            PhotonNetwork.CreateRoom(null, roomOptions);
        }
        else
        {
            Debug.Log("�г����� �����ϼ���");
        }
    }
    public void RandomRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("������ ����");
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            Debug.Log("���� ����");
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public void CodeRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("�ڵ�� ����");
            PhotonNetwork.JoinRoom("���̸��� �� ����");//�Ű������� �޾� �ڵ�� ���� �ϴ°�            
        }
    }
    #endregion

    public override void OnEnable()
    {
        base.OnEnable();
    }
    public override void OnDisable()
    {
        base.OnDisable();
    }

    #region ���� �ݹ� �Լ�
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster ���� ���� �Ϸ�.");
        //createRoomButton.interactable = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected �������.");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("���̾����, Ȥ�� �� �� �����"); //�ƹ����̳� ���� �ߴµ� ���� �� ȣ��Ǵ� �Լ�
    }
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + "�濡 ����");
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + "�� �濡 ������");
    }
    #endregion
}
