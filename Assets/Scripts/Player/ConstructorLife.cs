using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructorLife
{
    Player _player;
    int life = 3;

    public ConstructorLife(Player player)
    {
        _player = player;
    }

    public void ApplyHeal()
    {
        SoundManager.instance.PlaySound(SoundID.HEAL);
        if (life < 3)
        {
            EventManager.Trigger("HealLife", life);
            life++;
        }
    }

    public void ApplyDamage()
    {
        life--;

        SoundManager.instance.PlaySound(SoundID.TAKEDAMAGE);
        EventManager.Trigger("DmgLife", life);

        if (life <= 0)
            isDeath();
    }

    void isDeath()
    {
        EventManager.Trigger("ChangeBool");
        _player.TriggerAnimation("Death");
    }
}
