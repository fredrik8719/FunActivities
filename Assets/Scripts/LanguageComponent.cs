using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class LanguageComponent : MonoBehaviour
{
    public Text textComponentLanguage;
    public GameObject gameController;

    LanguageController lc;

    public string svenska;
    public string english;

    public void Start()
    {
        lc = gameController.GetComponent<LanguageController>();

        textComponentLanguage = this.gameObject.transform.GetComponent<Text>();

        switchLanguage();

        lc.switchLanguageEvent.AddListener(switchLanguage);

    }

    void switchLanguage()
    {
        if (lc.setLanguage == LanguageController.Language.Svenska)
        {
            textComponentLanguage.text = svenska;
        }

        else if (lc.setLanguage == LanguageController.Language.English)
        {
            textComponentLanguage.text = english;
        }
        //   t
    }
}
