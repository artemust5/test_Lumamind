using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MobController : MonoBehaviour
{
    [SerializeField] private float moveRadius = 5f;
    [SerializeField] private float moveDelay = 2f;
    [SerializeField] private float avoidDistance = 1f;
    [SerializeField] private Animator animator;
    
    private NavMeshAgent agent;
    private Vector3 targetPosition;
    private bool isMoving = true;
    private Coroutine moveCoroutine;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        moveCoroutine = StartCoroutine(MoveRandomly());
    }

    IEnumerator MoveRandomly()
    {
        while (isMoving)
        {
            targetPosition = MobHitAvoiding();

            while ((transform.position - targetPosition).magnitude > agent.stoppingDistance)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, avoidDistance);
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject != gameObject && collider.gameObject.CompareTag("Mob"))
                    {
                        targetPosition = MobHitAvoiding();
                        break;
                    }
                }

                agent.SetDestination(targetPosition);
                animator.SetBool("isMoving", true);
                yield return null;
            }
            animator.SetBool("isMoving", false);
            yield return new WaitForSeconds(moveDelay);
            
        }
    }

    private Vector3 MobHitAvoiding()
    {
        Vector3 randomDirection = Random.insideUnitSphere * moveRadius;
        randomDirection.y = 0;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, moveRadius, 1);
        return hit.position;
    }
    public void StopMoving()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
            isMoving = false;
        }
    }
}
