using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startup : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;

    private IEnumerator Start()
    {
        foreach (var obj in objects)
        {
            Instantiate(obj);
        }

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("MainScene");
    }
}
