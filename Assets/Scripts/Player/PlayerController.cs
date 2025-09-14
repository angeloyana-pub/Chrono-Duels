using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    [HideInInspector] public bool isFacingRight = false;

    private Animator anim;
    private SpriteRenderer sr;

    void Start()
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
            FaceRight();
        }
        else if (moveX < 0)
        {
            FaceLeft();
        }

        Vector3 move = transform.right * moveX + transform.forward * moveY;
        transform.Translate(move * speed * Time.deltaTime);
    }

    public void FaceLeft()
    {
        sr.flipX = true;
        isFacingRight = false;
    }

    public void FaceRight()
    {
        sr.flipX = false;
        isFacingRight = true;
    }

    public void Enable()
    {
        enabled = true;
    }

    public void Disable()
    {
        enabled = false;
        anim.SetFloat("Speed", 0);
    }
}
