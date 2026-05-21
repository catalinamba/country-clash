using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public string playerCountry;
    public TextMeshProUGUI textoPais;

    private Vector2 input;

    private Animator anim;
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (textoPais != null)
            textoPais.text = "País: " + playerCountry;
    }

    void Update()
    {
        input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        // Animación
        anim.SetBool("isMoving", input.magnitude > 0.1f);

        // Flip sprite
        if (input.x > 0)
            sr.flipX = false;
        else if (input.x < 0)
            sr.flipX = true;
    }

    void FixedUpdate()
    {
        // Movimiento con físicas
        rb.linearVelocity = input * moveSpeed;
    }
}
//void AnimacionMovimiento()
//{
//    float direction = transform.localScale.x >= 0 ? 1 : -1;

//    if (isMoving)
//    {
//        float scaleY = 1 + Mathf.Sin(Time.time * 10f) * 0.1f;
//        float scaleX = 1 - Mathf.Sin(Time.time * 10f) * 0.1f;

//        transform.localScale = new Vector3(direction * scaleX, scaleY, 1);
//    }
//    else
//    {
//        float scale = 1 + Mathf.Sin(Time.time * 3f) * 0.05f;

//        transform.localScale = new Vector3(direction * scale, scale, 1);
//    }
//}

//Mis endpoints en xampp local son:
//Guardar puntuación
//http://localhost/countryclash_api/saveScore.php
//Ranking
//http://localhost/countryclash_api/getRanking.php