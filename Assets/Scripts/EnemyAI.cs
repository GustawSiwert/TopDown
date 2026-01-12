using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 2f;
    [SerializeField] float roamRadius = 3f;
    [SerializeField] float waitBeforeMoving = 2f;

    [Header("Detection")]
    [SerializeField] Collider2D detectionCollider;

    Transform player;
    Vector2 roamTarget;
    bool isChasing;

    void Start()
    {
        PickNewRoamTarget();
        StartCoroutine(RoamRoutine());
    }

    void Update()
    {
        if (isChasing && player != null)
        {
            // Chase player
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );
        }
        else
        {
            // Roam
            transform.position = Vector2.MoveTowards(
                transform.position,
                roamTarget,
                speed * Time.deltaTime
            );
        }
    }

    void PickNewRoamTarget()
    {
        roamTarget = (Vector2)transform.position +
                     Random.insideUnitCircle * roamRadius;
    }

    IEnumerator RoamRoutine()
    {
        while (!isChasing)
        {
            yield return new WaitForSeconds(waitBeforeMoving);
            PickNewRoamTarget();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            isChasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = false;
            player = null;
            PickNewRoamTarget();
            StartCoroutine(RoamRoutine());
        }
    }
}
