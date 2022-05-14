using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class staminaBarScript : MonoBehaviour
{

    public Slider staminaBar;

    private int maxStamina = 60;

    public static staminaBarScript instance;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    public void UseStamina(int currentStamina)
    {
 
       staminaBar.value = currentStamina;
    }


}
