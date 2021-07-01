using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject body;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rbody;
    [SerializeField] private Toggle accToggle;

    [Header("Keybinds")]
    [SerializeField] KeyCode walkLeft = KeyCode.LeftArrow;
    [SerializeField] KeyCode walkRight = KeyCode.RightArrow;

    [Header("Checker")]
    [SerializeField] public string playerFace;

    [Header("Values")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float animVelocity;
    [SerializeField] private float animRate;
    [SerializeField] private float currentVelocity;
    [SerializeField] private float accelerationRate;
    [SerializeField] private float playerPositionPercent;
    [SerializeField] public float xPos = 0.0f;



    void move()
    {
        rbody.isKinematic = false;
        animator.SetBool("B_Run", true);
        animator.SetFloat("run_multi", 1);
        currentVelocity = currentVelocity + (accelerationRate * Time.deltaTime);
        animVelocity = animVelocity + (animRate * Time.deltaTime);
    }



    // Start is called before the first frame update
    void Start()
    {
        animator = body.GetComponent<Animator>();
        rbody = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentVelocity > moveSpeed)
            currentVelocity = moveSpeed;

        if (animVelocity > 1)
            animVelocity = 1;

        if (Input.GetKey(walkLeft))
        {
            playerFace = "left";
            xPos -= Time.deltaTime * moveSpeed;
            player.transform.rotation = Quaternion.Euler(0, 270, 0);
            move();

            if (!accToggle.isOn)
            {
                rbody.velocity = Vector2.left * moveSpeed;
                animator.SetFloat("run_multi", 1);
            }

            else if (accToggle.isOn)
            {
                rbody.velocity = Vector2.left * currentVelocity;
                animator.SetFloat("run_multi", animVelocity);
            }
        }

        if (Input.GetKey(walkRight))
        {
            playerFace = "right";
            xPos += Time.deltaTime * moveSpeed;
            player.transform.rotation = Quaternion.Euler(0, 90, 0);
            move();

            if (!accToggle.isOn)
            {
                rbody.velocity = Vector2.right * moveSpeed;
                animator.SetFloat("run_multi", 1);
            }

            else if (accToggle.isOn)
            {
                rbody.velocity = Vector2.right * currentVelocity;
                animator.SetFloat("run_multi", animVelocity);
            }
        }

        if (!Input.GetKey(walkLeft) && !Input.GetKey(walkRight))
        {

            if (!accToggle.isOn)
            {
                //rbody.isKinematic = true;
                animator.SetBool("B_Run", false);
                animator.SetFloat("run_multi", 0);

                rbody.velocity = Vector3.zero;
            }

            else if (accToggle.isOn)
            {
                if (playerFace == "right")
                    rbody.velocity = Vector2.right * currentVelocity;

                else if (playerFace == "left")
                    rbody.velocity = Vector2.left * currentVelocity;

                if (currentVelocity > 0)
                    currentVelocity = currentVelocity - (accelerationRate * Time.deltaTime);

                if (animVelocity > 0)
                    animVelocity = animVelocity - (animRate * Time.deltaTime);

                if (currentVelocity <= 0)
                {
                    animator.SetFloat("run_multi", animVelocity);
                    animator.SetBool("B_Run", false);
                    animator.SetFloat("run_multi", 0);
                }


            }
        }
        
    }


 
}



