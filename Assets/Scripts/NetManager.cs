using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : MonoBehaviour
{
    public static SimpleEvent<GetLeaderboardResult> Result = new SimpleEvent<GetLeaderboardResult>();

    private WaitForSeconds waitFor = new WaitForSeconds(30);

    private void Start()
    {
        DontDestroyOnLoad(this);

        Auth();
    }

    private IEnumerator Awaiter()
    {
        while (true)
        {
            UpdateLead();

            yield return waitFor;
        }
    }

    private void Auth()
    {
        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
        {
            CustomId = DataManager.instance.data.Id,
            CreateAccount = true
        },
        (result) =>
        {
            Debug.Log("Playfab login good");
            StartCoroutine(Awaiter());
        },
        (error) => { Debug.Log(error.ErrorMessage); });
    }

    public void UpdateNick(string val)
    {
        if (!PlayFabClientAPI.IsClientLoggedIn()) return;

        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = val
        },
        result => { },
        error => Debug.Log(error.GenerateErrorReport()));
    }

    public void UpdateLead()
    {
        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
        {
            StatisticName = "Top",
            StartPosition = 0,
            MaxResultsCount = 8
        },
        result =>
        {
            Result.Invoke(result);
        },
        error => Debug.LogError(error.GenerateErrorReport()));
    }

    public void UpdateRecord(int val)
    {
        if (!PlayFabClientAPI.IsClientLoggedIn()) return;

        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
            new StatisticUpdate {
                StatisticName = "Top",
                Value = val
            }
        }
        },
        result => { UpdateLead(); },
        error => Debug.LogError(error.GenerateErrorReport()));
    }
}
