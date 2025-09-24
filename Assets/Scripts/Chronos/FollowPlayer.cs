using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float MovementSpeed = 9f;
    public float MinDistance = 2f;

    private Transform _player;
    private Animator _anim;
    private SpriteRenderer _sr;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (_player != null)
        {
            float distance = Vector3.Distance(transform.position, _player.position);
            if (distance > MinDistance)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    _player.position,
                    MovementSpeed * Time.deltaTime
                );

                Vector3 direction = (_player.position - transform.position).normalized;
                if (direction.x > 0)
                {
                    _sr.flipX = false;
                }
                else if (direction.x < 0)
                {
                    _sr.flipX = true;
                }
                _anim.SetFloat("Speed", 1);
            }
            else
            {
                _anim.SetFloat("Speed", 0);
            }
        }
    }

    public void SetPlayer(Transform player)
    {
        _player = player;
    }
}
