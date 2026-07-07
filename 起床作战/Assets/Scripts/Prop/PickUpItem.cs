using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public int itemid=0;

    private bool isPickedUp = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isPickedUp) return;
        if (collision.gameObject.tag == "Player")
        {
            isPickedUp = true;
            collision.gameObject.GetComponent<PlayerControl>().PickUp(itemid,gameObject);
        }
    }
}
