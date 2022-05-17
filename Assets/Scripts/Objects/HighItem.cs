using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighItem : ItemsMove
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
        ITotalDmg totalDmg = other.gameObject.GetComponent<ITotalDmg>();

        if (totalDmg != null)
        {
            totalDmg.TotalDmg();
            Destroy(gameObject);
        }
    }
}
