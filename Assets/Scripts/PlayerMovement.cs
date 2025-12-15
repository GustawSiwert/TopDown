using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    public Animator animator;

    void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x < 0) // x is less than 0 
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (movement.x > 0) // x is greater than 0
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (movement.y < 0) // y is less than 0
        {
            transform.rotation = Quaternion.Euler(0, 0, -180);
        }
        else if (movement.y > 0) // y is greater than 0
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        

        if (movement.sqrMagnitude > 1)
        {
            movement.Normalize();
        }

        if (movement.sqrMagnitude > 0.1f)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                animator.SetFloat("Last-Horizontal", movement.x);
                animator.SetFloat("Last-Vertical", 0);
            }
            else
            {
                animator.SetFloat("Last-Horizontal", 0);
                animator.SetFloat("Last-Vertical", movement.y);
            }
        }
        else
        {
            animator.SetFloat("Speed", 0);
            animator.SetFloat("Horizontal", animator.GetFloat("Last-Horizontal"));
            animator.SetFloat("Vertical", animator.GetFloat("Last-Vertical"));
        }
    }

    void FixedUpdate()
    {
        // Use Rigidbody2D.velocity for smooth physics-based movement
        rb.linearVelocity = movement * moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "NextLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
