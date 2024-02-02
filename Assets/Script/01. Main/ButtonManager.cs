using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance = null;
    public GameObject createRoomCanvas;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    public void CreateRoomButton()
    {
        createRoomCanvas.SetActive(true);
    }
    public void RoomOpenButton()
    {
        createRoomCanvas.SetActive(false);
    }

    public void CancelRoomButton()
    {
        createRoomCanvas.SetActive(false);
    }
    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
