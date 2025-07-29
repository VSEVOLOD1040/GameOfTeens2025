using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Animator animator;

    private Vector3 lastPosition;
    private float moveThreshold = 0.01f;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        float distance = Vector3.Distance(currentPosition, lastPosition);

        bool isMoving = distance > moveThreshold;
        animator.SetBool("IsMoving", isMoving);



    }

}
