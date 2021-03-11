using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Mono.Data.Sqlite;
using System.Data;
using System;
using System.IO;
using System.Diagnostics;
using System.Linq;

public class DatabaseHandler : MonoBehaviour
{
    public Text testText;
    public LanguageController lcRef;

    public Text debugText;
    public int debugInt;
    public int currentActiveCard;

    public int currentListIndex = 0;

    public GameObject saveSymbol;

    public Text testDebug;

    public List<Item> testList = new List<Item>();
    public List<StoreItem> storeItemList = new List<StoreItem>();
    public List<Item> filterList;
    public List<Item> shuffledList;

    public List<Item> savedItems = new List<Item>();

    private void Awake()
    {
        this.gameObject.transform.GetComponent<InternalDB>().populateDB();
        this.gameObject.transform.GetComponent<InternalDBStoreItem>().populateDB();
    }

    private void Start()
    {
        lcRef = this.gameObject.transform.GetComponent<LanguageController>();
        ApplyFilter();
    }
    //Se till framöver så att laddandet av databasen blir bra.
    public void LoadSQLDBWin(string SQLfile)
    {
        testList = new List<Item>();
        debugText.text = "Running LoadSLDB";

        string conn = "URI=file:" + SQLfile; //DB path

        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * " + "FROM Activitysinfo";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string textSwe = reader.GetString(1);
            string textEng = "";
            if (!reader.IsDBNull(2))
            {
                textEng = reader.GetString(2);
            }

            bool costs = false;
            bool outside = false;
            bool winter = false;
            bool material = false;
            bool pedagogic = false;
            if (!reader.IsDBNull(3))
            {
                costs = true;
            }
            if (!reader.IsDBNull(4))
            {
                outside = true;
            }
            if (!reader.IsDBNull(5))
            {
                winter = true;
            }
            if (!reader.IsDBNull(6))
            {
                material = true;
            }
            if (!reader.IsDBNull(7))
            {
                pedagogic = true;
            }
            //    string tags = reader.GetString(2);

            testList.Add(new Item { id = id, textSwe = textSwe, textEng = textEng, costs = costs, outside = outside, winter = winter, material = material, pedagogic = pedagogic });
            //    Debug.Log("id: " + id + " text " + text/* + " tags " + tags*/);
        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        debugText.text = "LoadSLDB finished";

    }

    public void LoadSQLDBPurchasable(string SQLfile)
    {
        storeItemList = new List<StoreItem>();
        debugText.text = "Running LoadSLDBPurchasable";

        string conn = "URI=file:" + SQLfile; //DB path

        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * " + "FROM StoreItems";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            string name = reader.GetString(0);
            int price = reader.GetInt32(1);
            string image = "";
            string infoText = "";
            string messageText = "";
            if (!reader.IsDBNull(2))
            {
                image = reader.GetString(2);
            }
            if (!reader.IsDBNull(3))
            {
                infoText = reader.GetString(3);
            }
            if (!reader.IsDBNull(4))
            {
                messageText = reader.GetString(4);
            }

            storeItemList.Add(new StoreItem { name = name, price = price, image = image, info = infoText, messageText = messageText });
        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        debugText.text = "LoadSLDBPurchasable finished";

    }

    public void saveActiveCard()
    {
        foreach (Item i in filterList)
        {
            if (i.id == currentActiveCard)
            {
                foreach (Item s in savedItems)
                {
                    if (s.id == i.id)
                    {
                        UnityEngine.Debug.Log("Card is already saved, dont forget to add som sort of symbol to card.");
                        return;
                    }
                }
                savedItems.Add(i);
                saveSymbol.SetActive(true);
                return;
            }
        }
    }

    public void deleteFromSaveList(int id)
    {
        foreach (Item si in savedItems.ToList())
        {
            if (si.id == id)
            {
                savedItems.Remove(si);
                break;
            }
        }
    }

    public void ApplyFilter()
    {
        Filter filterRef = this.gameObject.transform.GetComponent<Filter>();
        filterList = new List<Item>();

        int costs = Convert.ToInt32(filterRef.Slider1.value);
        int outside = Convert.ToInt32(filterRef.Slider2.value);
        int winter = Convert.ToInt32(filterRef.Slider3.value);
        int material = Convert.ToInt32(filterRef.Slider4.value);
        int pedagogic = Convert.ToInt32(filterRef.Slider5.value);

        //Check
        foreach (Item i in testList)
        {
            //Check for blockers if no found
            if (costs == 1 && i.costs == true)
            {
                continue;
            }
            if (material == 1 && i.material == true)
            {
                continue;
            }
            if ((outside == 0 && i.outside == true) || (outside == 2 && i.outside == false))
            {
                continue;
            }
            if ((winter == 2 && i.winter == false) || (winter == 0 && i.winter == true))
            {
                continue;
            }

            if (pedagogic == 0 && i.pedagogic == false)
            {
                continue;
            }

            else
            {

                filterList.Add(i);
            }
        }
        RandomizeFilterList();
    }

    public void UpdateSaveSymbol()
    {
        saveSymbol.SetActive(false);
        if (savedItems.Count > 0)
        {
            foreach (Item s in savedItems)
            {
                if (s.id == currentActiveCard)
                {
                    saveSymbol.SetActive(true);
                    break;
                }
            }
        }
    }

