using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ItemManager : MonoBehaviour
{
    protected PlayerMovement player;
    
    private float speed = 15;
    private Vector3 moveVector;

    private void Update()
    {
        moveVector.x = 0;
        moveVector.y = speed;
        moveVector.z = 0;
        transform.Rotate((moveVector * speed) * Time.deltaTime);
    }

    virtual protected void Effectitem()
    {

    }

    protected virtual void Destroy()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Effect");
            player = other.gameObject.GetComponent<PlayerMovement>();
            Effectitem();
            Destroy();
        }
    }
}
