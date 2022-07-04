using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void V_Stay()
    {
        //pausar sonido caminata
        _animator.SetTrigger("Idle");
    }

    public void V_Walk()
    {
        //sonido caminata
        _animator.SetTrigger("Run");
    }

    public void V_Jump()
    {
        //pausar caminata
        //sonido salto
        _animator.SetTrigger("Jump");
    }

    public void V_Slide()
    {
        //pausar caminata
        //sonido slide
        //_animator.SetTrigger("Slide");
    }

    public void V_ShieldOn()
    {
        //activar escudo
        //sonido
    }

    public void V_ShieldOff()
    {
        //desactivar escudo
        //sonido
    }

    public void V_TakeDamage(float life)
    {
        //Poner animacion de dmg
        //cambiar mat a rojo
        SoundManager.instance.PlaySound(SoundID.TAKEDAMAGE);
        Debug.Log(life);
        EventManager.Trigger("DmgLife", life);
    }

    public void V_TakeHealth(float life)
    {
        //Particulas de cura
        SoundManager.instance.PlaySound(SoundID.HEAL);
        EventManager.Trigger("HealLife", life);
    }

    public void V_Death()
    {
        //pausar caminata
        _animator.SetTrigger("Death");
    }
}
