using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MovementSpeed = 10f;

    private Animator _anim;
    private SpriteRenderer _sr;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        _anim.SetFloat("Speed", moveX == 0 && moveY == 0 ? 0 : 1);
        if (moveX > 0)
        {
            _sr.flipX = false;
        }
        else if (moveX < 0)
        {
            _sr.flipX = true;
        }

        Vector3 move = transform.right * moveX + transform.forward * moveY;
        transform.Translate(move * MovementSpeed * Time.deltaTime);
    }
    
    void OnDisable()
    {
        _anim.SetFloat("Speed", 0);
    }
}
