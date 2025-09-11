using UnityEngine;

class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
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
