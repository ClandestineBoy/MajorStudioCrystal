using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObsticleController : MonoBehaviour
{
    public Text weightDisplay;
    private GameObject gameManager;
    private Game_Manager manager;
    Rigidbody rb;
    public int weight;
    bool meetsCount;
    public bool zerograv;
    void Start()
    {
        meetsCount = false;
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        manager = gameManager.GetComponent<Game_Manager>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.mass = weight * 2;
        if (gameObject.tag == "zerograv")
        {
            zerograv = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        weightDisplay.text = "" + weight;
        if (manager.playerStones.Count >= weight)
        {
            if (!meetsCount)
            {
                meetsCount = true;
                rb.isKinematic = false;
            }
            if (!manager.aiming)
            {
                rb.isKinematic = true;
            }
            else if (Input.GetMouseButton(0))
            {
                rb.isKinematic = false; 
                if (zerograv)
                {
                    rb.useGravity = false;
                }
                else
                {
                    rb.useGravity = true;
                }
                transform.SetParent(null);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Move Boy" && manager.aiming && manager.playerStones.Count >= weight && zerograv)
        {
           transform.SetParent(other.gameObject.transform);
            //transform.parent = other.transform;
            transform.position = other.gameObject.transform.position;
            rb.isKinematic = true;
            rb.useGravity = false;
            print("hit meh");
        }
       

        
    }
}
