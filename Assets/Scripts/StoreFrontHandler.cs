using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreFrontHandler : MonoBehaviour
{
    public WindowHandler WH;
    public PopupItemMessageManager PM;
    List<StoreItem> activeStoreItemsList = new List<StoreItem>();
    List<StoreItem> allStoreItemsList = new List<StoreItem>();
    List<GameObject> activeStoreItemsGoList = new List<GameObject>();
    public GameObject itemContainer;
    public GameObject storeItemButton;
    string currencyInUse = "kr";
    public int collumn1 = -121;
    public int collumn2 = 0;
    public int collumn3 = 121;
    public int rowSpacing = 114;
    public int rowStart = 0;

    private void Awake()
    {
        WH = this.gameObject.transform.GetComponent<WindowHandler>();
    }

    public void redrawButtons()
    {
        activeStoreItemsList = new List<StoreItem>();
        activeStoreItemsList = this.GetComponent<DatabaseHandler>().storeItemList;
        StoreItemInstantiation();
    }

    public void StoreItemInstantiation()
    {
        int counter = 1;
        int StoreItemCount = activeStoreItemsList.Count;
        int rowspacingCount = rowStart;
        foreach (StoreItem i in activeStoreItemsList)
        {
            if (counter >= 4)
            {
                counter = 1;
                rowspacingCount = rowspacingCount - 114;
                
            }
            GameObject storeItemButtonClone = storeItemButton;
            storeItemButton.name = i.name;
            Text storeText = storeItemButton.transform.GetChild(0).GetComponent<Text>();
            Text storePriceText = storeItemButton.transform.GetChild(1).GetComponent<Text>();
            storeText.text = i.info;
            storePriceText.text = i.price.ToString() + "." + currencyInUse;
            GameObject tempGO = Instantiate(storeItemButton, itemContainer.transform);
            tempGO.transform.GetComponent<Button>().onClick.AddListener(delegate { OpenStorePopupMessage(i.messageText, i.name); });

            Vector3 objectPosition = new Vector3();
            if (counter == 1){ objectPosition.x = collumn1; }
            else if (counter == 2) { objectPosition.x = collumn2; }
            else if (counter == 3) { objectPosition.x = collumn3; }
            objectPosition.y = rowspacingCount;

            tempGO.transform.localPosition = objectPosition;

            activeStoreItemsGoList.Add(tempGO);
            counter++;
        }       
    }

    public void OpenStorePopupMessage(string message, string buttonName)
    {
        WH.StorePopupMessageOn();
        PM.UnactivateAllButtons();
        PM.ActivateButton(buttonName);
        PM.messageText.text = message;
    }
}
