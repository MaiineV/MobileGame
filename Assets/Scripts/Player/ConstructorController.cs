using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ConstructorController
{
    Player _player;

    public ConstructorController(Player player)
    {
        _player = player;
        GameManager.instance.endSwipe=GetDir;
    }

    public void GetDir()
    {
        if (GameManager.instance.finalPosSwipe.x > (GameManager.instance.initPosSwipe.x + GameManager.instance.distancePixel) || Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("r");
            _player.Move(true);
        }
        else if (GameManager.instance.initPosSwipe.x > (GameManager.instance.finalPosSwipe.x + GameManager.instance.distancePixel) || Input.GetKeyDown(KeyCode.A))
        {
            _player.Move(false);
            Debug.Log("l");
        }

        if(GameManager.instance.finalPosSwipe.y > (GameManager.instance.initPosSwipe.y + GameManager.instance.distancePixel) || Input.GetKeyDown(KeyCode.W))
        {
            _player.Jump();
            Debug.Log("u");
        }
    }
}
