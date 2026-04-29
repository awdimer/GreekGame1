using UnityEngine;
using UnityEngine.UI;
public class staminahbar : MonoBehaviour
{
    public Slider staminaBarSlider;
    public testPlayerMovement player;
    void Update()
    {
        staminaBarSlider.value = player.stamina/player.maxStamina;
    }
    
}
