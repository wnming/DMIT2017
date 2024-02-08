using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField]
    Material[] lightColors;

    int colorIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        MyEvents.ChangeColor.AddListener(ColorSwitch);
        colorIndex = 0;
    }

    // Update is called once per frame
    void ToggleLight()
    {
        Light myLight;
        myLight = GetComponentInChildren<Light>();

        myLight.enabled = !myLight.enabled;
    }

    void ColorSwitch()
    {
        colorIndex++;
        if (colorIndex >= lightColors.Length)
        {
            colorIndex = 0;
        }

        GetComponentInChildren<Light>().color = lightColors[colorIndex].color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            MyEvents.Activate.AddListener(ToggleLight);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            MyEvents.Activate.RemoveListener(ToggleLight);
        }
    }
}
