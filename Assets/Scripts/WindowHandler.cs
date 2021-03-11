using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowHandler : MonoBehaviour
{
    public GameObject StartWindow;
    public GameObject ActivityCardWindow;
    public GameObject FilterWindow;
    public GameObject SettingsWindow;
    public GameObject LibraryWindow;
    public GameObject StoreWindow;
    public GameObject StorePopupMessage;
    public bool debugingTools = false;

    public GameObject DebugingWindow ;

    List<GameObject> windowList = new List<GameObject>();

    private void Awake()
    {
        windowList.Add(StartWindow);
        windowList.Add(ActivityCardWindow);
        windowList.Add(FilterWindow);
        windowList.Add(SettingsWindow);
        windowList.Add(LibraryWindow);
        windowList.Add(StoreWindow);

        if (debugingTools)
        {
            foreach (GameObject g in windowList)
            {
                g.SetActive(false);
            }

            DebugingWindow.SetActive(true);
        }
    }

    public void AllWindowClose()
    {
        foreach (GameObject g in windowList)
        {
            g.SetActive(false);
        }
    }
    public void StartWindowOn()
    {
        StartWindow.SetActive(true);
    }

    public void StartWindowOff()
    {
        StartWindow.SetActive(false);
    }

    public void ActivityCardWindowOn()
    {
        ActivityCardWindow.SetActive(true);
    }

    public void ActivityCardWindowOff()
    {
        ActivityCardWindow.SetActive(false);
    }

    public void FilterWindowOn()
    {
        FilterWindow.SetActive(true);
    }

    public void FilterWindowOff()
    {
        FilterWindow.SetActive(false);
    }

    public void SettingsWindowOn()
    {
        SettingsWindow.SetActive(true);
    }

    public void SettingsWindowOff()
    {
        SettingsWindow.SetActive(false);
    }

    public void LibraryWindowOn()
    {
        LibraryWindow.SetActive(true);
    }

    public void LibraryWindowOff()
    {
        LibraryWindow.SetActive(false);
    }

    public void StorWindowOn()
    {
        StoreWindow.SetActive(true);
    }

    public void StoreWindowOff()
    {
        StoreWindow.SetActive(false);
    }

    public void StorePopupMessageOn()
    {
        StorePopupMessage.SetActive(true);
    }

    public void StorePopupMessageOff()
    {
        StorePopupMessage.GetComponent<PopupItemMessageManager>().messageText.text = StorePopupMessage.GetComponent<PopupItemMessageManager>().missingTextMessage;
        StorePopupMessage.SetActive(false);    
    }
}
