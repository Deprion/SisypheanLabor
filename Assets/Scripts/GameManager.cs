using TMPro;
using UnityEngine;
using YG;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text bestTimeTxt;
    [SerializeField] private GameObject endMenu;

    private void Awake()
    {
        if (!Global.YReady)
        {
            YandexGame.GameReadyAPI();
            Global.YReady = true;
        }

        Events.End.AddListener(End);

        if (PlayerPrefs.HasKey("Time"))
        {
            bestTimeTxt.text = $"Рекорд: {PlayerPrefs.GetFloat("Time"):f2}";
        }
        else
            bestTimeTxt.text = null;
    }

    private void End()
    {
        GetComponent<ScoreManager>().isEnd = true;
        float time = GetComponent<ScoreManager>().time;

        var net = GameObject.FindGameObjectWithTag("Net").GetComponent<NetManager>();

        endMenu.SetActive(true);

        if (PlayerPrefs.HasKey("Time"))
        {
            if (PlayerPrefs.GetFloat("Time") > time)
            {
                PlayerPrefs.SetFloat("Time", time);
                bestTimeTxt.text = $"Рекорд: {time:f2}";
                net.UpdateRecord((int)(time * 1000)); // 3.21 * 100 => 00:321
                endMenu.GetComponent<EndMenu>().SetText
                    ($"Ваше лучшее время: {time:f2}\nВы побили свой рекорд!!!");
            }
            else
            {
                endMenu.GetComponent<EndMenu>().SetText
                    ($"Ваше время: {time:f2}\nВаше лучшее время: {PlayerPrefs.GetFloat("Time"):f2}");
            }
        }
        else
        {
            PlayerPrefs.SetFloat("Time", time);
            bestTimeTxt.text = $"Рекорд: {time:f2}";
            net.UpdateRecord((int)(time * 1000));
            endMenu.GetComponent<EndMenu>().SetText($"Ваше лучшее время: {time:f2}\nТак держать!!!");
        }

    }
    private void OnDestroy()
    {
        Events.End.RemoveListener(End);
    }
}
