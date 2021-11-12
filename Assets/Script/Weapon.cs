using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate = 0;
    public float Damage = 10f;
    public LayerMask whatToHit;
    private float timeToFire = 0f;
    private Transform firePoint;

    // for Bullet Trail effect
    public Transform bulletTrailPrefab;
    private float timeToSpawnEffect = 0;
    [SerializeField] private float effectSpawnRate = 5f;

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

        if(Time.time >= timeToSpawnEffect)
        {
            Effect();
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
        //Effect();   // Call the bullet trail effect fn.
        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition)* 100, Color.cyan);
        if(hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage !");
        }
    }

    private void Effect()
    {
        Instantiate(bulletTrailPrefab, firePoint.position, firePoint.rotation); // instantiating bullet trail effect

        // Instantiating muzzleFlash Effect
        Transform cloneOfMuzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
        cloneOfMuzzleFlash.parent = firePoint;
        float size = Random.Range(0.6f, 0.9f);
        cloneOfMuzzleFlash.localScale = new Vector3(size, size, size);
        Destroy(cloneOfMuzzleFlash.gameObject , 0.02f);
    }
}
