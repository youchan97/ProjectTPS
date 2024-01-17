using Cinemachine;
using Photon.Realtime;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class Player : MonoBehaviour, IHitable
{
    public State curState;
    public PlayerInput playerInput;
    public StarterAssetsInputs sa;
    public ThirdPersonController tpController;
    public Animator anim;
    public GameObject cam;
    public CinemachineVirtualCamera rifleZoomCam;
    public CinemachineVirtualCamera sniperZoomCam;
    public bool isShoot;
    public Gun playerGun;
    public float playerHp;
    

    public float Hp
    { 
        get => playerHp;
        set
        {
            playerHp = value;
            if (playerHp <= 0)
            {
                Destroy(gameObject);
                PhotonNetwork.LeaveRoom();
            }
                
        }
    }

    private void Awake()
    {
        playerGun = GetComponentInChildren<Gun>();
        playerInput = GetComponent<PlayerInput>();
        sa = GetComponent<StarterAssetsInputs>();
        tpController = GetComponent<ThirdPersonController>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        curState = new IdleState(this);
        isShoot = curState.shootAction.triggered;
    }

    private void Update()
    {
        curState = curState.InputState();
    }

    public void EndShoot()
    {
        anim.SetBool("IsShoot", false);
        playerGun.BulletCount--;
    }

    public void Hit(float damage)
    {
        //PhotonNetwork.Instantiate("Blood");
        Hp -= damage;
    }
}
