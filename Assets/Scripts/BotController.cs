using UnityEngine;

public class BotControllerSimple : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        GameObject target = null;
        GameObject[] objetos = GameObject.FindGameObjectsWithTag("Objeto");

        if (objetos.Length > 0)
            target = objetos[0]; // simplemente toma el primero disponible

        if (target != null)
        {
            Vector2 newPos = Vector2.MoveTowards(rb.position, target.transform.position, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
    }
}