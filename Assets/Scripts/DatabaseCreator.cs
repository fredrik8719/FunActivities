using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using UnityEngine.EventSystems;

public class DatabaseCreator : MonoBehaviour
{
    public void createDB()
    {
        List<Item> testlistRef = this.gameObject.transform.GetComponent<DatabaseHandler>().testList;
        this.gameObject.transform.GetComponent<DatabaseHandler>().LoadSQLDBWin(Application.dataPath + "/StreamingAssets/Activitys.s3db"); //Source
        string filePath = /*"G:/test/InternalDB.cs"*/Application.dataPath + "/Scripts/InternalDB.cs"; //Target

        string createText = "using UnityEngine;" + Environment.NewLine + Environment.NewLine + "public class InternalDB : MonoBehaviour" + Environment.NewLine + "{" + Environment.NewLine;
        createText = createText + "\t" + "public void populateDB()" + Environment.NewLine + "\t{" + Environment.NewLine + "\t\t";

          foreach (Item i in testlistRef)
          {
            createText = createText + "this.gameObject.transform.GetComponent<DatabaseHandler>().testList.Add(new Item {id = " + i.id + ", textSwe = \"" + i.textSwe + "\", textEng = \"" + i.textEng + "\", costs = " + i.costs.ToString().ToLower()  + ", outside = " + i.outside.ToString().ToLower() + ", winter = " + i.winter.ToString().ToLower() + ", material = " + i.material.ToString().ToLower() + ", pedagogic = " + i.pedagogic.ToString().ToLower() + "});" + Environment.NewLine + "\t\t";
          }

        //     createText = createText + "this.gameObject.transform.GetComponent<DatabaseHandler>().testList.Add(new Item {id = " + i.id + ", text = \"" + i.text + ", costs = \"" +  + "\"});" + Environment.NewLine + "\t\t";

        // (new Item {id = " + i.id + ", text = " + i.text + ", costs = " + i.costs + ", outside = " + i.outside + ", winter = " + i.winter + ", material = " + i.material + "});"

        createText = createText + Environment.NewLine + "\t" + "}" + Environment.NewLine + "}";

        File.WriteAllText(filePath,createText);

        Debug.Log(testlistRef.Count + " objects added");
        
    }

    public void createDBStoreItem()
    {
        List<StoreItem> storeItemListRef = this.gameObject.transform.GetComponent<DatabaseHandler>().storeItemList;
        this.gameObject.transform.GetComponent<DatabaseHandler>().LoadSQLDBPurchasable(Application.dataPath + "/StreamingAssets/Activitys.s3db"); //Source
        string filePath = /*"G:/test/InternalDBStoreItem.cs"*/Application.dataPath + "/Scripts/InternalDBStoreItem.cs"; //Target

        string createText = "using UnityEngine;" + Environment.NewLine + Environment.NewLine + "public class InternalDBStoreItem : MonoBehaviour" + Environment.NewLine + "{" + Environment.NewLine;
        createText = createText + "\t" + "public void populateDB()" + Environment.NewLine + "\t{" + Environment.NewLine + "\t\t"; 

        foreach (StoreItem i in storeItemListRef)
        {
            createText = createText + "this.gameObject.transform.GetComponent<DatabaseHandler>().storeItemList.Add(new StoreItem {name = \"" + i.name + "\", price = " + i.price + ", image = \"" + i.image + "\", info = \"" + i.info.ToString().ToLower() + "\", messageText = \"" + i.messageText + "\"});" + Environment.NewLine + "\t\t";
        }

        createText = createText + Environment.NewLine + "\t" + "}" + Environment.NewLine + "}";

        File.WriteAllText(filePath, createText);

    }
}
