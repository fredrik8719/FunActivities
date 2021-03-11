using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupItemMessageManager : MonoBehaviour
{
    public Text messageText;
    public string missingTextMessage;
    public List<GameObject> buttonGoList;

    public void UnactivateAllButtons()
    {
        foreach (GameObject go in buttonGoList)
        {
            go.SetActive(false);
        } 
    }

    public void ActivateButton(string name)
    {
        foreach (GameObject go in buttonGoList)
        {
            if (go.name == name)
            {
                go.SetActive(true);
                break;
            }
        }
    }
}
