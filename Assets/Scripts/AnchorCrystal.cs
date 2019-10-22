using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorCrystal : MonoBehaviour
{
    private GameObject gameManager;
    private Game_Manager manager;

    public GameObject target;
    Rigidbody rb;
    CapsuleCollider cc;
    Vector3 moveDir;
    public float moveSpeed;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        manager = gameManager.GetComponent<Game_Manager>();
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.aiming)
        {
            cc.isTrigger = false;
        }
        else
        {
            cc.isTrigger = true;
            if(Vector3.Distance(transform.position, target.transform.position) <= .5f)
            transform.position = target.transform.position;
        }
        moveDir = new Vector3(transform.position.x - target.transform.position.x, transform.position.y - target.transform.position.y, transform.position.z - target.transform.position.z);
        //moveDir.Normalize();
        rb.velocity = -moveDir * Time.deltaTime * moveSpeed;
    }
}
