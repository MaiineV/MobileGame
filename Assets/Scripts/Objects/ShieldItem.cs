using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : ItemsMove
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
        IShield playerShield = other.gameObject.GetComponent<IShield>();

        if (playerShield != null)
        {
            playerShield.ActiveShield();
            Destroy(gameObject);
        }
    }

}
