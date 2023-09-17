using TMPro;
using UnityEngine;

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

        time += Time.deltaTime;

        textTxt.text = $"Height: {(target.localPosition.y - 1.5) * 125:f0}";
        timeTxt.text = $"Time: {time:f2}";
    }
}
