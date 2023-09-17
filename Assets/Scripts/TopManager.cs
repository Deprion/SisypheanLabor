using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class TopManager : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject prefab;

    private void Awake()
    {
        NetManager.Result.AddListener(Top, true);
    }

    private void Top(GetLeaderboardResult res)
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
        { 
            Destroy(parent.GetChild(i).gameObject);
        }

        for (int i = 0; i < res.Leaderboard.Count; i++)
        {
            var obj = Instantiate(prefab, parent, false);
            string name;
            if (!string.IsNullOrEmpty(res.Leaderboard[i].DisplayName))
                name = res.Leaderboard[i].DisplayName;
            else
                name = "Sisyphus";

            obj.GetComponent<TMP_Text>().text = $"{name} {(float)res.Leaderboard[i].StatValue / 100}";
        }
    }

    private void OnDestroy()
    {
        NetManager.Result.RemoveListener(Top);
    }
}
