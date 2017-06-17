using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



//ATTEMPT AT CAMERA ROTATE WITH PLAYER

public class MainCameraBehaviour : MonoBehaviour {

    public bool IsCameraEnabled;
    private List<Camera> _cameras;
    private Transform _playertransform;
    public float CameraRotationSpeed;
    private Camera _maincamera;
    public float camX = 0;
    public float camY = 12;
    public float camZ = -6;
    public float ang = 0;
    
	void Start ()
    {
        _playertransform = GameObject.FindGameObjectWithTag("Player").transform;
        _maincamera = GetComponent<Camera>();
        _cameras = GameObject.FindObjectsOfType<Camera>().ToList<Camera>();       

    }
	
	void Update ()
    {
        _playertransform = GameObject.FindGameObjectWithTag("Player").transform;     
    }

    private void FixedUpdate()
    {
        _cameras = GameObject.FindObjectsOfType<Camera>().ToList<Camera>();
        _cameras.ForEach(camera =>
        {
            if (camera != null)
            {
                camera.enabled = false;
                camera.GetComponent<AudioListener>().enabled = false;

                if (IsCameraEnabled == false)
                {
                    camera.enabled = true;
                    camera.GetComponent<AudioListener>().enabled = true;
                }

                if (camera.tag == "MainCamera")
                {
                    camera.enabled = IsCameraEnabled;
                    camera.GetComponent<AudioListener>().enabled = IsCameraEnabled;
                }
            }
        });


        Vector3 offset = _playertransform.position;
        Vector3 calc_offset = new Vector3(Mathf.Clamp(camX, -4, 4), camY, Mathf.Clamp(camZ, -6, 6));
        offset += calc_offset;
        _maincamera.transform.position = offset;
        //_maincamera.transform.rotation = Quaternion.AngleAxis(ang, Vector3.right);

        _maincamera.transform.rotation = LookAtPlayer();
    }


    private Quaternion LookAtPlayer()
    {
        Quaternion rotation;

        Vector3 direction = _playertransform.position - transform.position;
        rotation = Quaternion.LookRotation(direction.normalized);

        return Quaternion.Slerp(transform.rotation, rotation, CameraRotationSpeed);
    }


    Vector3 Look()
    {
        //RIGHT JOYSTICK CONTROLL
        var _hori = Input.GetAxis("HorizontalRightJoy");
        var _vert = Input.GetAxis("VerticalRightJoy");

        //ARROW CONTROLLS
        //var _hori = Input.GetAxis("HorizontalArrow");
        //var _vert = Input.GetAxis("VerticalArrow");

        //_animator.SetFloat("AimMovement", Mathf.Abs(_hori) + Mathf.Abs(_vert));

        Vector3 _direction = new Vector3(_hori, 0, _vert);
        return _direction.normalized;
    }


}
