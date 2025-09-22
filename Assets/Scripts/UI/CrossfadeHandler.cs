using UnityEngine;
using UnityEngine.Events;

public class CrossfadeHandler : MonoBehaviour
{
    [HideInInspector] public UnityEvent m_OnCrossfadeShow;

    private Animator anim;

    void Awake()
    {
        m_OnCrossfadeShow = new UnityEvent();
        anim = GetComponent<Animator>();
    }

    public void e_OnCrossfadeShown()
    {
        m_OnCrossfadeShow.Invoke();
    }

    public void Show(UnityAction callback)
    {
        m_OnCrossfadeShow.AddListener(callback);
        anim.SetBool("IsShown", true);
    }

    public void Hide()
    {
        m_OnCrossfadeShow.RemoveAllListeners();
        anim.SetBool("IsShown", false);
    }
}
