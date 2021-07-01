using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform rotator;
    [SerializeField] private Transform blockOne;
    [SerializeField] private Transform blockTwo;

    [Header("Values")]
    [SerializeField] private float xPos = 0.0f;
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float lastPlatformNum = 0;
    [SerializeField] private float modifier = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        xPos = targetTransform.position.x;
        
        //if 0-1 = 0
        //if 1-2 = 1
        //if 2-3 = 0
        //and so on
        float currentXPosMod = (xPos + modifier) / (2 * modifier) % 2;
        
        //Debug.Log(currentXPosMod);
        
        //Debug.Log(currentXPosMod);

        float currentPlatformNum = 0;

        currentPlatformNum = Mathf.Floor(Mathf.Abs(currentXPosMod));
        
        //Debug.Log(currentPlatformNum);
        
        if (xPos < -modifier)
        {
            //reverse the rule
            if (currentPlatformNum == 0) currentPlatformNum = 1;
            else currentPlatformNum = 0;
        }

        if (currentPlatformNum == 0)
        {
            blockOne.SetParent(null);
            
            if (lastPlatformNum != currentPlatformNum)
            {
                blockOne.transform.eulerAngles = Vector3.zero;
                blockOne.transform.position =
                    new Vector3(blockOne.transform.position.x, 0, blockOne.transform.position.z);
            }
            rotator.transform.position = blockOne.transform.position;
            blockTwo.SetParent(rotator, true);
        }
        else if (currentPlatformNum == 1)
        {
            blockTwo.SetParent(null);
            if (lastPlatformNum != currentPlatformNum)
            {
                blockTwo.transform.eulerAngles = Vector3.zero;
                blockTwo.transform.position =
                    new Vector3(blockTwo.transform.position.x, 0, blockTwo.transform.position.z);
            }
            rotator.transform.position = blockTwo.transform.position;
            blockOne.SetParent(rotator, true);
        }
        
        float rotationValue = (-xPos + modifier) / (2 * modifier) * (-180);
        rotator.eulerAngles = new Vector3(rotator.eulerAngles.x, rotator.eulerAngles.y, rotationValue);

        lastPlatformNum = currentPlatformNum;
    }
}
