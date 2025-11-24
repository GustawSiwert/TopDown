using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    private float hurtTimer = 0f;
    private bool isTouching = false;
    private HealtManager healthManager;
    public int damageToGive = 10;
    public float damageInterval = 2f;

    void Start()
    {
        healthManager = FindFirstObjectByType<HealtManager>();
    }

    void Update()
    {
        if (isTouching)
        {
            hurtTimer -= Time.deltaTime;
            if (hurtTimer <= 0f)
            {
                healthManager.HurtPlayer(damageToGive);
                hurtTimer = damageInterval;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            isTouching = true;
            hurtTimer = 0f;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            isTouching = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            isTouching = false;
            hurtTimer = 0f;
        }
    }
}