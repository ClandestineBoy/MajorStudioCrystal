using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public bool aiming;
    public List<GameObject> playerStones;
    public Transform playerpos, spawnpoint;
    //public Vector3 diagonal;
    //public Vector3 reversediagonal;
    //Orbit orbit;

    void Start()
    {
       


    }

    // Update is called once per frame
    void Update()
    {
        //if(playerpos.position.y <= -6f)
        //{
        //    playerpos.position = spawnpoint.position;
        //}
        if (playerpos.position.y <= -6f)
            playerpos = spawnpoint;
    }

    public void idSetter()
    {
        for (int i = 0; i < playerStones.Count; i++)
        { 
            

            if ((i + 1) % 2 == 0 && i > 0)
            {
                playerStones[i].GetComponent<Orbit>().even = true;
               // playerStones[i].GetComponent<Orbit>().formation3 = playerStones[i].GetComponent<Orbit>().diagonal;
            }
            else
            {
                //playerStones[i].GetComponent<Orbit>().formation3 = playerStones[i].GetComponent<Orbit>().reversediagonal;
            }
        }
    }
}
