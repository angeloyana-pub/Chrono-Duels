using UnityEngine;

public class BattleChrono : MonoBehaviour
{
    public float speed = 3f;
    public Vector3 destination;
    public bool hasDestination = false;
    public bool flipXOnArrive = false;

    private Animator anim;
    private SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasDestination)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                destination,
                speed * Time.deltaTime
            );

            Vector3 direction = (destination - transform.position).normalized;
            if (direction.x > 0)
            {
                sr.flipX = false;
            }
            else if (direction.x < 0)
            {
                sr.flipX = true;
            }

            anim.SetFloat("Speed", 1);
            if (transform.position == destination)
            {
                hasDestination = false;
                anim.SetFloat("Speed", 0);
                sr.flipX = flipXOnArrive;
            }
            return;
        }
    }

    public void SetDestination(Vector3 destination, bool flipXOnArrive = false)
    {
        this.destination = destination;
        this.flipXOnArrive = flipXOnArrive;
        hasDestination = true;
    }
}
