using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    ParticlePool particlePool;


    private void Start()
    {
        particlePool = ParticlePool.instance;    
    }

    private void FixedUpdate()
    {
        particlePool.SpawnFromPool("explosion", transform.position, Quaternion.identity);
    }
}
