using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using YG.Utils.LB;

public class TopManager : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject prefab;

    private Regex reg = new Regex(@"\d+\.\s\S+:\s\d+");
    private Regex smallReg = new Regex(@"\s\d+");

    private void Awake()
    {
        NetManager.Result.AddListener(Top);
    }

    private void Top(LBData lb)
    {
        if (lb.entries == "no data") return;

        lb.entries = lb.entries.Replace("anonymous", "Сизиф");

        for (int i = parent.childCount - 1; i >= 0; i--)
        { 
            Destroy(parent.GetChild(i).gameObject);
        }

        var obj = Instantiate(prefab, parent, false);

        string result = "";

        var ma = reg.Matches(lb.entries);

        foreach (Match match in ma)
        {
            string str = match.Value;

            float val = float.Parse(smallReg.Match(str).Value) / 1000;

            result += smallReg.Replace(str, $" {val:f2}") + "\n";
        }

        obj.GetComponent<TMP_Text>().text = result;
    }

    private void OnDestroy()
    {
        NetManager.Result.RemoveListener(Top);
    }
}
