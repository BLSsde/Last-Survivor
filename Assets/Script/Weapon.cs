using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    public float fireRate = 0;
    public int Damage = 10;
    public LayerMask whatToHit;
    private float timeToFire = 0f;
    private Transform firePoint;

    // for Bullet Trail effect
    public Transform bulletTrailPrefab;
    public Transform hitPrefab;
    private float timeToSpawnEffect = 0;
    [SerializeField] private float effectSpawnRate = 5f;

    // Shake the camera when we fire with the Powerful Gun
    [SerializeField] private float camShakeAmnt = 0.05f;
    [SerializeField] private float camShakeLength = 0.1f;
    private CameraShake camShake;

    // for Muzzle Flash
    public Transform muzzleFlashPrefab;

    private void Awake()
    {
        firePoint = transform.GetChild(0);
        if (firePoint == null)
        {
            Debug.LogError("No firePoint Available !");
        }
    }

    private void Start()
    {
        camShake = GameManager.gameManager.GetComponent<CameraShake>();
        if (camShake == null)
            Debug.LogError("No CameraShake script found on GameManager object!");

    }

    void Update()
    {
       
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))   // get input and shoot in Single Mode
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y );
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);

        
        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition)* 100, Color.cyan);

        if(hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            EnemyScript enemy = hit.collider.GetComponent<EnemyScript>();
            if(enemy != null)
            {
                enemy.DamageEnemy(Damage);
                // Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage !");
            }
        }

        if (Time.time >= timeToSpawnEffect)
        {
            Vector3 hitPoss;
            Vector3 hitNormal;   // the Normal(N) of the line

            if (hit.collider == null)
            {
                hitPoss = (mousePosition - firePointPosition) * 30;
                hitNormal = new Vector3(9999, 9999, 9999);
            }
            else
            {
                hitPoss = hit.point;           //hit.point return vector3 position of hit point
                hitNormal = hit.normal;
            }

            Effect(hitPoss, hitNormal);
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
    }

    private void Effect(Vector3 hitPoss, Vector3 hitNormal)
    {
        Transform trail = Instantiate(bulletTrailPrefab, firePoint.position, firePoint.rotation) as Transform; // instantiating bullet trail effect
        LineRenderer lr = trail.GetComponent<LineRenderer>();

        if(lr != null)
        {
            lr.SetPosition(0, firePoint.position);  //seting the position of lineRenderer
            lr.SetPosition(1, hitPoss);
        }

        Destroy(trail.gameObject, 0.03f);

        if(hitNormal != new Vector3(9999,9999,9999))
        {
            Transform hitParticle = Instantiate(hitPrefab, hitPoss, Quaternion.FromToRotation(Vector3.right, hitNormal));
            Destroy(hitParticle.gameObject, 1f);
        }

        // Instantiating muzzleFlash Effect
        Transform cloneOfMuzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
        cloneOfMuzzleFlash.parent = firePoint;
        float size = Random.Range(0.6f, 0.9f);
        cloneOfMuzzleFlash.localScale = new Vector3(size, size, size);
        Destroy(cloneOfMuzzleFlash.gameObject , 0.02f);

        // Shake the Camera
        camShake.Shake(camShakeAmnt, camShakeLength);
    }
}
