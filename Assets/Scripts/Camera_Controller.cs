using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    private GameObject gameManager;
    private Game_Manager manager;

    public Transform camRotator;
    public Transform player;
    public Camera cam;
    [Header("Camera Position")]
    public float baseRightDistance;
    public float baseUpDistance;
    private Vector3 camPos;

    [Header("Camera Zoom")]
    public Vector3 camBack;
    public Vector3 camZoom;
    public float zoomSpeed;

    public float Y_ANGLE_MIN, Y_ANGLE_MAX;

    private float currentMouseX;
    private float currentMouseY;
    [Header("Camera Sensitivity")]
    public float mouseSensitivityX;
    public float mouseSensitivityY;

    private Vector3 dir;
    public Vector3 clipDistance;

    void Awake()
    {
        camPos.z = -4;
    }
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        manager = gameManager.GetComponent<Game_Manager>();
        manager.aiming = false;
        cam = Camera.main;
        //clipDistance = new Vector3(0, 0, 1);
        
    }

    void Update()
    {
        MoveCam();
        ZoomCam();
        dir = player.position - transform.position;
        CameraCollideCheck();
    }

    void CameraCollideCheck()
    {
        RaycastHit hit;
        //Debug.DrawRay(transform.position, -transform.forward, Color.red, 1000000f);
        Debug.DrawRay(transform.position, dir, Color.red, 1000000f);
        if (Physics.Raycast(transform.position + clipDistance, dir, out hit, 100f))
        {
//            Debug.Log(hit.transform.name);
            // cam.transform.position += new Vector3(0f, 0f, 1f);
            Vector3.MoveTowards(transform.position, player.position, 10 * Time.deltaTime);
        }
    }

    void RotateCam()
    {
        //getting mouse input
        currentMouseX += Input.GetAxis("Mouse X") * mouseSensitivityX;
        currentMouseY += Input.GetAxis("Mouse Y") * mouseSensitivityY;

        //rotating the rotator, which the base rotates around, moving the cam
        camRotator.localRotation = Quaternion.Euler(-currentMouseY, currentMouseX, 0);
        currentMouseY = Mathf.Clamp(currentMouseY, Y_ANGLE_MIN, Y_ANGLE_MAX);
       // cam.transform.LookAt(transform);
    }

    void MoveCam()
    {
        //rotator stays on player
        camRotator.position = player.position;
        //position of cam base is set relative to rotator
        transform.localPosition = new Vector3(baseRightDistance, baseUpDistance, 0);
        //cam base rotates with rotator
        transform.rotation = camRotator.rotation;
        //cam is set back from base
        cam.transform.localPosition = camPos;
        RotateCam();
    }

    private float zoomStartTime;
    private float journeyTime;
    void ZoomCam()
    {
        journeyTime = 1/zoomSpeed;

        if (Input.GetMouseButtonDown(0) && !manager.aiming)
        {
            manager.aiming = true;
            zoomStartTime = Time.time;
        }
        else if (Input.GetMouseButtonDown(0) && manager.aiming)
        {
            manager.aiming = false;
            zoomStartTime = Time.time;
        }
        float fracComplete = (Time.time - zoomStartTime) / journeyTime;
        if (manager.aiming)
        {
            camPos = Vector3.Lerp(camBack, camZoom, fracComplete);
        }
        else
        {
            camPos = Vector3.Lerp(camZoom, camBack, fracComplete);
        }
    }
}
