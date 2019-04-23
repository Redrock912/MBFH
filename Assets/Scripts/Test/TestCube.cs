using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour, IParticle
{
    public float upForce = 1.0f;
    public float sideForce = .1f;

    public void ObjectPlay()
    {
        float forceX = Random.Range(-sideForce, sideForce);
        float forceY = Random.Range(upForce / 2.0f, upForce);
        float forceZ = Random.Range(-sideForce, sideForce);

        Vector3 force = new Vector3(forceX, forceY, forceZ);

        GetComponent<Rigidbody>().velocity = force;
    }
}
