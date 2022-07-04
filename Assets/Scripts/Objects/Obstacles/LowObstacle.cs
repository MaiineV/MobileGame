using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowObstacle : ItemsMove
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
        ITakeDamage lowDmg = other.gameObject.GetComponent<ITakeDamage>();

        if (lowDmg != null)
        {
            lowDmg.DownDamage();
            Destroy(gameObject);
        }
    }
}
