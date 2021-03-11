using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveScript : MonoBehaviour
{
    private Vector3 startPosition;
    public AnimationHandler aH;
    private void Start()
    {
        startPosition = this.gameObject.transform.position;
        Debug.Log(startPosition);
    }
    private void Update()
    {
        if (aH.testHold)
        {
            this.gameObject.transform.position = new Vector3(Input.mousePosition.x, startPosition.y, startPosition.z);
        }
        
    }
    
}
