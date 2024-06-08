using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private float attackRadius = 5f;
    [SerializeField] private float damagePerSecond = 20f;

    private List<Health> targets = new List<Health>();
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = lineRenderer.endWidth = 0.1f;
    }

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius);
        foreach (var hitCollider in hitColliders)
        {
            Health healthScript = hitCollider.GetComponent<Health>();
            if (healthScript != null && !targets.Contains(healthScript) && targets.Count < 2)
            {
                targets.Add(healthScript);
                healthScript.isTakingDamage = true;
            }
        }

        for (int i = targets.Count - 1; i >= 0; i--)
        {
            Health target = targets[i];
            if (target == null || Vector3.Distance(transform.position, target.transform.position) > attackRadius)
            {
                target.StopTakingDamage();
                targets.RemoveAt(i);
            }
            else
            {
                target.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }

        lineRenderer.positionCount = targets.Count * 2;
        for (int i = 0; i < targets.Count; i++)
        {
            Vector3 elevatedPosition = transform.position;
            elevatedPosition.y += 2; 

            Vector3 targetPosition = targets[i].transform.position;
            targetPosition.y += 1;

            lineRenderer.SetPosition(i * 2, elevatedPosition);
            lineRenderer.SetPosition(i * 2 + 1, targetPosition);
        }
    }
}
