﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    Camera viewCamera;

    // Start is called before the first frame update
    void Start()
    {
        viewCamera = Camera.main;
        
    }

    //public void OnMouseOver(Tiles tile)
    //{
    //    if(Input.GetMouseButton(0))
    //    {
            
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit hit;

        // 3d로 결정했습니다! 그림이 다 같은 사이즈로 올거야...!
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 20, Color.yellow);
        if(Physics.Raycast(ray,out hit, 100.0f))
        {

            print(hit.transform.parent);
            
        }
        
    }
    IEnumerable CheckHit()
    {
        
        yield return new WaitForSeconds(0.5f);
    }

}
