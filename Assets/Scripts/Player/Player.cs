using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ILowDmg, ITotalDmg, IHeal
{
    public List<Transform> posibleMove;
    public int actualPosition = 1;
    public LayerMask floorMask;

    ConstructorController _controller;
    ConstructorLife _life;

    public float jumpForce;
    public Rigidbody _rb;
    public Animator _ani;

    delegate void TakeDmg();
    TakeDmg _lowDmg;
    TakeDmg _totalDmg;

    bool _canJump = true;
    bool isRunning = false;
    bool isOnAir = false;

    private void Awake()
    {
        EventManager.ResetEventDictionary();
    }

    void Start()
    {
        _controller = new ConstructorController(this);
        _life = new ConstructorLife(this);
        _rb = GetComponent<Rigidbody>();
        _ani = GetComponent<Animator>();

        _lowDmg = ApplyDmg;
        _totalDmg = ApplyDmg;

        EventManager.Subscribe("ChangeBool", InitialVoid);
    }

    public void InitialVoid(params object[] parameter)
    {
        isRunning = true;
        TriggerAnimation("Run");
    }

    public void Move(bool right)
    {
        if (right && actualPosition < 2)
        {
            actualPosition++;
            transform.position = new Vector3(posibleMove[actualPosition].position.x, transform.position.y, transform.position.z);
        }
        else if (!right && actualPosition > 0)
        {
            actualPosition--;
            transform.position = new Vector3(posibleMove[actualPosition].position.x, transform.position.y, transform.position.z);
        }
    }

    public void Jump()
    {
        if (_canJump)
        {
            _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            TriggerAnimation("Jump");
        }
    }

    public void LowDmg()
    {
        _lowDmg();
    }

    public void TotalDmg()
    {
        _totalDmg();
    }

    void ApplyDmg()
    {
        _life.ApplyDamage();
    }

    public void Heal()
    {
        _life.ApplyHeal();
    }

    public void TriggerAnimation(string animation)
    {
        _ani.SetTrigger(animation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            if (isRunning && isOnAir)
            {
                TriggerAnimation("Run");
                isOnAir = false;
            }

            _canJump = true;
            _lowDmg = ApplyDmg;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            _canJump = false;
            _lowDmg = delegate { };
            isOnAir = true;

        }
    }

}
