﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    Camera viewCamera;
    PlayerManager playerManager;

    // 누를 때 소리가 나는거니까 여기에 배치
    public AudioClip explosionSoundAudio;
    public AudioClip revealSoundAudio;

    public Transform testEffectPrefab;
    

    // Start is called before the first frame update
    void Start()
    {
        viewCamera = Camera.main;
        playerManager = FindObjectOfType<PlayerManager>();
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

            Tiles tempTile = hit.transform.parent.GetComponent<Tiles>();

            Instantiate(testEffectPrefab, tempTile.transform);

            if (Input.GetMouseButtonDown(0) && tempTile.isRevealed == false && tempTile.parentGrid.isTopGrid == true)
            {
                // 타일을 까도록하자.

                if (tempTile.isMine)
                {
                    AudioManager.instance.PlaySound(explosionSoundAudio, transform.position);
                }
                else
                {
                    AudioManager.instance.PlaySound(revealSoundAudio, transform.position);
                }
               
                tempTile.RevealTile();


                // 클릭할 때마다 기회 사용
                playerManager.UseCount();
            }
      
            
        }
        
    }


}
