using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float speed = 9f;
    public float minDistance = 2f;

    private Transform player;
    private Animator anim;
    private SpriteRenderer sr;

    void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > minDistance)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    player.position,
                    speed * Time.deltaTime
                );

                Vector3 direction = (player.position - transform.position).normalized;
                if (direction.x > 0)
                {
                    sr.flipX = false;
                }
                else if (direction.x < 0)
                {
                    sr.flipX = true;
                }
                anim.SetFloat("Speed", 1);
            }
            else
            {
                anim.SetFloat("Speed", 0);
            }
        }
    }

    public void SetPlayer(Transform player)
    {
        this.player = player;
    }
}
