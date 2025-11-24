using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Här kan du lägga till en animation, tappa loot och så vidare. 
        Destroy(gameObject);
    }
}