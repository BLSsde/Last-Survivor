
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float fireRate = 0;
    private float timeToFire = 0f;
    [SerializeField] private float bulletForce = 20f;

    [Header("Camera Shake")]
    // Shake the camera when we fire with the Powerful Gun
    [SerializeField] private float camShakeIntensity = 0.05f;
    [SerializeField] private float camShakeTime = 0.1f;


    [Header("Reference for Bullets")]
    // for Bullet 
    [SerializeField] private GameObject bulletsObj;
    [SerializeField] private Transform firePoint;

    private Rigidbody2D rbBullet;

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
        // animation set
  
        GameObject NewBullet = Instantiate(bulletsObj, firePoint.position, firePoint.rotation);
        rbBullet = NewBullet.GetComponent<Rigidbody2D>();
        rbBullet.AddForce(bulletForce * Time.deltaTime * firePoint.up , ForceMode2D.Impulse);

        AudioManager.instance.PlaySound("Shot");

        // Shake the Camera
        CameraShake.Instance.ShakeCamera(camShakeIntensity, camShakeTime);

        if(NewBullet.activeInHierarchy)
            Destroy(NewBullet, 0.5f);

    }
  

}
