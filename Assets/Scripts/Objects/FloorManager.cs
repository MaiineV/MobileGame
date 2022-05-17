using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public float speed;
    public float finishDist;
    public float restartDist;

    bool canMove = false;

    private void Start()
    {
        EventManager.Subscribe("ChangeBool", ChangeBool);
    }

    void Update()
    {
        if (canMove)
        {
            if (transform.position.z <= finishDist)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, restartDist);
            }

            transform.position += Vector3.back * Time.deltaTime * speed;
        }
    }

    void ChangeBool(params object[] parameter)
    {
        canMove = !canMove;
    }
}
