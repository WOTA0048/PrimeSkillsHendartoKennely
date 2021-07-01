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
    //[SerializeField] private GameObject PlatG;
    //[SerializeField] private GameObject PlatR;

    [Header("Keybinds")]
    [SerializeField] KeyCode walkLeft = KeyCode.LeftArrow;
    [SerializeField] KeyCode walkRight = KeyCode.RightArrow;

    [Header("Checker")]
    [SerializeField] private bool moving;
    [SerializeField] public string playerFace;
    [SerializeField] private bool onPlatformG;
    [SerializeField] private bool onPlatformR;
    [SerializeField] private bool onPlatR;
    [SerializeField] private bool onPlatL;

    [Header("Values")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float animVelocity;
    [SerializeField] private float animRate;
    [SerializeField] private float currentVelocity;
    [SerializeField] private float accelerationRate;
    [SerializeField] private float playerPositionPercent;

    public float xPos = 0.0f;
    public Transform targetTransform;
    //public Camera mainCamera;

    public Transform rotator;

    public Transform block_one;
    public Transform block_two;

    public float lastPlatformNum = 0;







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
            rbody.isKinematic = false;
            player.transform.rotation = Quaternion.Euler(0, 270, 0);
            animator.SetBool("B_Run", true);
            animator.SetFloat("run_multi", 1);

            currentVelocity = currentVelocity + (accelerationRate * Time.deltaTime);
            animVelocity = animVelocity + (animRate * Time.deltaTime);

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
            rbody.isKinematic = false;
            player.transform.rotation = Quaternion.Euler(0, 90, 0);
            animator.SetBool("B_Run", true);
            animator.SetFloat("run_multi", 1);

            currentVelocity = currentVelocity + (accelerationRate * Time.deltaTime);
            animVelocity = animVelocity + (animRate * Time.deltaTime);

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



