using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalLock : MonoBehaviour
{
    private GameObject gameManager;
    private Game_Manager manager;

    public GameObject door;
    public List<GameObject> stonesTaken;
    public int crystalRequired;
    public Text display;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        manager = gameManager.GetComponent<Game_Manager>();

        display.text = crystalRequired + "";
        door = transform.parent.gameObject;
        Debug.Log(transform.parent.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(manager.playerStones.Count >= crystalRequired)
            door.SetActive(false);

        }
    }
}
