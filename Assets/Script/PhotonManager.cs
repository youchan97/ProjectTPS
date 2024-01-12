using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviour
{
    public TMP_InputField nickNameInput;           // �г��� �Է�
    public TMP_InputField roomCodeInput;           // ���ڵ� �Է�

    public TMP_Dropdown setNumPlayerDropDown;      // �ִ� �ο� ����

    public Button createRoomButton;                //�游���
    public Button randomRoomButton;                //������ ����
    public Button codeRoomButton;                  //�ڵ�� �� ����

    public Button settingButton;                   //�ɼ� ��ư
    public Button ExitButton;                      //������ ��ư

    void Start()
    {
        Debug.Log("���� ���� �õ�.");
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
