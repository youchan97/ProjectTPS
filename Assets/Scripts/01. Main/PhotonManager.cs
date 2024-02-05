using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public static PhotonManager Instance = null;

    public TMP_InputField nickNameInput;           // �г��� �Է�
    public TMP_InputField roomCodeInput;           // ���ڵ� �Է�
    public TMP_InputField roomNameInput;           // ���̸�

    public TMP_Dropdown setNumPlayerDropDown;      // �ִ� �ο� ����

    public Button createCharButton;                // ĳ���͸����
    public Button createRoomButton;                // �游���
    public Button randomRoomButton;                // ������ ����
    public Button codeRoomButton;                  // �ڵ�� �� ����

    public GameObject nickSetCanvers;

    public RoomListUI roomListUI;

    public static string nick;
    public static int characterNum;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        Debug.Log("���� ���� �õ�.");
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }

    #region ��ư �Լ�
    public void CreateChar()
    {
        nick = nickNameInput.text;

        if (nick.Length > 0)
        {
            PhotonNetwork.LocalPlayer.NickName = nick; // ���� �÷��̾� �г���
            nickSetCanvers.SetActive(false);
            characterNum = CharNum.CharSelectNum; //������ ĳ���� ��ȣ ����
        }
        else
        {
            Debug.Log("�г����� �����ϼ���");
        }
    }

    public void CreateRoom()
    {
        if (roomNameInput.text.Length > 0)
        {
            byte maxPlayers = byte.Parse(setNumPlayerDropDown.options[setNumPlayerDropDown.value].text); // ��Ӵٿ�� �� ������.

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = maxPlayers; // ��Ӵٿ�� ������ �ο���
            PhotonNetwork.CreateRoom(roomNameInput.text, roomOptions);//���̸��� �ο���
            roomListUI.AddRoom(maxPlayers, roomNameInput.text);
            ButtonManager.Instance.RoomOpenButton();
        }
        else
        {
            Debug.Log("�� �̸��� �����ϼ���");
        }
    }
    public void RandomRoom()//��������
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("������ ����");
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            Debug.Log("���� ����");
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
    //���⼭ Ŭ���ؼ� �����Ϸ��� Ŭ���� ���̸� �����ϰ� �Ű����� ����

    public void Disconnect() => PhotonNetwork.Disconnect();//������ ��ư
    public void JoinLobby()//�κ����� ��ư
    {
        Debug.Log("�κ� �������Դϴ�.");

        PhotonNetwork.JoinLobby();
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
    public override void OnDisconnected(DisconnectCause cause) => Debug.Log("OnDisconnected �������.");
    public override void OnCreatedRoom() => Debug.Log("�游��� �Ϸ�");
    public override void OnCreateRoomFailed(short returnCode, string message) => Debug.Log("�� ����� ����");
    public override void OnJoinRoomFailed(short returnCode, string message) => Debug.Log("�� ���� ����"); 
    public override void OnJoinRandomFailed(short returnCode, string message) => Debug.Log("�� �������� ����");
    //�ƹ����̳� ���� �ߴµ� ���� �� ȣ��Ǵ� �Լ�
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + "�̸��� �濡 ����" + "\r\n" +
                  PhotonNetwork.LocalPlayer.NickName + "�� �濡 ������");
        SceneManager.LoadScene("02. Lobby");

    }
    public override void OnJoinedLobby() => Debug.Log("�κ� ����Ǿ����ϴ�.");
    #endregion
}
