using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Controller
{
    private Action ActualControl = delegate { };

    private readonly Model _model;

    private const int _distancePixel = 50;

    private Vector2 _initPosSwipe;
    private Vector2 _finalPosSwipe;

    public Controller(Model model, View view)
    {
        _model = model;

        _model.actionsList[(int)Model.viewNames.STAY] += view.V_Stay;
        _model.actionsList[(int)Model.viewNames.MOVEMENT] += view.V_Walk;
        _model.actionsList[(int)Model.viewNames.JUMP] += view.V_Jump;
        _model.actionsList[(int)Model.viewNames.DEATH] += view.V_Death;
        _model.actionsList[(int)Model.viewNames.SHIELDIN] += view.V_ShieldOn;
        _model.actionsList[(int)Model.viewNames.SHIELDOUT] += view.V_ShieldOff;
        _model.actionsList[(int)Model.viewNames.SLIDE] += view.V_Slide;

        _model.healthTake += view.V_TakeHealth;
        _model.damageTake += view.V_TakeDamage;

        EventManager.Subscribe("ChangeInputs", ChangeInputs);
        EventManager.Trigger("SetButtons", this);

        if (PlayerPrefs.HasKey("ActualControls"))
        {
            var control = PlayerPrefs.GetInt("ActualControls");

            switch (control)
            {
                case 0:
                    ActualControl = SwipeControllers;
                    EventManager.Trigger("ChangeButtons", false);
                    break;
                case 1:
                    ActualControl =  delegate { };
                    EventManager.Trigger("ChangeButtons", true);
                    break;
            }
        }
        else
        {
            ActualControl = SwipeControllers;
            EventManager.Trigger("ChangeButtons", false);
        }

#if UNITY_EDITOR
        ActualControl += KeyBoardControllers;
#endif
    }

    public void OnUpdate()
    {
        ActualControl();
    }

    private void ChangeInputs(params object[] parameters)
    {
        switch ((int)parameters[0])
        {
            case 0:
                ActualControl = SwipeControllers;
                EventManager.Trigger("ChangeButtons", false);
                break;
            case 1:
                ActualControl =  delegate { };
                EventManager.Trigger("ChangeButtons", true);
                break;
        }

#if UNITY_EDITOR
        ActualControl += KeyBoardControllers;
#endif
    }

    #region Swipe

    private void SwipeControllers()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            _initPosSwipe = Input.touches[0].position;
        }
        else if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            _finalPosSwipe = Input.touches[0].position;

            if (_finalPosSwipe.y > (_initPosSwipe.y + _distancePixel))
            {
                _model.Jump();
            }
            else if (_finalPosSwipe.x > (_initPosSwipe.x + _distancePixel))
            {
                _model.Move(true);
            }
            else if (_initPosSwipe.x > (_finalPosSwipe.x + _distancePixel))
            {
                _model.Move(false);
            }
            else if (_finalPosSwipe.y < (_initPosSwipe.y + _distancePixel))
            {
                _model.Slide();
            }
        }
    }

    #endregion

    #region Buttons

    public void RightButton()
    {
        _model.Move(true);
    }

    public void LeftButton()
    {
        _model.Move(false);
    }

    public void JumpButton()
    {
        _model.Jump();
    }

    public void SlideButton()
    {
        _model.Slide();
    }

    #endregion

    #region Keyboard

    private void KeyBoardControllers()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _model.Jump();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _model.Move(true);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _model.Move(false);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _model.Slide();
        }
    }

    #endregion
    
}