using UnityEngine;

class FollowPlayer : MonoBehaviour {
    public Transform player;
    public float speed = 4.5f;
    public float minDistance = 0.7f;
    public bool disabled = false;
    
    private Animator anim;
    private SpriteRenderer sr;
    
    void Start() {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    
    void Update() {
        float distance = Vector3.Distance(transform.position, player.position);
        if (!disabled && player != null) {
            if (distance > minDistance) {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    player.position,
                    speed * Time.deltaTime
                );
                
                Vector3 direction = (player.position - transform.position).normalized;
                if (direction.x > 0) {
                    sr.flipX = true;
                } else if (direction.x < 0) {
                    sr.flipX = false;
                }
                anim.SetFloat("Speed", 1);
            } else {
                anim.SetFloat("Speed", 0);
            }
        }
    }
    
    public void SetPlayer(Transform player) {
        this.player = player;
    }
    
    public void Enable() {
        disabled = false;
    }
    
    public void Disable() {
        disabled = true;
    }
}
