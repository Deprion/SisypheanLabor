using UnityEngine;

public class End : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rock"))
        {
            Events.End.Invoke();
        }
    }
}
