using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviour
{
    public GameObject[] lobbyCharSlot;
    public Sprite[] charImg;
    private void Awake()
    {
        for (int i = 0; i < lobbyCharSlot.Length; i++)
        {
            lobbyCharSlot[i].GetComponent<Image>().sprite = null;
        }
    }
    void Start()
    {
        ImageMatching();
    }

    public void ImageMatching()
    {
        int num = 0;
        if (lobbyCharSlot[num].GetComponent<Image>().sprite == null)
        {
            lobbyCharSlot[num].GetComponent<Image>().sprite = charImg[CharNum.CharSelectNum];
        }
        else
        {
            num++;
            ImageMatching();
        }
    }
}
