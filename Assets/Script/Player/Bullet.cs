using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] private float speed = 20f;
    [SerializeField] private int damageToEnemy = 10;
   

    // hit effect
    public GameObject impactEffect;
    
    private void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if ( hitInfo.gameObject.tag == "Enemy"  )
        {
            EnemyScript enemy = hitInfo.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                enemy.DamageEnemy(damageToEnemy);
            }
           GameObject impactNew = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(impactNew.gameObject, 1f);
        }
        
        Destroy(gameObject);
    }
}
