using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float minDistance = 0.7f;
    public float speed;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (speed == 0f)
        {
            speed = player.GetComponent<PlayerController>().speed - 0.5f;
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > minDistance)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );
            anim.SetFloat("Speed", 1);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }

        Vector3 dir = player.position - transform.position;
        if (dir.x > 0)
        {
            FaceRight();
        }
        else if (dir.x < 0)
        {
            FaceLeft();
        }
    }

    void FaceLeft()
    {
        Vector3 newScale = transform.localScale;
        newScale.x = -Mathf.Abs(transform.localScale.x);
        transform.localScale = newScale;
    }

    void FaceRight()
    {
        Vector3 newScale = transform.localScale;
        newScale.x = Mathf.Abs(transform.localScale.x);
        transform.localScale = newScale;
    }
}
