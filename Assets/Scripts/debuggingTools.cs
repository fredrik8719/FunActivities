using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class debuggingTools : MonoBehaviour
{
    public int debugingInt;
    public GameObject controllerRef;
    public Text testTextDebug;

    public void updateText()
    {
        Debug.Log(debugingInt);
        List<Item> listRef = controllerRef.transform.GetComponent<DatabaseHandler>().testList;

        foreach(Item i in listRef)
        {
            if (i.id == debugingInt)
            {
                testTextDebug.text = i.textSwe;
            }
        }
    }
}
