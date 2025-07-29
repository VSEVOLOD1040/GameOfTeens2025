using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyScript : MonoBehaviour
{
    public float lifetime = 2f;
    public BulletEnemyAttack controller;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerScript>(out PlayerScript player))
        {
            player.TakeDamage(controller.damage);
        }

        Destroy(gameObject);
    }
}
