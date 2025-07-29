using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class RocketScript : MonoBehaviour
{
    public float speed = 5f;
    public float rotateSpeed = 200f;
    public float lifetime = 5f;
    private Transform target;
    public AudioClip explosion;
    

    void Start()
    {
        target = FindClosestEnemy();
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (target == null) return;

        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        GetComponent<Rigidbody2D>().angularVelocity = -rotateAmount * rotateSpeed;
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closest = null;
        float minDist = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = enemy.transform;
            }
        }
        return closest;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyScript>(out EnemyScript es))
        {

            AudioSource.PlayClipAtPoint(explosion, transform.position);

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
