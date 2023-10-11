using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus) Cursor.visible = false;
    }
}
