using UnityEngine;
using Pathfinding;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(AIDestinationSetter), typeof(AIPath))]
public class EnemyFollower : MonoBehaviour
{
    public float speed = 3f;
    public float stopDistance = 1.5f;

    private AIPath aiPath;
    private AIDestinationSetter destinationSetter;
    private Transform player;

    void Start()
    {
        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        aiPath.maxSpeed = speed;

        StartCoroutine(FindPlayerLater());
    }

    IEnumerator FindPlayerLater()
    {
        yield return new WaitForSeconds(5f);
        while (player == null)
        {
            GameObject found = GameObject.FindObjectsOfType<GameObject>()
                .FirstOrDefault(obj => obj.name.Contains("Player"));

            if (found != null)
            {
                player = found.transform;
                destinationSetter.target = player;
                yield break;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    void Update()
    {
        if (player == null)
        {
            StartCoroutine(FindPlayerLater());
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= stopDistance)
        {
            aiPath.isStopped = true;
        }
        else
        {
            aiPath.isStopped = false;
        }
    }
}
