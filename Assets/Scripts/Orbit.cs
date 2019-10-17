using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    private GameObject gameManager;
    private Game_Manager manager;

    public Vector3 dir, forward, side, cross;
    Rigidbody rb;
    public bool Orbiting;
    GameObject orbitPoint;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        manager = gameManager.GetComponent<Game_Manager>();
        Orbiting = false;
        rb = gameObject.GetComponent<Rigidbody>();
        rb.drag = Random.Range(5.5f, 6.5f);
    }

    // Update is called once per frame
    float colorProgress = 0;
    bool colorRise = true;
    void Update()
    {
        if (Orbiting)
        {
            OrbitAround();
        }
        else
        {
           
        }
        if (Input.GetMouseButtonDown(0))
        {
            if(crystalClockwise < 0)
            {
                crystalClockwise = 1;
            }
            else
            {
                crystalClockwise = -1;
            }
        }
    }
    int crystalClockwise = 1;
    public void OrbitAround()
    {
        dir = orbitPoint.transform.position - transform.position;
        transform.LookAt(dir.normalized); // look at the target
        rb.AddForce(dir.normalized * 50);//add gravity like force pulling your object towards the target
        cross = Vector3.Cross(dir, side); //90 degrees vector to the initial sideway vector (orbit direction) /I use it to make the object orbit horizontal not vertical as the vertical lookatmatrix is flawed/
        rb.AddForce(cross.normalized * 50 * crystalClockwise); //add the force to make your object move (orbit)

        if(Vector3.Distance(transform.position, orbitPoint.transform.position) >= 20)
        {
            transform.position = new Vector3(orbitPoint.transform.position.x, orbitPoint.transform.position.y, orbitPoint.transform.position.z + 5);
        }
    }
    public void SetUpOrbit()
    {
        Orbiting = true;
        
        forward = transform.forward; //forward vector just to have it (in 3d you need a plane to calculate normalvector, this will be one side of the plane)
        dir = orbitPoint.transform.position - transform.position; //direction from your object towards the target object what you will orbit (the other side of the plane)
        side = new Vector3(0, crystalClockwise, 0);
        //side = Vector3.Cross(dir, forward); //90 degrees (normalvector) to the plane closed by the forward and the dir vectors

        manager.playerStones.Add(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "OrbitPoint")
        {
            if (!Orbiting)
            {
                Debug.Log("catch");
                orbitPoint = other.gameObject;
                SetUpOrbit();
            }
        }
    }

}
