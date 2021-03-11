using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseFulfillment : MonoBehaviour
{
    public void PurchaseFulfillmentDebugMessager(string message)
    {
        string buyMessage = "Message: " + message;
        Debug.Log(buyMessage);
    }
}
