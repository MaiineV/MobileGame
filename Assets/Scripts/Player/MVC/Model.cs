using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Model : MonoBehaviour, ITakeDamage, IHeal, IShield, ICoin
{
    public List<Transform> posibleMove;
    public int actualPosition = 1;
    public LayerMask floorMask;

    Controller _controller;

    [SerializeField] int _maxLife;
    int _life;
    [SerializeField] float _jumpForce;
    [SerializeField] float _shieldTimer;

    Rigidbody _rb;

    public enum viewNames {STAY, MOVEMENT, JUMP, SLIDE, DEATH, SHIELDIN, SHIELDOUT}
    public List<Action> actionsList = new List<Action>(7);

    public event Action<int> healthTake;
    public event Action<int> damageTake;

    public delegate void DamageEntry();
    public DamageEntry fromTop;
    public DamageEntry fromBot;
    public DamageEntry allDamage;

    bool _canMoveVertical = true;
    bool _isRunning = false;
    bool _isOnAir = false;

    void Awake()
    {
        EventManager.ResetEventDictionary();
    }

    void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            Action a = delegate { };
            actionsList.Add(a);
        }

        _controller = new Controller(this, GetComponent<View>());
        _rb = GetComponent<Rigidbody>();

        fromBot = ApplyDmg;
        fromTop = ApplyDmg;
        allDamage = ApplyDmg;

        _life = _maxLife;

        _shieldTimer = _shieldTimer * (PlayerPrefs.GetInt("ShieldLevel") + 1);

        EventManager.Subscribe("ChangeBool", InitialVoid);
    }

    void Update()
    {
        _controller?.OnUpdate();
    }

    #region STARTING VOID
    public void InitialVoid(params object[] parameter)
    {
        _isRunning = !_isRunning;

        if (_isRunning)
            actionsList[(int)viewNames.MOVEMENT]?.Invoke();
        else
            actionsList[(int)viewNames.STAY]?.Invoke();
    }
    #endregion

    #region POSIBLE ACTIONS
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
        if (_canMoveVertical)
        {
            actionsList[(int)viewNames.JUMP]?.Invoke();
            _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        }
    }

    public void Slide()
    {
        if (_canMoveVertical)
        {
            fromTop = delegate { };
            _canMoveVertical = false;
            actionsList[(int)viewNames.SLIDE]?.Invoke();
        }
    }

    public void StopSlide()
    {
        _canMoveVertical = true;
        fromTop = ApplyDmg;
        actionsList[(int)viewNames.MOVEMENT]?.Invoke();
    }
    #endregion

    #region POSIBLE DAMAGE
    public void TopDamage()
    {
        fromTop?.Invoke();
    }

    public void DownDamage()
    {
        fromBot?.Invoke();
    }

    public void TotalDamage()
    {
        allDamage?.Invoke();
    }

    void ApplyDmg()
    {
        _life--;
        damageTake?.Invoke(_life);

        if (_life <= 0)
        {
            EventManager.Trigger("ChangeBool");
            actionsList[(int)viewNames.DEATH]?.Invoke();
        }
    }
    #endregion

    #region ITEMS FUNCTIONS
    public void ApplyHealth()
    {
        if (_life < _maxLife)
        {
            healthTake?.Invoke(_life);
            _life++;
        }
    }

    public void ActiveShield()
    {
        StopAllCoroutines();
        fromBot = delegate { };
        fromTop = delegate { };
        allDamage = delegate { };
        actionsList[(int)viewNames.SHIELDIN]?.Invoke();
        StartCoroutine(ShieldTimer());
    }

    IEnumerator ShieldTimer()
    {
        yield return new WaitForSeconds(_shieldTimer);
        actionsList[(int)viewNames.SHIELDOUT]?.Invoke();
        allDamage = ApplyDmg;
        fromBot = ApplyDmg;
        fromTop = ApplyDmg;
    }

    public void AddCoin(int value)
    {
        int finalValue = value * (PlayerPrefs.GetInt("CoinLevel") + 1);
        CoinManager.instance.AddCoins(finalValue);
        EventManager.Trigger("AddScore", finalValue);
    }
    #endregion

    #region COLLISIONS
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            if (_isRunning && _isOnAir)
            {
                actionsList[(int)viewNames.MOVEMENT]?.Invoke();
                _isOnAir = false;
            }

            _canMoveVertical = true;
            fromBot = ApplyDmg;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            _canMoveVertical = false;
            fromBot = delegate { };
            _isOnAir = true;
        }
    }
    #endregion
}
