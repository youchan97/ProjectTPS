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

    public TMP_InputField nickNameInput;           // 닉네임 입력
    public TMP_InputField roomCodeInput;           // 방코드 입력
    public TMP_InputField roomNameInput;           // 방이름

    public TMP_Dropdown setNumPlayerDropDown;      // 최대 인원 설정

    public Button createCharButton;                // 캐릭터만들기
    public Button createRoomButton;                // 방만들기
    public Button randomRoomButton;                // 랜덤방 입장
    public Button codeRoomButton;                  // 코드로 방 입장

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
        Debug.Log("서버 연결 시도.");
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }

    #region 버튼 함수
    public void CreateChar()
    {
        nick = nickNameInput.text;

        if (nick.Length > 0)
        {
            PhotonNetwork.LocalPlayer.NickName = nick; // 현재 플레이어 닉네임
            nickSetCanvers.SetActive(false);
            characterNum = CharNum.CharSelectNum; //선택한 캐릭터 번호 저장
        }
        else
        {
            Debug.Log("닉네임을 설정하세요");
        }
    }

    public void CreateRoom()
    {
        if (roomNameInput.text.Length > 0)
        {
            byte maxPlayers = byte.Parse(setNumPlayerDropDown.options[setNumPlayerDropDown.value].text); // 드롭다운에서 값 얻어오기.

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = maxPlayers; // 드롭다운에서 선택한 인원수
            PhotonNetwork.CreateRoom(roomNameInput.text, roomOptions);//방이름과 인원수
            roomListUI.AddRoom(maxPlayers, roomNameInput.text);
            ButtonManager.Instance.RoomOpenButton();
        }
        else
        {
            Debug.Log("방 이름을 설정하세요");
        }
    }
    public void RandomRoom()//랜덤입장
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("랜덤룸 누름");
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            Debug.Log("방이 없음");
        }
    }
    public void CodeRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("코드룸 누름");
            PhotonNetwork.JoinRoom("방이름이 들어갈 변수");//매개변수를 받아 코드로 들어가게 하는거            
        }
    }
    //여기서 클릭해서 입장하려면 클릭시 방이름 저장하고 매개변수 전달

    public void Disconnect() => PhotonNetwork.Disconnect();//나가기 버튼
    public void JoinLobby()//로비참가 버튼
    {
        Debug.Log("로비에 참가중입니다.");

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

    #region 포톤 콜백 함수
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster 서버 접속 완료.");
        //createRoomButton.interactable = true;
    }
    public override void OnDisconnected(DisconnectCause cause) => Debug.Log("OnDisconnected 연결끊김.");
    public override void OnCreatedRoom() => Debug.Log("방만들기 완료");
    public override void OnCreateRoomFailed(short returnCode, string message) => Debug.Log("방 만들기 실패");
    public override void OnJoinRoomFailed(short returnCode, string message) => Debug.Log("방 참가 실패"); 
    public override void OnJoinRandomFailed(short returnCode, string message) => Debug.Log("방 랜덤참가 실패");
    //아무방이나 들어가려 했는데 없을 때 호출되는 함수
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + "이름의 방에 들어옴" + "\r\n" +
                  PhotonNetwork.LocalPlayer.NickName + "이 방에 들어왔음");
        SceneManager.LoadScene("02. Lobby");

    }
    public override void OnJoinedLobby() => Debug.Log("로비에 연결되었습니다.");
    #endregion
}
