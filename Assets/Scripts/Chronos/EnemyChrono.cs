using UnityEngine;

public class EnemyChrono : MonoBehaviour
{
    public ChronoStats stats;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void FaceLeft()
    {
        sr.flipX = true;
    }
}
