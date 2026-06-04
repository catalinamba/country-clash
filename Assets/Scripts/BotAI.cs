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

        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        rb.freezeRotation = true;

        PickNewWanderTarget();
    }

    void Update()
    {
        FindTarget();

        if (target != null)
        {
            MoveTo(target.position);
        }
        else
        {
            Wander();
        }

        UpdateAnimation();
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            if (Vector2.Distance(rb.position, wanderTarget) < 0.3f)
            {
                PickNewWanderTarget();
            }
        }
    }

    void MoveTo(Vector2 pos)
    {
        Vector2 dir = pos - rb.position;

        if (dir.magnitude < 0.2f)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = dir.normalized * speed;
    }

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

    void FindTarget()
    {
        float minDist = Mathf.Infinity;
        Collectible closest = null;

        foreach (Collectible c in CollectibleManager.all)
        {
            if (c == null) continue;

            if (c.countryName != targetCountry) continue;

            float dist = Vector2.Distance(rb.position, c.transform.position);

            if (dist < minDist)
            {
                minDist = dist;
                closest = c;
            }
        }

        target = closest != null ? closest.transform : null;
    }

    void UpdateAnimation()
    {
        bool moving = Vector2.Distance(rb.position, lastPos) > 0.01f;
        anim.SetBool("isMoving", moving);

        lastPos = rb.position;

        Vector2 dir = rb.linearVelocity;

        if (dir.x > 0.1f)
            sr.flipX = false;
        else if (dir.x < -0.1f)
            sr.flipX = true;
    }
}