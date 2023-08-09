using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the movement of the player with given input from the input manager
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Speed at which the player moves")]
    public float moveSpeed = 2f;
    [Tooltip("Speed at which player looks left and right (calculated in degrees)")]
    public float lookSpeed = 60f;
    [Tooltip("The power with which the player jumps")]
    public float jumpPower = 8f;
    [Tooltip("The strength of gravity")]
    public float gravity = 9.81f;

    [Header("Jump Timing")]
    [Tooltip("The strength of gravity")]
    public float jumpTimeLeniency = 0.1f;
    float timeToStopBeingLenient = 0;

    [Header("Required References")]
    [Tooltip("The player shooter script that fires projectiles")]
    public PlayerShooter playerShooter;
    public Health playerHealth;
    public List<GameObject> disableWhileDead;

    bool doubleJumpAvailable = false;
    bool bossHpBarActivated = false;

    //The Character controller component on the player
    private CharacterController controller;
    private InputManager inputManager;

    public GameObject boss;
    public GameObject bossHealthBar;
    public float bossHpBarActivationDistance;

    /// <summary>
    /// Description:
    /// Standard Unity function called once before the first Update call
    /// Input:
    /// none
    /// Return:
    /// void (no return)
    /// </summary>
    void Start()
    {
        SetupCharacterController();
        SetupInputManager();
        
    }

    private void SetupCharacterController()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("The player controller script does not have a character controller on the same game object");
        }
    }

    private void SetupInputManager()
    {
        inputManager = InputManager.instance;
    }

    /// <summary>
    /// Description:
    /// Standard Unity function called once every frame
    /// Input:
    /// none
    /// Return:
    /// void (no return)
    /// </summary>
    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            foreach (GameObject inGameObject in disableWhileDead)
            {
                inGameObject.SetActive(false);
            }
            return;
        }
        else
        {
            foreach (GameObject inGameObject in disableWhileDead)
            {
                inGameObject.SetActive(true);
            }

        }

        ProcessMovement();
        ProcessRotation();
        BossHpBarCheck();
    }

    private void BossHpBarCheck()
    {        
        float distanceToBoss = Vector3.Distance(boss.transform.position, transform.position);
        if (distanceToBoss <= bossHpBarActivationDistance && bossHealthBar != null && !bossHpBarActivated)
        {
            bossHealthBar.SetActive(true);
            bossHpBarActivated = true;
        }
    }

    Vector3 moveDirection;

    void ProcessMovement()
    {
        //Get the input from the input manager
        float leftRightInput = inputManager.horizontalMoveAxis;        
        float forwardBackwardInput = inputManager.verticalMoveAxis;
        bool jumpPressed = inputManager.jumpPressed;

        //Handle the control of the player while it is on the ground.
        if (controller.isGrounded)
        {
            doubleJumpAvailable = true;
            timeToStopBeingLenient = Time.time + jumpTimeLeniency;

            // Set the movement direction to be the received input,set Y to 0 since we are on the ground.
            moveDirection = new Vector3(leftRightInput, 0, forwardBackwardInput);
            //Set the move direction in relation to the transform. 
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * moveSpeed;

            if (jumpPressed)
            {
                moveDirection.y = jumpPower;
            }
        }
        else
        {
            moveDirection = new Vector3(leftRightInput * moveSpeed, moveDirection.y, forwardBackwardInput * moveSpeed);
            moveDirection = transform.TransformDirection(moveDirection);
            if (jumpPressed && Time.time < timeToStopBeingLenient)
            {
                moveDirection.y = jumpPower;
            }
            else if (jumpPressed && doubleJumpAvailable)
            {
                moveDirection.y = jumpPower;
                doubleJumpAvailable = false;



            }
        }

        moveDirection.y -= gravity * Time.deltaTime;

        if (controller.isGrounded && moveDirection.y < 0)
        {
            moveDirection.y = 0.3f;
        }

        controller.Move(moveDirection * Time.deltaTime);
    }

    void ProcessRotation()
    {
        float horizontalLookInput = inputManager.horizontalLookAxis;
        Vector3 playerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(playerRotation.x,
            playerRotation.y + horizontalLookInput * lookSpeed * Time.deltaTime, playerRotation.z));
    }


}
