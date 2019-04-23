using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    

    #region Singleton

    public static ParticlePool instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        for (int i = 0; i < pools.Count; i++)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int j = 0; j < pools[i].size; j++)
            {
                GameObject gmaeObject = Instantiate(pools[i].prefab);

                gameObject.SetActive(false);
                objectPool.Enqueue(gameObject);
            }

            poolDictionary.Add(pools[i].tag, objectPool);
        }
    }


    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {



        GameObject objectToSpawn =  poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IParticle pooledObj = objectToSpawn.GetComponent<IParticle>();

        if (pooledObj != null)
        {
            pooledObj.ObjectPlay();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
