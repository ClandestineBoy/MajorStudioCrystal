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
    List<Transform> crystalPositions;
    public int crystalRequired;
    public Text display;
    float sliceDegree;

    void Start()
    {
        sliceDegree = 360 / crystalRequired;
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        manager = gameManager.GetComponent<Game_Manager>();

        display.text = crystalRequired + "";
        door = transform.GetChild(0).gameObject;
        Debug.Log(Mathf.Sin(90));
    }


    void Update()
    {
        
    }
    Vector3 newPos;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(manager.playerStones.Count >= crystalRequired)
            {
                door.SetActive(false);
                for (int i = 0; i < crystalRequired; i++)
                {
                    stonesTaken.Add(manager.playerStones[i]);
                    newPos = new Vector3(Mathf.Cos(((sliceDegree * (i + 1)-(sliceDegree/2))*Mathf.PI)/180) * ((door.GetComponent<MeshFilter>().mesh.bounds.size.x * transform.localScale.x)/2), Mathf.Sin(((sliceDegree * (i + 1) - (sliceDegree / 2)) * Mathf.PI) / 180) * ((door.GetComponent<MeshFilter>().mesh.bounds.size.x * transform.localScale.x) / 2),1);
                    newPos = newPos + door.transform.position;
                    stonesTaken[i].GetComponent<Orbit>().Orbiting = false;
                    stonesTaken[i].GetComponent<Orbit>().lockEnd = newPos;
                    stonesTaken[i].GetComponent<Orbit>().lockSetUp();
                }
                for (int i = crystalRequired-1; i >= 0; i--)
                {
                    manager.playerStones.Remove(stonesTaken[i]);
                }
            }
        }
    }
}
