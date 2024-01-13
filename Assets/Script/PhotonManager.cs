using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField nickNameInput;           // 닉네임 입력
    public TMP_InputField roomCodeInput;           // 방코드 입력

    public TMP_Dropdown setNumPlayerDropDown;      // 최대 인원 설정

    public Button createRoomButton;                // 방만들기
    public Button randomRoomButton;                // 랜덤방 입장
    public Button codeRoomButton;                  // 코드로 방 입장

    public Button settingButton;                   // 옵션 버튼
    public Button ExitButton;                      // 나가기 버튼


    public static string nick;
    void Start()
    {
        Debug.Log("서버 연결 시도.");
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }

    #region 버튼 함수
    public void CreateRoom()
    {
        nick = nickNameInput.text;
        if (nick.Length > 0)
        {
            PhotonNetwork.LocalPlayer.NickName = nick; // 현재 플레이어 닉네임
            // 드롭다운에서 선택한 인원수
            byte maxPlayers = byte.Parse(setNumPlayerDropDown.options[setNumPlayerDropDown.value].text); // 드롭다운에서 값 얻어오기.

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = maxPlayers;
            PhotonNetwork.CreateRoom(null, roomOptions);
        }
        else
        {
            Debug.Log("닉네임을 설정하세요");
        }
    }
    public void RandomRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("랜덤룸 누름");
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            Debug.Log("방이 없음");
            PhotonNetwork.ConnectUsingSettings();
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

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected 연결끊김.");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("방이없어요, 혹은 들어갈 수 없어요"); //아무방이나 들어가려 했는데 없을 때 호출되는 함수
    }
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + "방에 들어옴");
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + "이 방에 들어왔음");
    }
    #endregion
}
