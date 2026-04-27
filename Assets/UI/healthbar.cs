using UnityEngine;
using UnityEngine.UI;
public class healthbar : MonoBehaviour
{
    public Slider healthBarSlider;
    void Update()
    {
    
    }
    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        healthBarSlider.value = currentValue/maxValue;

    }
}
