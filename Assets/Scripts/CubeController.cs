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


        SetScaleToScreenSize();
    }

    public void hideCube()
    {
        cube.enabled = false;
        box.enabled = false;
    }

    public void SetScaleToScreenSize()
    {

        float width = cube.bounds.size.x;
        float height = cube.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        // 이 값은 나중에 다른데서 받아올수도 있음.
        float rowLength = 8;

        float modifiedHeight = (worldScreenHeight / height) * (9.0f / 16.0f) * (1.0f / rowLength);

        float modifiedWidth = (worldScreenWidth / width) * (1.0f / rowLength);

        // 0.1f 로 얇은 판 유지
        cube.transform.localScale = new Vector3(modifiedWidth, modifiedHeight, 0.1f);
    }
}
