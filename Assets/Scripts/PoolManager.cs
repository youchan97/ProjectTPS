using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [SerializeField]
    private GameObject effectPrefabs;
    private int poolSize = 5;
    private List<GameObject> objPools;


    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        InitObjectPool();
    }

    void Update()
    {
        
    }

    private void InitObjectPool()
    {
        objPools = new List<GameObject> ();
        for(int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(effectPrefabs);
            obj.SetActive(false);
            objPools.Add(obj);
        }
    }

    public GameObject UseObject()
    {
        GameObject obj = null;
        for(int i= 0; i< objPools.Count; i++)
        {
            if (objPools[i].activeSelf == false)
            {
                obj = objPools[i];
                obj.SetActive(true);
                return obj;
            }
        }

        obj = Instantiate(effectPrefabs);
        objPools.Add(obj);
        obj.SetActive(true);

        return obj;
    }
}
