using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : ItemsMove
{
    public int coinBaseValue;

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
        ICoin thisCoin = other.gameObject.GetComponent<ICoin>();

        if (thisCoin != null)
        {
            thisCoin.AddCoin(coinBaseValue);
            Destroy(gameObject);
        }
    }
}
