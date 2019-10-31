﻿using System.Collections; using System.Collections.Generic; using UnityEngine;  public class Orbit : MonoBehaviour {     private GameObject gameManager;     private Game_Manager manager;     public Vector3 dir, forward, side, cross, formation3;     Rigidbody rb;     public bool Orbiting;     GameObject orbitPoint;     private int formationindex = 0;     public Vector3[] formationarray;     int crystalClockwise = 1;     public Vector3 diagonal;     public bool even;     public bool locked;      public Vector3 lockStart;     public Vector3 lockEnd;     float lockSpeed = 20f;     public float lockStartTime;     float journeyLength;           void Start()     {                  locked = false;         //diagonal = new Vector3((float)crystalClockwise, 0, Random.Range(.5f, .75f));         //reversediagonal = new Vector3((float)-crystalClockwise, 0, Random.Range(.5f, .75f));          gameManager = GameObject.FindGameObjectWithTag("GameController");         manager = gameManager.GetComponent<Game_Manager>();         Orbiting = false;         rb = gameObject.GetComponent<Rigidbody>();         rb.drag = Random.Range(5.5f, 6.5f);          formationarray = new[] { new Vector3(0, crystalClockwise, 0), new Vector3(crystalClockwise, 0, 0), new Vector3(crystalClockwise, 0, Random.Range(.5f, .75f)) };         side = formationarray[0];     }          float colorProgress = 0;         bool colorRise = true;      void Update()     {         if (even == true )         {              formationarray[2] = new Vector3(formationarray[2].x, formationarray[2].y, formationarray[2].z);         }         else         {             formationarray[2] = new Vector3(-formationarray[2].x, formationarray[2].y, formationarray[2].z);         }          if (Orbiting)         {             OrbitAround();         }         if(locked)         {
            goToLock();          }         if (Input.GetMouseButtonDown(0))         {             if(crystalClockwise < 0)             {                 crystalClockwise = 1;             }             else             {                 crystalClockwise = -1;             }         }          if (Input.GetKeyDown(KeyCode.Tab))         {             formationindex++;             if (formationindex >= formationarray.Length)             {                 formationindex = 0;             }         }         if (Input.GetKeyDown(KeyCode.B))         {           // Debug.Log("This many crystals in the list" + manager.playerStones.Count + " " + manager.playerStones.Count % 2);         }          //Debug.Log(formationindex);          side = formationarray[formationindex];     }      public void lockSetUp()
    {
        locked = true;
        lockStart = transform.position;
        lockStartTime = Time.time;
        journeyLength = Vector3.Distance(lockStart, lockEnd);
        Debug.Log(lockStart + "   " + lockEnd);
        rb.velocity = Vector3.zero;
    }     public void goToLock()
    {
        
        float distCovered = (Time.time - lockStartTime) * lockSpeed;

        float fractionOfJourney = distCovered / journeyLength;

        transform.position = Vector3.Slerp(lockStart, lockEnd, fractionOfJourney);
    }      public void OrbitAround()     {         dir = orbitPoint.transform.position - transform.position;         transform.LookAt(dir.normalized); // look at the target         rb.AddForce(dir.normalized * 50);//add gravity like force pulling your object towards the target         cross = Vector3.Cross(dir, side); //90 degrees vector to the initial sideway vector (orbit direction) /I use it to make the object orbit horizontal not vertical as the vertical lookatmatrix is flawed/         rb.AddForce(cross.normalized * 50 * crystalClockwise); //add the force to make your object move (orbit)          if(Vector3.Distance(transform.position, orbitPoint.transform.position) >= 20)         {             transform.position = new Vector3(orbitPoint.transform.position.x, orbitPoint.transform.position.y, orbitPoint.transform.position.z + 5);         }     }      public void SetUpOrbit()     {         Orbiting = true;         Destroy(transform.GetChild(0).gameObject);         forward = transform.forward; //forward vector just to have it (in 3d you need a plane to calculate normalvector, this will be one side of the plane)         dir = orbitPoint.transform.position - transform.position; //direction from your object towards the target object what you will orbit (the other side of the plane)         side = formationarray[formationindex];         //side = Vector3.Cross(dir, forward); //90 degrees (normalvector) to the plane closed by the forward and the dir vectors          manager.playerStones.Add(gameObject);//Adds each stone to the list         //print(manager.playerStones.Count);         manager.idSetter();          }       public void OnTriggerEnter(Collider other)     {         if (other.gameObject.tag == "OrbitPoint")         {             if (!Orbiting && !locked)             {                 orbitPoint = other.gameObject;                 SetUpOrbit();             }         }     }  }  