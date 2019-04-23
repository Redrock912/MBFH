using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour, IParticle
{
    // Start is called before the first frame update
    ParticleSystem particleSystem;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void ObjectPlay()
    {
        if (particleSystem)
        {
            particleSystem.Play();
            
        }
    }


}
