using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimationHandler : MonoBehaviour
{
    public bool testHold = false;
    public Vector3 startMousePosition;
    public Vector3 currentMousePosition;
    public Vector3 moveTarget;
    public Vector3 noteStartPosition;
    public GameObject noteObject;
    public bool debugSwapCardDistance = false;
    public float swapCardDistance = 1.0f;
    public GameObject debugSwapCardBarsL;
    public GameObject debugSwapCardBarsR;
    public float positionTracker;

    public float yStartPosition;


    public bool startSwipe = false;

    public GameObject pointerRight;
    public GameObject pointerLeft;

    public Vector3 swipeStart;
    public Vector3 swipeTarget;
    public Transform travelStart;
    public Transform travelEnd;
    public float swipeSpeed = 10.0f; // units/s
    public float swipeDistance;
    public float swipeStartTime;

    public GameObject targetL;
    public GameObject targetR;
    /*  public Vector2 mousePosition;
      void Update()
      {
          if (Input.GetButtonDown("Fire1"))
          {
              Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
              RaycastHit hit;

              if (Physics.Raycast(ray, out hit, 100))
              {
                  Debug.Log("Hit");
                  Debug.Log(hit.transform.gameObject.name);
              }
          //    mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
          }
      }
      */

    public void Start()
    {
        noteStartPosition = noteObject.transform.position;
        yStartPosition = noteObject.transform.localPosition.y;
    }

    void Update()
    {
        if (testHold)
        {
            float distanceMoved = Input.mousePosition.x - moveTarget.x;
            Vector3 moveMovementFromCenter = new Vector3(noteStartPosition.x + distanceMoved, noteStartPosition.y, noteStartPosition.z);

            noteObject.transform.position = new Vector3(noteStartPosition.x + distanceMoved, noteStartPosition.y, noteStartPosition.z);
            /*
            float moveTargetDistance;
            currentMousePosition = new Vector3(Input.mousePosition.x, noteStartPosition.y, noteStartPosition.z);
            moveTarget = new Vector3(noteStartPosition.x + (currentMousePosition.x - noteStartPosition.x) , noteStartPosition.y, noteStartPosition.z);
            moveTargetDistance = noteObject.transform.position.x - currentMousePosition.x;
            noteObject.transform.position = new Vector3(noteStartPosition.x + moveTargetDistance, noteStartPosition.y, noteStartPosition.x);
            //noteObject.transform.position = currentMousePosition;*/
        }

        if (debugSwapCardDistance)
        {
            debugSwapCardBarsL.GetComponent<RectTransform>().sizeDelta = new Vector2(swapCardDistance, Screen.height);
            debugSwapCardBarsR.GetComponent<RectTransform>().sizeDelta = new Vector2(swapCardDistance, Screen.height);
            debugSwapCardBarsL.SetActive(true);
            debugSwapCardBarsR.SetActive(true);
        }

        /*        if (startSwipe)
                {
                    float step = swipeSpeed * Time.deltaTime;
                    Debug.Log("DropSwipe started " + swipeStart + " " + swipeTarget + " " + swipeSpeed);
                    noteObject.transform.position = Vector3.MoveTowards(swipeStart, swipeTarget, 3);
                    Debug.Log("DropSwipe ended " + swipeStart + " " + swipeTarget + " " + swipeSpeed);
                    startSwipe = false;
                    if (noteObject.transform.position == swipeTarget)
                    {
                        Debug.Log("settofalse");
                        startSwipe = false;
                    }
                }*/


    /*    float distCovered = (Time.time - swipeStartTime) * swipeSpeed;

        float fracJourney = distCovered / swipeDistance;

        if (startSwipe)
        {
            noteObject.transform.position = Vector3.Lerp(travelStart.position, targetR.transform.position, fracJourney);
        }
        if (noteObject.transform.position == targetR.transform.position)
        {
            startSwipe = false;
        }
        
        */



    }

    public void onPressNote()
    {
        noteStartPosition = noteObject.transform.position;
        testHold = true;
        startMousePosition = new Vector3(Input.mousePosition.x, 0, 0); //Clickposition
        moveTarget = new Vector3(Input.mousePosition.x, noteStartPosition.y, noteStartPosition.z);
        //currentMousePosition = new Vector3(Input.mousePosition.x, 0 , 0);
        pointerRight.SetActive(false);
        pointerLeft.SetActive(false);

    }

    public void onReleaseNote()
    {
        testHold = false;
        float BorderR = Screen.width - swapCardDistance;
        //Debug.Log(BorderR);
        float BorderL = 0 + swapCardDistance;
        //Debug.Log(BorderL);
        Debug.Log(Input.mousePosition.x);
        if (Input.mousePosition.x >= BorderR)
        {
            this.gameObject.GetComponent<DatabaseHandler>().PreviousActivity();
            noteObject.transform.localPosition = new Vector3(0, yStartPosition, 0);
        }
        else if (Input.mousePosition.x <= BorderL)
        {
            this.gameObject.GetComponent<DatabaseHandler>().NextActivity();
            noteObject.transform.localPosition = new Vector3(0, yStartPosition, 0);
        }

        else
        {
            noteObject.transform.localPosition = new Vector3(0, yStartPosition, 0);
        }

        pointerRight.SetActive(true);
        pointerLeft.SetActive(true);

        /*    swipeStart = noteObject.transform.position;
            travelStart = noteObject.transform;


            if (positionTracker >= BorderR - swapCardDistance)
            {
                this.gameObject.GetComponent<DatabaseHandler>().TestRandomClick();
                noteObject.transform.localPosition = new Vector3(BorderR + 125, 0, 0);
            //    swipeTarget = new Vector3(BorderR + 125, noteObject.gameObject.transform.position.y, 0);
            //    startSwipe = true;
            //    swipeStartTime = Time.time;
            //    travelStart = noteObject.transform;
            //    travelEnd = noteObject.transform;
            //    travelEnd.position = swipeTarget;

               Debug.Log(travelStart.position);
                Debug.Log(targetR.transform.position);

                swipeDistance = Vector3.Distance(travelStart.position, targetR.transform.position);

                Debug.Log(swipeDistance);
            }

            else if (positionTracker <= BorderL + swapCardDistance)
            {
                this.gameObject.GetComponent<DatabaseHandler>().TestRandomClick();
                //    noteObject.transform.localPosition = new Vector3(BorderL - 125, 0, 0);
               swipeTarget = new Vector3(BorderL - 125, noteObject.gameObject.transform.position.y, 0);
                startSwipe = true;
                swipeStartTime = Time.time;
            }
            else
            {
                noteObject.transform.localPosition = new Vector3(0, 0, 0);
            }

            pointerRight.SetActive(true);
            pointerLeft.SetActive(true);
            */
    }
}
