using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUp : MonoBehaviour
{
    bool isFast=false;
    TMP_Text text;

    private void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
        text.text = "Speed up";
    }

    public void ToggleSpeed()
    {
        if (!isFast)
        {
            Time.timeScale = 2;
            isFast = true;
            text.text = "Slow Down";
        }
        else
        {
            Time.timeScale = 1;
            isFast = false;
            text.text = "Speed up";
        }
    }
}
