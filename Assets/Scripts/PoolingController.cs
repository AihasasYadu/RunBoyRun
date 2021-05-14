using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingController : MonoSingletonGeneric<PoolingController>
{
    [System.Serializable]
    public class Pool
    {
        public PoolObjects objectType;
        public GameObject prefab;
        public int size;
    }

    [SerializeField] private List<Pool> poolList;
    private Dictionary<PoolObjects, List<GameObject>> poolDict;

    private void Start()
    {
        InstantiatePool();
    }

    private void InstantiatePool()
    {
        poolDict = new Dictionary<PoolObjects, List<GameObject>>();

        for (int i = 0; i < poolList.Count; i++)
        {
            List<GameObject> temp = new List<GameObject>();
            for (int j = 0; j < poolList[i].size; j++)
            {
                GameObject obj = Instantiate(poolList[i].prefab);
                obj.SetActive(false);
                temp.Add(obj);
            }

            poolDict.Add(poolList[i].objectType, temp);
        }
    }

    public GameObject SpawnFromPool(PoolObjects objType, Transform spawnTarget)
    {
        if(!poolDict.ContainsKey(objType))
        {
            return null;
        }

        GameObject objToSpawn = null;

        for (int i = 0; i < poolDict[objType].Count; i++)
        {
            if(!poolDict[objType][i].activeInHierarchy)
            {
                objToSpawn = poolDict[objType][i];
                break;
            }
        }
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = spawnTarget.position;
        objToSpawn.transform.rotation = spawnTarget.rotation;

        return objToSpawn;
    }
}
