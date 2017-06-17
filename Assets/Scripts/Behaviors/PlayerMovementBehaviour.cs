using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehaviour : MonoBehaviour {


    #region //MEMEBER VARIABLES    
    public float MovementSpeed = 20;
    public float AimSpeed = 10;
    Animator _animator;
    #endregion

    Vector3 Move()
    {
        //LEFT JOYSTICK CONTROLL
        //var h = Input.GetAxis("HorizontalLeftJoy");
        //var v = Input.GetAxis("VerticalLeftJoy");

        //WSAD CONTROLL
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        _animator.SetFloat("WalkMovement", Mathf.Abs(h) + Mathf.Abs(v));

        Vector3 _direction = new Vector3(h, 0, v);
        return _direction.normalized;
    }

    Vector3 Aim()
    {
        //RIGHT JOYSTICK CONTROLL
        var _hori = Input.GetAxis("HorizontalRightJoy");
        var _vert = Input.GetAxis("VerticalRightJoy");

        //ARROW CONTROLLS
        //var _hori = Input.GetAxis("HorizontalArrow");
        //var _vert = Input.GetAxis("VerticalArrow");

        _animator.SetFloat("AimMovement", Mathf.Abs(_hori) + Mathf.Abs(_vert));

        Vector3 _direction = new Vector3(_hori, 0, _vert);
        return _direction.normalized;
    }
    
    void Start ()
    {
        _animator = GetComponent<Animator>();
    }
	
    private void FixedUpdate()
    {
        if (GetComponent<PlayerBehaviour>()._player.Alive == true)
        {
            if (Move() != Vector3.zero) //CHECK IF THE PLAYER IS MOVING
            {
                if (Aim() == Vector3.zero) //CHECK IF THE PLAYER IS AIMING
                {
                    MovementSpeed = 20f;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Move()), Time.deltaTime * AimSpeed);
                }
                else
                    MovementSpeed = 2f;
            }

            if (Aim() != Vector3.zero) // CHECKING IF THE ARROW INPUTS ARE ZERO, WILL LOCK PLAYER ROTATION ON RELEASE
            {
                transform.localRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Aim()), Time.deltaTime * AimSpeed);
            }

            transform.localPosition += Move() * MovementSpeed * Time.deltaTime;
        }
    }
}
