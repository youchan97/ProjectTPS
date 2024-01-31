using Cinemachine;
using Photon.Realtime;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using UnityEditor;

/*struct Playerdata
{
    public float Hp;
    public string Name;
}*/



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
    public float playerMaxHp;
    public float getAbleRadius;
    public static readonly int getLayer = 1 << 7;
    public GameObject hasGunObject;
    public Collider[] cols;
    //Playerdata data;

    [HideInInspector]
    public InputAction zoomAction;
    [HideInInspector]
    public InputAction changeShootAction;
    [HideInInspector]
    public InputAction autoShootAction;
    [HideInInspector]
    public InputAction shootAction;
    [HideInInspector]
    public InputAction reloadAction;
    InputAction farmAction;

    public List<HealItem> healItems;



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
            else if (playerHp >= playerMaxHp)
                playerHp = playerMaxHp;
                
        }
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        sa = GetComponent<StarterAssetsInputs>();
        tpController = GetComponent<ThirdPersonController>();
        anim = GetComponent<Animator>();
        farmAction = playerInput.actions["Farm"];
        zoomAction = playerInput.actions["Zoom"];
        changeShootAction = playerInput.actions["ChangeShoot"];
        shootAction = playerInput.actions["Shoot"];
        autoShootAction = playerInput.actions["AutoShoot"];
        reloadAction = playerInput.actions["Reload"];
    }
    private void Start()
    {
        curState = new IdleState(this);
        isShoot = shootAction.triggered;
    }

    private void Update()
    {
        curState = curState.InputState();
        cols = Physics.OverlapSphere(transform.position, 5, getLayer);
        if(farmAction.triggered)
        {
            Farm();
        }
        if(reloadAction.triggered)
        {
            anim.SetTrigger("Reload");
        }
    }

    public void GunReLoad()
    {
        playerGun.ReLoad();
    }


    public void EndShoot()
    {
        anim.SetBool("IsShoot", false);
    }

    public void Hit(float damage)
    {
        //PhotonNetwork.Instantiate("Blood");
        Hp -= damage;
    }

    public void Farm()
    {       
        if (cols.Length > 0 && playerGun == null)
        {
            if (cols[0].TryGetComponent(out IGetable getable))
            {
                getable.Get(this);
                playerGun.gameObject.transform.position = hasGunObject.transform.position;
                playerGun.gameObject.transform.rotation = hasGunObject.transform.rotation;
            }
        }
        else if(cols.Length > 0 && playerGun != null)
        {
            if (cols[0].TryGetComponent(out IGetable getable))
            {
                playerGun.gameObject.transform.SetParent(null);
                playerGun.gameObject.transform.SetLocalPositionAndRotation(new Vector3(this.gameObject.transform.position.x,0.3f, this.gameObject.transform.position.z + 3), this.gameObject.transform.rotation);
                playerGun.Throw();
                playerGun = null;
                getable.Get(this);
                playerGun.gameObject.transform.position = hasGunObject.transform.position;
                playerGun.gameObject.transform.rotation = hasGunObject.transform.rotation;
            }
        }
        else
        {
            Debug.Log("�ֿ�� ����");
        }
    }
}
