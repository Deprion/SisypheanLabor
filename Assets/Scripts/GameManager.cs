using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text bestTimeTxt;

    private void Awake()
    {
        Events.End.AddListener(End);

        if (PlayerPrefs.HasKey("Time"))
        {
            bestTimeTxt.text = $"Best: {PlayerPrefs.GetFloat("Time"):f2}";
        }
        else
            bestTimeTxt.text = null;
    }

    private void End()
    {
        GetComponent<ScoreManager>().isEnd = true;
        float time = GetComponent<ScoreManager>().time;

        var net = GameObject.FindGameObjectWithTag("Net").GetComponent<NetManager>();

        if (PlayerPrefs.HasKey("Time"))
        {
            if (PlayerPrefs.GetFloat("Time") > time)
            {
                PlayerPrefs.SetFloat("Time", time);
                bestTimeTxt.text = $"Best: {time:f2}";
                net.UpdateRecord((int)(time * 100));
            }
        }
        else
        {
            PlayerPrefs.SetFloat("Time", time);
            bestTimeTxt.text = $"Best: {time:f2}";
            net.UpdateRecord((int)(time * 100));
        }
    }
    private void OnDestroy()
    {
        Events.End.RemoveListener(End);
    }
}
