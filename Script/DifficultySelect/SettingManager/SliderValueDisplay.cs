using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValueDisplay : MonoBehaviour
{
    public Slider targetSlider;
    public TextMeshProUGUI valueText;

    void Start()
    {
        if (targetSlider != null)
        {
            UpdateValue(targetSlider.value);
            targetSlider.onValueChanged.AddListener(UpdateValue);
        }
    }

    void UpdateValue(float value)
    {
        valueText.text = Mathf.RoundToInt(value * 100f) + "%";
    }
}
