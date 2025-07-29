using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceZone : MonoBehaviour
{
    public float player_speed;
    public float enemy_speed;
    public float player_speed_normal;
    public float enemy_speed_normal;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerScript))
        {
            player_speed_normal = playerScript.moveSpeed;
            playerScript.moveSpeed = player_speed;
            playerScript.rotationSpeed *= 2;
            
        }
        if (collision.gameObject.TryGetComponent<EnemyFollower>(out EnemyFollower enemyScript))
        {
            enemy_speed_normal = enemyScript.speed;
            enemyScript.speed = enemy_speed;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController playerScript))
        {
           
            playerScript.moveSpeed = player_speed_normal;
            playerScript.rotationSpeed /= 2;

        }
        if (other.gameObject.TryGetComponent<EnemyFollower>(out EnemyFollower enemyScript))
        {
            enemyScript.speed = enemy_speed_normal;
        }
    }
}
