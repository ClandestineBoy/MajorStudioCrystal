using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallChecker : MonoBehaviour
{
    public Transform player;
    public Transform spawnpoint;

    private void OnTriggerEnter(Collider other)
    {
        player.position = spawnpoint.position;
    }
}
