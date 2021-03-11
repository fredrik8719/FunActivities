using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LanguageController : MonoBehaviour
{
    public enum Language{Svenska, English };

    public Language setLanguage;

    public UnityEvent switchLanguageEvent = new UnityEvent();

    public Dropdown languageSelector;


    public void changeLanguage()
    {
        if (languageSelector.options[languageSelector.value].text.ToString() == "Svenska")
        {
            setLanguage = Language.Svenska;
        }

        else if (languageSelector.options[languageSelector.value].text.ToString() == "English")
        {
            setLanguage = Language.English;
        }

        switchLanguageEvent.Invoke();
    }
}
