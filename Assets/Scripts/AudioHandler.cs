using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioHandler : MonoBehaviour
{
    public AudioSource aS;
    public Slider volumeSlider;


    public void VolumeChange()
    {
        
        aS.volume = volumeSlider.value;
    }
}
