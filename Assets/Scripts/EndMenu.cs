using TMPro;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public void SetText(string txt)
    { 
        text.text = txt + "\n\n\nR - рестарт";
    }

}
