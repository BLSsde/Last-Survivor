using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //[SerializeField] private float speed = 20f;
    [SerializeField] private int damageToPlayer;
    //public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        //rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player")
        {
            Player player = hitInfo.GetComponent<Player>();

            if (player != null)
            {
                player.DamagePlayer(damageToPlayer);
            }
            
        }

        Destroy(gameObject);
    }
}
