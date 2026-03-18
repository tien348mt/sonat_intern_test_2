using TMPro;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class UIGamePlay : MonoBehaviour
{
    public TextMeshProUGUI lvText;
    public TextMeshProUGUI stepText;
    void Update()
    {
        if (GridManager.Instance != null
            && GridManager.Instance.activeLevel != null)
        {
            lvText.text = GridManager.Instance.activeLevel.name;
            stepText.text = GridManager.Instance.currentStep.ToString() + " Moves";
        }
        
    }
}
