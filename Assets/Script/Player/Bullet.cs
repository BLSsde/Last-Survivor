
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    [SerializeField] private int damageToEnemy;
    // hit effect
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private GameObject enemyHitEffect;

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        
        if (hitInfo.gameObject.CompareTag("Enemy"))
        {
            EnemyScript enemy = hitInfo.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                enemy.DamageEnemy(damageToEnemy);
                
            }

            GameObject hitNew = Instantiate(enemyHitEffect, transform.position, transform.rotation);
            Destroy(hitNew, 0.5f);
        }
        if (hitInfo.gameObject.CompareTag("Obstacles"))
        {
            
            GameObject impactNew = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(impactNew, 0.5f);
        }

        Destroy(gameObject);
    }

}
