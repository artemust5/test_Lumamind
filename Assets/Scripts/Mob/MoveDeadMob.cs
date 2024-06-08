using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDeadMob : MonoBehaviour
{
    [SerializeField] private float radius = 1f;
    [SerializeField] private float height = 1f;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float deadMobSpeed = 15f;
    [SerializeField] private MobController mobController;
    [SerializeField] private Animator animator;
    private Collider mobCollider;
    private Sequence sequence;


    private void Start()
    {
        mobController.StopMoving();
        mobCollider = GetComponent<Collider>();
        mobCollider.enabled = false;
        animator.enabled = false;

        Vector3 randomPointInRadius = Random.insideUnitSphere * radius;
        randomPointInRadius.y = height;

        sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalJump(transform.position + randomPointInRadius, height, 1, duration));

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            Collider player = playerObject.GetComponent<Collider>();
            if (player != null)
            {
                sequence.AppendCallback(() => FollowPlayer(player));
            }
        }
    }

    private void FollowPlayer(Collider player)
    {
        Vector3 edge = player.bounds.center + player.bounds.extents;
        Tweener tweener = transform.DOMove(edge, 0.15f).SetEase(Ease.Linear);
        tweener.OnComplete(() => {
            StartCoroutine(MoveToCenter(player));
        });
        sequence.Append(tweener);
    }

    private IEnumerator MoveToCenter(Collider player)
    {
        transform.SetParent(player.transform);
        while (Vector3.Distance(transform.localPosition, Vector3.zero) > 0.55f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, deadMobSpeed * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
        sequence.Kill();
    }
}
