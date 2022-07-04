using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighObstacle : ItemsMove
{
    private void Start()
    {
        EventManager.Subscribe("ChangeBool", ChangeBool);
    }

    void Update()
    {
        if (canMove)
            Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        ITakeDamage topDamage = other.gameObject.GetComponent<ITakeDamage>();

        if (topDamage != null)
        {
            topDamage.TopDamage();
            Destroy(gameObject);
        }
    }
}
