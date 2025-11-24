using UnityEngine;

public class FirePoint : MonoBehaviour
{
    public Transform player;
    public float distanceFromPlayer = 1.0f; //Hur långt från spelaren firepoint ska vara

    void Update()
    {
        //Hämta mus positionen från världens storlek
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        //Hämta riktningen musen har i relation till spelaren
        Vector3 direction = (mousePos - player.position).normalized;
        transform.position = player.position + direction * distanceFromPlayer;

        //Rotera firepoint till att ha anisktet mot musen
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}