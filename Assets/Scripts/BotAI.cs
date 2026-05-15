using UnityEngine;

public class BotAI : MonoBehaviour
{
    public string targetCountry;
    public float speed = 2f;

    private Rigidbody2D rb;
    private Transform target;

    private Vector2 wanderTarget;
    private float wanderTimer;

    private Animator anim;
    private SpriteRenderer sr;

    private Vector2 lastPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        PickNewWanderTarget();
    }

    void Update()
    {
        FindTarget();

        // =========================
        // MOVIMIENTO
        // =========================
        if (target != null)
            MoveTo(target.position);
        else
            Wander();

        // =========================
        // ANIMACIÓN
        // =========================
        bool moving = Vector2.Distance(rb.position, lastPos) > 0.01f;
        anim.SetBool("isMoving", moving);

        lastPos = rb.position;

        // =========================
        // FLIP SPRITE
        // =========================
        Vector2 dir = (target != null)
            ? (Vector2)target.position - rb.position
            : wanderTarget - rb.position;

        if (dir.x > 0.1f)
            sr.flipX = false;
        else if (dir.x < -0.1f)
            sr.flipX = true;
    }

    void FixedUpdate()
    {
        // Si no hay target ni wander, parar
        if (target == null && Vector2.Distance(rb.position, wanderTarget) < 0.2f)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    // =========================
    // MOVIMIENTO
    // =========================
    void MoveTo(Vector2 pos)
    {
        Vector2 dir = (pos - rb.position).normalized;
        rb.linearVelocity = dir * speed;
    }

    // =========================
    // EXPLORAR
    // =========================
    void Wander()
    {
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0f || Vector2.Distance(rb.position, wanderTarget) < 0.2f)
        {
            PickNewWanderTarget();
        }

        MoveTo(wanderTarget);
    }

    void PickNewWanderTarget()
    {
        wanderTarget = new Vector2(
            Random.Range(-8f, 8f),
            Random.Range(-4f, 4f)
        );

        wanderTimer = Random.Range(2f, 5f);
    }

    // =========================
    // BUSCAR OBJETOS
    // =========================
    void FindTarget()
    {
        GameObject[] all = GameObject.FindGameObjectsWithTag("Collectible");

        float minDist = Mathf.Infinity;
        GameObject closest = null;

        foreach (GameObject obj in all)
        {
            Collectible c = obj.GetComponent<Collectible>();

            if (c != null && c.countryName == targetCountry)
            {
                float dist = Vector2.Distance(rb.position, obj.transform.position);

                if (dist < minDist)
                {
                    minDist = dist;
                    closest = obj;
                }
            }
        }

        target = (closest != null) ? closest.transform : null;
    }
}