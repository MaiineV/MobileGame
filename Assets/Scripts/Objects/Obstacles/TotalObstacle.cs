using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalObstacle : ItemsMove
{
    void Start()
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
        ITakeDamage fullDamage = other.gameObject.GetComponent<ITakeDamage>();

        if (fullDamage != null)
        {
            fullDamage.TotalDamage();
            Destroy(gameObject);
        }
    }
}
