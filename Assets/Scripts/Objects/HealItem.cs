using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : ItemsMove
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
        IHeal thisHeal = other.gameObject.GetComponent<IHeal>();

        if (thisHeal != null)
        {
            thisHeal.Heal();
            Destroy(gameObject);
        }
    }
}
