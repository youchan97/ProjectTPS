using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CharNum : MonoBehaviourPunCallbacks
{
    public static CharNum Instance = null;
    public GameObject image;

    public Sprite[] characterImg;

    static int charSelectNum;
    public static int CharSelectNum
    {
        get { return charSelectNum; }
        set
        {
            charSelectNum = value;
            if (CharSelectNum >= 8)
                CharSelectNum = 0;
        }
    }
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
        image.GetComponent<Image>().sprite = characterImg[0];
    }
    public void ArrowCilck()
    {
        CharSelectNum++;
        image.GetComponent<Image>().sprite = characterImg[CharSelectNum];
    }
}
