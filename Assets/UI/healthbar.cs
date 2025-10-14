using UnityEngine;
using UnityEngine.UI;
public class healthbar : MonoBehaviour
{
    public Slider healthBarSlider;
    public health_player playerHealth;
    // Update is called once per frame
    void Update()
    {
        healthBarSlider.value = playerHealth.health;
        healthBarSlider.maxValue = playerHealth.maxHealth;
    }
}
