
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
   
    [SerializeField] private int damageToPlayer;
  
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag("Player"))
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