    public void RandomizeFilterList()
    {
        // Loops through array
        for (int i = filterList.Count - 1; i > 0; i--)
        {
            // Randomize a number between 0 and i (so that the range decreases each time)
            int rnd = UnityEngine.Random.Range(0, i);

            // Save the value of the current i, otherwise it'll overright when we swap the values
            Item temp = filterList[i];

            // Swap the new and old values
            filterList[i] = filterList[rnd];
            filterList[rnd] = temp;
        }
        UnityEngine.Debug.Log("List is randomized");
        ResetListPosition();
    }

    public void ResetListPosition()
    {
        currentListIndex = 0;
    }

    public void DisplayCurrentActivity()
    {
        RatingHandler rh = this.gameObject.transform.GetComponent<RatingHandler>();
        if (filterList.Count > 0)
        {
            currentActiveCard = filterList[currentListIndex].id;
            UpdateSaveSymbol();
            rh.SetVisualRating(filterList[currentListIndex].rating);

            if (lcRef.setLanguage == LanguageController.Language.Svenska)
            {
                testText.text = filterList[currentListIndex].textSwe;
            }
            else if (lcRef.setLanguage == LanguageController.Language.English)
            {
                if (filterList[currentListIndex].textEng == "")
                {
                    testText.text = "Oops! Something went wrong. The Activity is not yet translated. Activitynumber is: " + filterList[currentListIndex].id;
                }
                else
                {
                    testText.text = filterList[currentListIndex].textEng;
                }
            }
        }
        else
        {
            testText.text = "Det finns inga aktiviteter som matchar dina filterinställningar, försök igen";
        }
    }

    public void NextActivity()
    {
        RatingHandler rh = this.gameObject.transform.GetComponent<RatingHandler>();
        if (filterList.Count > 0)
        {
            if (currentListIndex == filterList.Count - 1)
            {
                currentListIndex = 0;
            }
            else
            {
                currentListIndex++;
            }

            currentActiveCard = filterList[currentListIndex].id;
            UpdateSaveSymbol();
            rh.SetVisualRating(filterList[currentListIndex].rating);

            if (lcRef.setLanguage == LanguageController.Language.Svenska)
            {
                testText.text = filterList[currentListIndex].textSwe;
            }
            else if (lcRef.setLanguage == LanguageController.Language.English)
            {
                if (filterList[currentListIndex].textEng == "")
                {
                    testText.text = "Oops! Something went wrong. The Activity is not yet translated. Activitynumber is: " + filterList[currentListIndex].id;
                }
                else
                {
                    testText.text = filterList[currentListIndex].textEng;
                }
            }
        }
        else
        {
            testText.text = "Det finns inga aktiviteter som matchar dina filterinställningar, försök igen";
        }
    }

    public void PreviousActivity()
    {
        RatingHandler rh = this.gameObject.transform.GetComponent<RatingHandler>();
        if (filterList.Count > 0)
        {
            if (currentListIndex == 0)
            {
                currentListIndex = filterList.Count -1;
            }
            else
            {
                currentListIndex--;
            }
            currentActiveCard = filterList[currentListIndex].id;
            UpdateSaveSymbol();
            rh.SetVisualRating(filterList[currentListIndex].rating);

            if (lcRef.setLanguage == LanguageController.Language.Svenska)
            {
                testText.text = filterList[currentListIndex].textSwe;
            }
            else if (lcRef.setLanguage == LanguageController.Language.English)
            {
                if (filterList[currentListIndex].textEng == "")
                {
                    testText.text = "Oops! Something went wrong. The Activity is not yet translated. Activitynumber is: " + filterList[currentListIndex].id;
                }
                else
                {
                    testText.text = filterList[currentListIndex].textEng;
                }
            }
        }
        else
        {
            testText.text = "Det finns inga aktiviteter som matchar dina filterinställningar, försök igen";
        }

    }
    public void RandomizeNextActivtiy()
    {
        RatingHandler rh = this.gameObject.transform.GetComponent<RatingHandler>();
        ApplyFilter();
        int r = 0;
        testDebug.text = filterList.Count.ToString();
        UpdateSaveSymbol();
        if (filterList.Count > 0)
        {
            r = UnityEngine.Random.Range(0, filterList.Count);

            currentActiveCard = filterList[r].id;
            UpdateSaveSymbol();
            rh.SetVisualRating(filterList[r].rating);

            if (lcRef.setLanguage == LanguageController.Language.Svenska)
            {
                testText.text = filterList[r].textSwe;
            }
            else if (lcRef.setLanguage == LanguageController.Language.English)
            {
                if (filterList[r].textEng == "")
                {
                    testText.text = "Oops! Something went wrong. The Activity is not yet translated. Activitynumber is: " + filterList[r].id; 
                }
                else
                {
                    testText.text = filterList[r].textEng;
                }
                
            }
    //        RatingHandler.DisplayCurrentRating(filterList[r].rating);
        }
        else
        {
            testText.text = "Det finns inga aktiviteter som matchar dina filterinställningar, försök igen";
        }
    }
}