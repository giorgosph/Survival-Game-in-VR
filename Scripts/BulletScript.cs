using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletScript : MonoBehaviour
{
    private float timer;
    private float damageValue = 2f;

    void Start()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime *= Time.timeScale;
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
    }

    private void SetDamage()
    {
        damageValue /= timer;
        if (damageValue > 1f)
            damageValue = 1f;
    }

    void OnTriggerEnter(Collider coll)
    {
        SetDamage();
        if (coll.GetComponent<Collider>().gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            coll.gameObject.GetComponent<Enemy>().TakeDamage(damageValue);
            Destroy(gameObject);
        }
        else if (coll.GetComponent<Collider>().gameObject.layer == LayerMask.NameToLayer("Explosive"))
        {
            coll.gameObject.GetComponent<Explode>().TakeDamage(damageValue);
            Destroy(gameObject);
        }
    }
}
