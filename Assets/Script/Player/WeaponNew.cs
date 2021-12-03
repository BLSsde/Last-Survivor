using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNew : MonoBehaviour
{

    public float fireRate = 0;
    private float timeToFire = 0f;


    // for Bullet 
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform hitPrefab;
    
    // Shake the camera when we fire with the Powerful Gun
    [SerializeField] private float camShakeAmnt = 0.05f;
    [SerializeField] private float camShakeLength = 0.1f;
    private CameraShake camShake;

    // for Muzzle Flash
    public Transform muzzleFlashPrefab;


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

        GameObject NewBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Destroy(NewBullet.gameObject, 4f);

        // Instantiating muzzleFlash Effect
        Transform cloneOfMuzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
        cloneOfMuzzleFlash.parent = firePoint;
        float size = Random.Range(0.6f, 0.9f);
        cloneOfMuzzleFlash.localScale = new Vector3(size, size, size);
        Destroy(cloneOfMuzzleFlash.gameObject, 0.02f);


        // Shake the Camera
        camShake.Shake(camShakeAmnt, camShakeLength);
    }

  
}
