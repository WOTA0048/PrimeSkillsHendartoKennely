using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float xPos = 0.0f;
    public Transform targetTransform;
    public Camera mainCamera;
    public float moveSpeed = 1.0f;

    public Transform rotator;
    
    public Transform block_one;
    public Transform block_two;

    public float lastPlatformNum = 0;

    public float modifier = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //mainCamera.transform.position = new Vector3(xPos,0.5f,-3);
        
        /*
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            

            //xPos -= Time.deltaTime * moveSpeed;
            targetTransform.position = new Vector3(xPos, targetTransform.position.y, targetTransform.position.z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //xPos += Time.deltaTime * moveSpeed;
            targetTransform.position = new Vector3(xPos, targetTransform.position.y, targetTransform.position.z);
        }
        */
        
        xPos = targetTransform.position.x;
        
        //if 0-1 = 0
        //if 1-2 = 1
        //if 2-3 = 0
        //and so on
        float currentXPosMod = (xPos + modifier) / (2 * modifier) % 2;
        
        Debug.Log(currentXPosMod);
        
        //Debug.Log(currentXPosMod);

        float currentPlatformNum = 0;

        currentPlatformNum = Mathf.Floor(Mathf.Abs(currentXPosMod));
        
        Debug.Log(currentPlatformNum);
        
        if (xPos < -modifier)
        {
            //reverse the rule
            if (currentPlatformNum == 0) currentPlatformNum = 1;
            else currentPlatformNum = 0;
        }

        if (currentPlatformNum == 0)
        {
            block_one.SetParent(null);
            
            if (lastPlatformNum != currentPlatformNum)
            {
                block_one.transform.eulerAngles = Vector3.zero;
                block_one.transform.position =
                    new Vector3(block_one.transform.position.x, 0, block_one.transform.position.z);
            }
            rotator.transform.position = block_one.transform.position;
            block_two.SetParent(rotator, true);
        }
        else if (currentPlatformNum == 1)
        {
            block_two.SetParent(null);
            if (lastPlatformNum != currentPlatformNum)
            {
                block_two.transform.eulerAngles = Vector3.zero;
                block_two.transform.position =
                    new Vector3(block_two.transform.position.x, 0, block_two.transform.position.z);
            }
            rotator.transform.position = block_two.transform.position;
            block_one.SetParent(rotator, true);
        }
        
        float rotationValue = (-xPos + modifier) / (2 * modifier) * (-180);
        rotator.eulerAngles = new Vector3(rotator.eulerAngles.x, rotator.eulerAngles.y, rotationValue);

        lastPlatformNum = currentPlatformNum;
    }
}
