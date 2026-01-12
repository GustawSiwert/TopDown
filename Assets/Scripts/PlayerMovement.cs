using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    [SerializeField] KeyCode moveForward = KeyCode.W;
    [SerializeField] KeyCode moveBackward = KeyCode.S;
    [SerializeField] KeyCode moveLeft = KeyCode.A;
    [SerializeField] KeyCode moveRight = KeyCode.D;

    void Update()
    {
        RotateToCursor();
        Movement();
    }

    void RotateToCursor()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = mouseWorld - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }



    void Movement()
    {
        if (Input.GetKey(moveForward))
        {
            transform.position += moveSpeed * Time.deltaTime * transform.up;
        }

        if (Input.GetKey(moveBackward))
        {
            transform.position -= moveSpeed * Time.deltaTime * transform.up;
        }

        if (Input.GetKey(moveLeft))
        {
            transform.position -= moveSpeed * Time.deltaTime * transform.right;
        }

        if (Input.GetKey(moveRight))
        {
            transform.position += moveSpeed * Time.deltaTime * transform.right;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "NextLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
