using UnityEngine;

public class BulletEnemyAttack : EnemyAttack
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    private float fireTimer;
    public float damage;
    private void Update()
    {
        fireTimer += Time.deltaTime;
    }

    public override void PerformAttack(Transform target)
    {
        if (fireTimer >= fireRate)
        {
            fireTimer = 0f;
            Vector2 direction = (target.position - firePoint.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * 5f;
            bullet.GetComponent<BulletEnemyScript>().controller = this;
        }
    }
}
