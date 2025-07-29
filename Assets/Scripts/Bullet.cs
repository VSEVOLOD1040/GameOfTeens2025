using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 2f;
    public WeaponController controller;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyScript>(out EnemyScript enemyScript))
        {
            enemyScript.TakeDamage(controller.bullet_damage);

        }

        Destroy(gameObject);
    }
}
