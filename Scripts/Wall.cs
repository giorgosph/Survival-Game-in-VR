using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.layer == LayerMask.NameToLayer("Body"))
        {
            this.GetComponent<Renderer>().enabled = true;
        }
    }

    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Body"))
        {
            this.GetComponent<Renderer>().enabled = false;
        }
    }
}
