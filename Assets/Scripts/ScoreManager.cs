using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text textTxt, timeTxt;

    private Transform target;

    public float time;

    public bool isEnd;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Rock").transform;

        time = 0;
    }

    private void Update()
    {
        if (isEnd) return;

        if (target.transform.localPosition.y < -10) SceneManager.LoadScene(1);

        time += Time.deltaTime;

        textTxt.text = $"Высота: {(target.localPosition.y - 1.5) * 125:f0}";
        timeTxt.text = $"Время: {time:f2}";
    }
}
