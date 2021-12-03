using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    
    private float NextTimeToFire = 0f ;

    [SerializeField] private float TimeBetweenShoot = 1f;
    [SerializeField] private float shootSpeed = 7f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPos;


    Rigidbody2D rb;
    Player target;
    // Enemy AI
    [SerializeField] private float range = 10f;
    [SerializeField] private Transform _player;
    private Transform enemyGraphics;
    private float distToPlayer;
    private void Awake()
    {
       enemyGraphics = transform.Find("EGraphics");
    }
    private void Start()
    {
        target = GameObject.FindObjectOfType<Player>();
    }
    private void Update()
    {
        if ( (distToPlayer <= range) && (_player != null) )
        {
            if( _player.position.x > transform.position.x && (enemyGraphics.localScale.x < 0))
            {
                Flip();
            }
            if(_player.position.x < transform.position.x && (enemyGraphics.localScale.x > 0 ))
            {
                Flip();
            }

            Attack();
        }
    }

    void FixedUpdate()
    {
        if(_player != null)
        {
            distToPlayer = Vector2.Distance(transform.position, _player.position);
        } 
    }

    
    private void Attack()
    {
       
        if (Time.time > NextTimeToFire)
        {
            int direction()
            {
                if (enemyGraphics.localScale.x < 0f)
                {
                    return -1;
                }
                else
                {
                    return +1;
                }
            }

            GameObject newBullet = Instantiate(bulletPrefab, shootPos.position, Quaternion.identity);

            rb = newBullet.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(shootSpeed * direction() , 0f );

            //newBullet.transform.localScale = new Vector2(newBullet.transform.localScale.x * direction(), newBullet.transform.localScale.y);


         
            Destroy(newBullet.gameObject, 5f);

            NextTimeToFire = Time.time + TimeBetweenShoot;
        }
    }

    private void Flip()
    {
        enemyGraphics.localScale = new Vector2(enemyGraphics.localScale.x * -1, enemyGraphics.localScale.y);

        //Vector3 theScale = enemyGraphics.localScale;
        //theScale.x *= -1;
        //enemyGraphics.localScale = theScale;

    }
}   

