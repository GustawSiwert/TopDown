using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime); //Förstör bullet efter att lifetime är slut
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       
        EnemyHealthManager enemy = other.GetComponent<EnemyHealthManager>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject); //Förstör bullet om den träffar enemy
        }
    }
}