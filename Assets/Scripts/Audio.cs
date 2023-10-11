using UnityEngine;

public class Audio : MonoBehaviour
{
    private bool started = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        Events.AudioStart.AddListener(Begin);
    }

    private void Begin()
    {
        if (started) return;

        started = true;

        GetComponent<AudioSource>().Play();
    }

    private void OnDestroy()
    {
        Events.AudioStart.RemoveListener(Begin);
    }
}
