using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int distancePixel;
    [HideInInspector]
    public bool startSwipe;
    [HideInInspector]
    public Vector2 initPosSwipe;
    [HideInInspector]
    public Vector2 finalPosSwipe;
    public Action endSwipe;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        SwipeManager();
    }

    void SwipeManager()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Debug.Log("a");
            startSwipe = true;
            initPosSwipe = Input.touches[0].position;
        }
        else if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            Debug.Log("b");
            RefreshSwipe();
        }
    }

    void RefreshSwipe()
    {
        finalPosSwipe = Input.touches[0].position;
        startSwipe = false;
        endSwipe.Invoke();
    }
}
