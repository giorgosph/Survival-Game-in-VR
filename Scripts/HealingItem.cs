using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider coll)
    {

        if (coll.GetComponent<Collider>().gameObject.layer == LayerMask.NameToLayer("Body"))
        {
            coll.gameObject.GetComponent<Player>().HealPlayer();
            Destroy(gameObject);
        }
    }

}
