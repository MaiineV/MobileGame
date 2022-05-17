using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsMove : MonoBehaviour
{
    public float speed;
    public float finishDist;
    public bool canMove = true;

    protected void Move()
    {
        if (transform.position.z <= finishDist)
        {
            Destroy(gameObject);
        }

        transform.position += Vector3.back * Time.deltaTime * speed;
    }

    protected void ChangeBool(params object[] parameter)
    {
        canMove = !canMove;
    }
}
