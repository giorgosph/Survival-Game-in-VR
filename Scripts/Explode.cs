using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] private float health = 2f;
    [SerializeField] private float explodeRange = 2f;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private AudioSource explodeSource;

    private LayerMask enemyLayer;

    // Start is called before the first frame update
    void Awake()
    {
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    void Start()
    {
        explosion.Stop();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(health <= 0f)
        {
            explodeSource.Play();
            explosion.Play();
            Invoke(nameof(Exploding), .2f);
            Invoke(nameof(DestroyObject), 2.5f);
        }
    }

    private void Exploding()
    {
        //Physics.CheckSphere(transform.position, explodeRange, enemyLayer);
        Collider[] enemies = Physics.OverlapSphere(transform.position, explodeRange, enemyLayer);
        foreach (var enemy in enemies)
        {
            enemy.gameObject.GetComponent<Enemy>().TakeDamage(3f);
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explodeRange);
    }
}

