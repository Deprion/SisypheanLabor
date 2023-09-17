using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject nameChanger;

    private TMP_InputField input;

    private void Awake()
    {
        input = nameChanger.GetComponentInChildren<TMP_InputField>();
        input.onEndEdit.AddListener(NickChange);
    }

    private void NickChange(string val)
    {
        if (val.Length < 3) return;

        GameObject.FindGameObjectWithTag("Net").GetComponent<NetManager>().UpdateNick(val);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !nameChanger.activeSelf)
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            nameChanger.SetActive(!nameChanger.activeSelf);
            if (nameChanger.activeSelf)
            {
                input.Select();
            }
        }
    }
}
