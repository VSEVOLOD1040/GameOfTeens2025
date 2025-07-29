using UnityEngine;

public class WarriorAttack : EnemyAttack
{
    public float attackCooldown = 1.0f;
    public float damageAmount = 10f;
    public float attackRange = 1.5f;

    private float lastAttackTime = -Mathf.Infinity;

    public override void PerformAttack(Transform target)
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget > attackRange)
            return;

        if (Time.time - lastAttackTime < attackCooldown)
            return;

        lastAttackTime = Time.time;

        PlayerScript player = target.GetComponent<PlayerScript>();
        if (player != null)
        {
            player.TakeDamage(damageAmount);
        }
    }
}
