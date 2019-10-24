using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRotate : MonoBehaviour
{
    private GameObject gameManager;
    private Game_Manager manager;

    RectTransform rt;
    public Text count;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        manager = gameManager.GetComponent<Game_Manager>();

        rt = GetComponent<RectTransform>();
        zRot = 0;
    }

    float zRot;
    void Update()
    {
        count.text = manager.playerStones.Count + "";
        zRot+=.1f;
        rt.rotation = Quaternion.Euler(0, 0, zRot);
    }
}
