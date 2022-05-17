using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowItem : ItemsMove
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
        ILowDmg lowDmg = other.gameObject.GetComponent<ILowDmg>();

        if (lowDmg != null)
        {
            lowDmg.LowDmg();
            Destroy(gameObject);
        }
    }
}
