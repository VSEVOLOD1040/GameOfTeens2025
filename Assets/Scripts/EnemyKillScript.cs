using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillScript : MonoBehaviour
{
    public WeaponController controller;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<EnemyScript>(out EnemyScript enemyScript))
        {
            enemyScript.TakeDamage(controller.bullet_damage);
        }
    }
}
