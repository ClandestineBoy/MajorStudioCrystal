using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBehavior : MonoBehaviour
{
    public GameObject Lvl1Music;

    public GameObject Lvl2Music;

    public GameObject lvl3Music; 
    
    // Start is called before the first frame update
    void Start()
    {
        Lvl1Music.SetActive(true);
        Lvl2Music.SetActive(false);
        lvl3Music.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "lvl1end")
        {
            Lvl1Music.SetActive(false);
            Lvl2Music.SetActive(true);
            print("lvl1done");
        }

        if (other.gameObject.tag == "lvl2end")
        {
            Lvl2Music.SetActive(false);
            lvl3Music.SetActive(true);
        }
    }
}
