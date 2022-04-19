using TMPro;
using UnityEngine;

public class UIPowerupTimer : MonoBehaviour
{
    public static TextMeshProUGUI Text;

    void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
        Text.enabled = false;
    }

    public static void Display(float value) => Text.SetText("{0:0}", value);
}