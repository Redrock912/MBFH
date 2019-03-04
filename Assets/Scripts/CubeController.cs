using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (MeshRenderer))]
public class CubeController : MonoBehaviour
{

    public MeshRenderer cube;
    public BoxCollider box;

    // Start is called before the first frame update
    void Start()
    {
        cube = gameObject.GetComponent<MeshRenderer>();
        box = gameObject.GetComponent<BoxCollider>();
    }

    public void hideCube()
    {
        cube.enabled = false;
        box.enabled = false;
    }
}
