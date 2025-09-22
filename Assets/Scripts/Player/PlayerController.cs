using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    private Animator anim;
    private SpriteRenderer sr;

    void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        anim.SetFloat("Speed", moveX == 0 && moveY == 0 ? 0 : 1);
        if (moveX > 0)
        {
            sr.flipX = false;
        }
        else if (moveX < 0)
        {
            sr.flipX = true;
        }

        Vector3 move = transform.right * moveX + transform.forward * moveY;
        transform.Translate(move * speed * Time.deltaTime);
    }
    
    void OnDisable()
    {
        anim.SetFloat("Speed", 0);
    }
}
