using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireZone : MonoBehaviour
{
    public float player_damage;
    public float enemy_daamge;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerScript>(out PlayerScript playerScript))
        {
            playerScript.TakeDamage(player_damage);
        }
        if (collision.gameObject.TryGetComponent<EnemyScript>(out EnemyScript enemyScript))
        {
            enemyScript.TakeDamage(enemy_daamge);
        }
    }
}
