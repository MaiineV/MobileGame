using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller
{
    Model _model;

    [SerializeField] int distancePixel = 50;

    Vector2 initPosSwipe;
    Vector2 finalPosSwipe;

    public Controller(Model model, View view)
    {
        _model = model;

        _model.actionsList[(int)Model.viewNames.STAY] += view.V_Stay;
        _model.actionsList[(int)Model.viewNames.MOVEMENT] += view.V_Walk;
        _model.actionsList[(int)Model.viewNames.JUMP] += view.V_Jump;
        _model.actionsList[(int)Model.viewNames.DEATH] += view.V_Death;
        _model.actionsList[(int)Model.viewNames.SHIELDIN] += view.V_ShieldOn;
        _model.actionsList[(int)Model.viewNames.SHIELDOUT] += view.V_ShieldOff;

        _model.healthTake += view.V_TakeHealth;
        _model.damageTake += view.V_TakeDamage;
    }

    public void OnUpdate()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            initPosSwipe = Input.touches[0].position;
        }
        else if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            finalPosSwipe = Input.touches[0].position;
            GetDir();
        }
    }

    public void GetDir()
    {
        if (finalPosSwipe.y > (initPosSwipe.y + distancePixel) || Input.GetKeyDown(KeyCode.W))
        {
            _model.Jump();
        }
        else if (finalPosSwipe.x > (initPosSwipe.x + distancePixel) || Input.GetKeyDown(KeyCode.D))
        {
            _model.Move(true);
        }
        else if (initPosSwipe.x > (finalPosSwipe.x + distancePixel) || Input.GetKeyDown(KeyCode.A))
        {
            _model.Move(false);
        }
    }
}
