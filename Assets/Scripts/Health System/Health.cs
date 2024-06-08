using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private MoveDeadMob moveDeadMob;
    [HideInInspector] public bool isTakingDamage = false;

    private void Start()
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
        healthSlider.gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        isTakingDamage = true;
        health -= damage;
        healthSlider.value = health;

        if (health <= 0)
        {
            moveDeadMob.enabled = true;
        }
    }

    public void StopTakingDamage()
    {
        isTakingDamage = false;
    }

    private void Update()
    {
        if (isTakingDamage)
        {
            healthSlider.gameObject.SetActive(true);
        }
        else
        {
            healthSlider.gameObject.SetActive(false);
        }
    }
}
