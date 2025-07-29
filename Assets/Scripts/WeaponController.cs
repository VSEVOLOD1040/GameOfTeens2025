using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject laserObject;

    public float bulletSpeed = 10f;
    public float fireRate;
    private float fireTimer = 0f;

    private int attackMode = 1;

    public float bullet_damage;
    public float laser_damage;

    public GameObject rocketPrefab;
    public Transform rocketSpawnPoint;
    public float rocketCooldown = 2f;
    private float rocketTimer = 0f;

    private float lastHorizontalInput = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) attackMode = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) attackMode = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) attackMode = 3;

        float horizontalInput = Input.GetAxis("Horizontal");

        bool directionChanged = Mathf.Sign(horizontalInput) != Mathf.Sign(lastHorizontalInput) && horizontalInput != 0;

        if (attackMode == 1)
        {
            laserObject.SetActive(false);

            if (directionChanged)
            {
                FireBullet();
                fireTimer = 0f;
            }
        }
        else if (attackMode == 2)
        {
            laserObject.SetActive(true);
        }
        else if (attackMode == 3)
        {
            laserObject.SetActive(false);

            if (directionChanged && rocketTimer <= 0f)
            {
                LaunchRocket();
                rocketTimer = rocketCooldown;
            }
        }

        rocketTimer -= Time.deltaTime;
        lastHorizontalInput = horizontalInput;
    }

    void LaunchRocket()
    {
        Instantiate(rocketPrefab, rocketSpawnPoint.position, rocketSpawnPoint.rotation);
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().controller = this;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * bulletSpeed;
    }
}
