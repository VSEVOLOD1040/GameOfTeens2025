using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public float health;
    public GameObject ParticlesOnDestroy;
    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health-=damage;
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<UIManager>().PlayEffectFromAnotherScript(Color.green, 0.2f, 0.1f);

            Data.DestroyedEnemies += 1;
            GameObject.Find("Canvas").GetComponent<UIManager>().SetTextDestroyedEnemies($"Destroyed enemies: {Data.DestroyedEnemies}");
            
            GameObject particle = Instantiate(ParticlesOnDestroy, gameObject.transform.position, quaternion.identity);
            Destroy(particle,2);

            health = 0;
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        gameObject.GetComponent<EnemyAttack>().PerformAttack(GameObject.Find("Player(Clone)").transform);
    }
}
