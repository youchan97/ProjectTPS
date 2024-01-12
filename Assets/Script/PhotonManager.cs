using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviour
{
    public TMP_InputField nickNameInput;           // 닉네임 입력
    public TMP_InputField roomCodeInput;           // 방코드 입력

    public TMP_Dropdown setNumPlayerDropDown;      // 최대 인원 설정

    public Button createRoomButton;                //방만들기
    public Button randomRoomButton;                //랜덤방 입장
    public Button codeRoomButton;                  //코드로 방 입장

    public Button settingButton;                   //옵션 버튼
    public Button ExitButton;                      //나가기 버튼

    void Start()
    {
        Debug.Log("서버 연결 시도.");
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
