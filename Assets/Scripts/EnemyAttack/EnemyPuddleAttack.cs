using UnityEngine;
using System.Collections.Generic;

public class EnemyPuddleAttack : EnemyAttack
{
    public List<GameObject> puddlePrefabs = new List<GameObject>();
    public float minDistance = 6f;
    public float maxDistance = 10f;
    public LayerMask groundLayer;
    public float attackCooldown = 15f;
    private float lastAttackTime = -Mathf.Infinity;
    public float TimeOfExistence;
    public override void PerformAttack(Transform target)
    {
        if (Time.time - lastAttackTime < attackCooldown)
            return;

        lastAttackTime = Time.time;

        if (puddlePrefabs.Count == 0)
            return;

        float angle = Random.Range(0f, 360f);
        float distance = Random.Range(minDistance, maxDistance);

        Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        Vector3 spawnPosition = transform.position + new Vector3(direction.x, direction.y, 0) * distance;

        RaycastHit2D hit = Physics2D.Raycast(spawnPosition + Vector3.up * 2, Vector2.down, 5f, groundLayer);
        if (hit.collider != null)
        {
            spawnPosition.y = hit.point.y;
        }

        GameObject puddlePrefab = puddlePrefabs[Random.Range(0, puddlePrefabs.Count)];
        GameObject inst = Instantiate(puddlePrefab, spawnPosition, Quaternion.identity);
        Destroy(inst, TimeOfExistence);
    }
}

