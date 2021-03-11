using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Filter : MonoBehaviour
{
    public Slider Slider1;
    public Slider Slider2;
    public Slider Slider3;
    public Slider Slider4;
    public Slider Slider5;

    public bool costs;
    public bool outside;
    public bool winter;
    public bool material;
    public bool pedagogic;

    public void setFilter()
    {
        Debug.Log("Slider1: " + Slider1.value + "Slider2: " + Slider2.value + "Slider3: " + Slider3.value + "Slider4: " + Slider4.value + "Slider5: " + Slider5.value);
    }
}
