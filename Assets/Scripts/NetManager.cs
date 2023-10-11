using System.Collections;
using UnityEngine;
using YG;
using YG.Utils.LB;

public class NetManager : MonoBehaviour
{
    [SerializeField] private string lbName;

    public static SimpleEvent<LBData> Result = new SimpleEvent<LBData>();

    private WaitForSeconds waitFor = new WaitForSeconds(30);

    private void Start()
    {
        DontDestroyOnLoad(this);

        YandexGame.onGetLeaderboard += Result.Invoke;

        StartCoroutine(Awaiter());
    }

    private IEnumerator Awaiter()
    {
        yield return new WaitForSeconds(3);

        while (true)
        {
            RefreshLead();

            yield return waitFor;
        }
    }

    public void RefreshLead()
    {
        YandexGame.GetLeaderboard(lbName, 8, 8, 8, "nonePhoto");
    }

    public void UpdateRecord(int val)
    {
        if (!YandexGame.auth)
        {
            //YandexGame.AuthDialog();
            return;
        }

        YandexGame.NewLBScoreTimeConvert(lbName, val);
    }

    private void OnDestroy()
    {
        YandexGame.onGetLeaderboard -= Result.Invoke;
    }
}
