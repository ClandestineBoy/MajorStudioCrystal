using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private GameObject gameManager;
    private Game_Manager manager;

    public float InputX;
    public float InputZ;
    public Vector3 desiredMoveDirection;
    public bool blockRotation;
    public float desiredRotationSpeed;
    private float moveSpeed;
    public float moveAimSpeed;
    public float moveWalkSpeed;
    public float jumpSpeed;
    public float gravity;

    public float inputMag;
    public float playerRotateThreshold;
    public Camera cam;
    public CharacterController characterController;
    public bool isGrounded;
    private float verticalVelocity;
    private Vector3 moveVector;

    public KeyCode swarmForward, swarmBackward;
    public float swarmSpeed;
    public Vector3 swarmRestPos;

    GameObject orbitPoint;

    Transform swarmPar;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        manager = gameManager.GetComponent<Game_Manager>();
        orbitPoint = transform.GetChild(0).gameObject;
        swarmRestPos = orbitPoint.transform.localPosition;
        swarmPar = orbitPoint.transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main;
        characterController = this.GetComponent<CharacterController>();
    }

    int rotVar = 0;
    void Update()
    {
        rotVar += 1;
        orbitPoint.transform.rotation = Quaternion.Slerp(orbitPoint.transform.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f),desiredRotationSpeed);
        if (!manager.aiming)
        {
            swarmPar.rotation = transform.rotation;
        }

        //If Character is off ground, apply gravity
        isGrounded = characterController.isGrounded;
        if (isGrounded)
        {
            verticalVelocity = -gravity;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpSpeed;
            }
        }
        else
        {
            verticalVelocity -= gravity;
        }

        //Calculate the desired orthagonal move direction
        InputMagnitude();
        //apply move speed
        desiredMoveDirection *= moveSpeed;
        //apply gravity
        desiredMoveDirection.y = verticalVelocity;
        //move
        characterController.Move(desiredMoveDirection * Time.deltaTime);
    }

    void PlayerMoveAndRotate()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        //get what forward and right are for the camera's current rotation
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        //apply to our desired move direction
        desiredMoveDirection = forward * InputZ + right * InputX;

        if (!manager.aiming && !blockRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
        }
        //characterController.Move(desiredMoveDirection * Time.deltaTime * moveSpeed);
    }

    void InputMagnitude()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        inputMag = new Vector2(InputX, InputZ).sqrMagnitude;

        if(inputMag > playerRotateThreshold)
        {
            PlayerMoveAndRotate();
        }
        else
        { 
            desiredMoveDirection.x = 0;
            desiredMoveDirection.z = 0;
        }
        AimControl();
        SwarmMove();
    }

    void AimControl()
    {
        if (manager.aiming)
        {
            Quaternion newRot = cam.transform.rotation;
            swarmPar.rotation = cam.transform.rotation;
            newRot.x = 0;
            newRot.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, newRot, desiredRotationSpeed);
            moveSpeed = moveAimSpeed;
        }
        else
        {
            moveSpeed = moveWalkSpeed;
        }
    }

    void SwarmMove()
    {
        if (manager.aiming)
        {
            orbitPoint.GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (Input.GetKey(swarmForward))
            {
                orbitPoint.GetComponent<Rigidbody>().velocity = swarmPar.forward * swarmSpeed * Time.deltaTime;
                //orbitPoint.GetComponent<Rigidbody>().velocity = Vector3.Normalize(new Vector3(orbitPoint.transform.position.x - transform.position.x, 0, orbitPoint.transform.position.z - transform.position.z))*swarmSpeed;
            }
            if (Input.GetKey(swarmBackward))
            {
                orbitPoint.GetComponent<Rigidbody>().velocity = swarmPar.forward * -swarmSpeed * Time.deltaTime;
                //orbitPoint.GetComponent<Rigidbody>().velocity = Vector3.Normalize(new Vector3(orbitPoint.transform.position.x - transform.position.x, 0, orbitPoint.transform.position.z - transform.position.z)) * -swarmSpeed;
            }
        }
        else
        {
            orbitPoint.transform.localPosition = swarmRestPos;
        }
    }
}
