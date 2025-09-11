using UnityEngine;
using Unity.Cinemachine;

public class BattleTrigger : MonoBehaviour
{
    public CinemachineCamera battleCamera;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered battle field.");
            battleCamera.Priority = 5;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited battle field.");
            battleCamera.Priority = 0;
        }
    }
}
