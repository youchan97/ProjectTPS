using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MainManager : MonoBehaviourPunCallbacks
{
    public GameObject image;

    public Sprite currentImg;
    public Sprite[] characterImg;

    int charSelectNum;
    public int CharSelectNum
    { 
        get { return charSelectNum; }
        set 
        { 
            charSelectNum = value;
            if(CharSelectNum >= 8)            
                CharSelectNum = 0;            
        }
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
