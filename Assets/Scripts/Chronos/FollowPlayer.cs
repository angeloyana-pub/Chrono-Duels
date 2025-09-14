using UnityEngine;

class FollowPlayer : MonoBehaviour
{
    public float speed = 4f;
    public float minDistance = 0.5f;

    private Transform player;
    private Animator anim;
    private SpriteRenderer sr;

    void Start()
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
                    FaceRight();
                }
                else if (direction.x < 0)
                {
                    FaceLeft();
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

    public void FaceLeft()
    {
        sr.flipX = true;
    }

    public void FaceRight()
    {
        sr.flipX = false;
    }
}
