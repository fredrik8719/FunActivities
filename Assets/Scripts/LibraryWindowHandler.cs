using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LibraryWindowHandler : MonoBehaviour
{
    public GameObject libraryCardPrefab;
    public GameObject scrollWindowRef;
    public DatabaseHandler databaseRef;
    public LanguageController lcRef;
	public AudioSource buttonClickAudioSourceControll;
	public AudioClip buttonClickAudio;

    List<GameObject> instantiatedLibraryCardGameobjects;

    List<GameObject> activityGoList;

    public Vector3 startPosition;
    public Vector3 lastPosition;
    public float separation = 80;
    public int cardHeight = 80;
    public Vector3 objectSeparation;

    private void Start()
    {
        lcRef = this.transform.GetComponent<LanguageController>();
        startPosition = new Vector3(0,166,0);
        objectSeparation = new Vector3(0, separation, 0);

        instantiatedLibraryCardGameobjects = new List<GameObject>();
        databaseRef = this.gameObject.transform.GetComponent<DatabaseHandler>();
        activityGoList = new List<GameObject>();
    }

    public void UpdateScrollSize(int distance, int objectHeight)
    {
        int savedItemListCount = databaseRef.savedItems.Count;
        int newHeight = savedItemListCount * (objectHeight + distance);
        GameObject content = scrollWindowRef.transform.GetChild(0).gameObject;
        RectTransform rt = content.GetComponent<RectTransform>();
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight/2);
    }

    public float GetScrollStart(float objectHeight)
    {
        GameObject content = scrollWindowRef.transform.GetChild(0).gameObject;
        float startPosition = ((content.GetComponent<RectTransform>().sizeDelta.y)/2)-(objectHeight/2);

        return startPosition;
    }

	public void AddPlayAudioListener(Button b)
	{
		buttonClickAudioSourceControll.PlayOneShot(buttonClickAudio, 1.0F);
	}

    public void UpdateGoList()
    {
        //Backwards for-loop, start is to clear the list
        for (int i = activityGoList.Count; i > 0; i--)
        {
            Destroy(activityGoList[i-1]);
        }
        activityGoList = new List<GameObject>();
        int steps = 0;
        UpdateScrollSize(cardHeight, Mathf.RoundToInt(separation));
        startPosition.y = 0 - (cardHeight/2);
        foreach (Item i in databaseRef.savedItems)
        {
            steps++;
            GameObject go = Instantiate(libraryCardPrefab);

            go.transform.SetParent(scrollWindowRef.transform.GetChild(0), false);
            if (steps == 1)
            {
                go.transform.localPosition = startPosition;
                lastPosition = startPosition;
            }
            else
            {
                go.transform.localPosition = lastPosition - objectSeparation;
                lastPosition = lastPosition - objectSeparation;
            }
            go.name = i.id.ToString();
            GameObject goText;
            Button goDeleteButton;
            goDeleteButton = go.transform.GetChild(0).GetComponent<Button>();
            goText = go.transform.GetChild(1).gameObject;

            goDeleteButton.onClick.AddListener(delegate { databaseRef.deleteFromSaveList(i.id); });
            goDeleteButton.onClick.AddListener(UpdateGoList);
			goDeleteButton.onClick.AddListener (delegate { AddPlayAudioListener(goDeleteButton); });

            if (lcRef.setLanguage == LanguageController.Language.Svenska)
            {
                goText.GetComponent<Text>().text = i.textSwe;
            }
            else if (lcRef.setLanguage == LanguageController.Language.English)
            {
                goText.GetComponent<Text>().text = i.textEng;
            }

            activityGoList.Add(go);

         //   go.transform.localPosition = scrollWindowRef.transform;
        }
    }
}
