using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Item : MonoBehaviour
{
    PlayerMovement player;
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("speedBoots");
            player = other.gameObject.GetComponent<PlayerMovement>();
            player.SetSpeed(10);
        }
        else
        {
            Debug.Log("NoPlayer");
        }
        
    }
}
