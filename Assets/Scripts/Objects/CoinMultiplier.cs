using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMultiplier : ItemsMove
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
        IMultiplier thisHeal = other.gameObject.GetComponent<IMultiplier>();

        if (thisHeal != null)
        {
            thisHeal.ApplyMultiplier();
            Destroy(gameObject);
        }
    }
}
