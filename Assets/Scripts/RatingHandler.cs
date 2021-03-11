using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RatingHandler : MonoBehaviour
{
    public Slider ratingSlider;

    DatabaseHandler databaseRef;

    public Image star1;
    public Image star2;
    public Image star3;
    public Image star4;
    public Image star5;

    public void Start()
    {
        databaseRef = this.gameObject.transform.GetComponent<DatabaseHandler>();
        star1.color = SetStarColorDark();
        star2.color = SetStarColorDark();
        star3.color = SetStarColorDark();
        star4.color = SetStarColorDark();
        star5.color = SetStarColorDark();
    }

    public Color32 SetStarColorDark()
    {
        return new Color32(152, 152, 152, 255);
    }

    public Color32 SetStarColorLight()
    {

        return new Color32(255, 255, 0, 255);
    }

    public void SetVisualRating(int value)
    {
        ratingSlider.value = value;
    }

    public void SaveRating(Item i)
    {
        i.rating = Mathf.RoundToInt(ratingSlider.value);
    }
    public void SliderValueChanged()
    {
        Debug.Log(star1.name);

        if (ratingSlider.value == 0)
        {
            star1.color = SetStarColorDark();
            star2.color = SetStarColorDark();
            star3.color = SetStarColorDark();
            star4.color = SetStarColorDark();
            star5.color = SetStarColorDark();
        }

        else if (ratingSlider.value == 1)
        {
            star1.color = SetStarColorLight();
            star2.color = SetStarColorDark();
            star3.color = SetStarColorDark();
            star4.color = SetStarColorDark();
            star5.color = SetStarColorDark();
        }

        else if (ratingSlider.value == 2)
        {
            star1.color = SetStarColorLight();
            star2.color = SetStarColorLight();
            star3.color = SetStarColorDark();
            star4.color = SetStarColorDark();
            star5.color = SetStarColorDark();
        }

        else if (ratingSlider.value == 3)
        {
            star1.color = SetStarColorLight();
            star2.color = SetStarColorLight();
            star3.color = SetStarColorLight();
            star4.color = SetStarColorDark();
            star5.color = SetStarColorDark();
        }

        else if (ratingSlider.value == 4)
        {
            star1.color = SetStarColorLight();
            star2.color = SetStarColorLight();
            star3.color = SetStarColorLight();
            star4.color = SetStarColorLight();
            star5.color = SetStarColorDark();
        }

        else if (ratingSlider.value == 5)
        {
            star1.color = SetStarColorLight();
            star2.color = SetStarColorLight();
            star3.color = SetStarColorLight();
            star4.color = SetStarColorLight();
            star5.color = SetStarColorLight();
        }
        foreach (Item item in databaseRef.filterList)
        {
            if (item.id == databaseRef.currentActiveCard)
            {
                SaveRating(item);
            }
        }
    }
}
