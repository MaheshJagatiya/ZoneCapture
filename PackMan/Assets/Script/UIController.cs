using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI progressText;

    void OnEnable()
    {
        GameEvents.OnLivesChanged += l => livesText.text = "Lives: " + l;
        GameEvents.OnAreaCaptured += p => progressText.text = "Progress: " + Mathf.RoundToInt(p) + "%";
    }
}
