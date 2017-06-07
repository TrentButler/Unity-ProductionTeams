﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    
        
    public Player Player;
    public GameObject Ammunition;
    public float LookSpeed = 10;
    public float BulletSpeed = 20;

    private Transform _bulletspawn;
    private float _speed = 20;

    // Use this for initialization
    void Start()
    {
        _bulletspawn = GetComponentInChildren<Transform>();
    }

    Vector3 LookAround()
    {
        var _hori = Input.GetAxis("HorizontalArrow");
        var _vert = Input.GetAxis("VerticalArrow");

        Vector3 _direction = new Vector3(_hori, 0, _vert);

        return _direction.normalized;
    }

    void Shoot()
    {
        var _bullet = Instantiate(Ammunition, _bulletspawn.position, _bulletspawn.localRotation);
        _bullet.GetComponent<Rigidbody>().velocity += _bulletspawn.forward * BulletSpeed;
        Destroy(_bullet, 2.0f);
    }


    // Update is called once per frame
    void Update()
    {  
        //SETUP MOVEMENT
        //SETUP CAMERA FOR PLAYER
        //var h = Input.GetAxis("Horizontal");
        //var v = Input.GetAxis("Vertical");

        //var hSpin = Input.GetAxis("HorizontalArrow");

        //transform.position += new Vector3(h, 0, v);
        //transform.Rotate(new Vector3(0, hSpin * 5, 0) * Time.deltaTime * _lookspeed);

        
    }

    void FixedUpdate()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        transform.localPosition += new Vector3(h, 0, v) * _speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if (LookAround() != Vector3.zero) // CHECKING IF THE ARROW INPUTS ARE ZERO, WILL LOCK PLAYER ROTATION ON RELEASE
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(LookAround()), Time.deltaTime * LookSpeed);
        }
    }

}
