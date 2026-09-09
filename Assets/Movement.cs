using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float increaseSpeed = 0.1f;
    [SerializeField] private float jumpForce = 3;
    [SerializeField] private Rigidbody rid;
    [SerializeField] private float groundCheckDistance = 1f;      // ระยะตรวจพื้น (สำหรับ jump/landed จริง)
    [SerializeField] private float nearGroundDistance = 2f;       // ระยะ "เกือบถึงพื้น" (สำหรับเล่นอนิเมชันล่วงหน้า)
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private StartGame startGame;
    [SerializeField] private Transform jumpEffect;
    private Health health;

    [SerializeField] private float slowAmount = 2f;
    [SerializeField] private float slowRecoverTime = 1.5f;
    private Coroutine slowCoroutine;

    private bool isStart = false;
    private bool wasNearGround = true; // track previous frame's "almost grounded" state
    private bool isDoubleJump;

    private void OnEnable()
    {
        startGame.OnClickEvent += StartGame;
        health.GetDamagedEvent += StartSlow;
    }

    private void OnDisable()
    {
        startGame.OnClickEvent -= StartGame;
        health.GetDamagedEvent -= StartSlow;
    }

    private void StartGame()
    {
        isStart = true;
        playerAnimator.SetTrigger("StartGame");
    }

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void StartSlow()
    {
        // Restart the effect if hit again mid-slow, instead of stacking
        if (slowCoroutine != null)
            StopCoroutine(slowCoroutine);

        slowCoroutine = StartCoroutine(SlowRoutine());
    }

    private IEnumerator SlowRoutine()
    {
        float originalSpeed = movementSpeed;
        float slowedSpeed = Mathf.Max(0f, originalSpeed / slowAmount);

        movementSpeed = slowedSpeed;

        float elapsed = 0f;
        while (elapsed < slowRecoverTime)
        {
            elapsed += Time.deltaTime;
            movementSpeed = Mathf.Lerp(slowedSpeed, originalSpeed, elapsed / slowRecoverTime);
            yield return null;
        }

        movementSpeed = originalSpeed;
        slowCoroutine = null;
    }

    void Update()
    {
        if (!isStart) return;

        bool isNearGround = IsNearGround();
        bool isFalling = rid.velocity.y <= 0f;

        bool grounded = IsGround();
        playerAnimator.SetBool("Jump", false);
        playerAnimator.SetBool("IsGrounded", grounded);

        // Fire "Grounded" the moment we come within nearGroundDistance while falling,
        // but only once per approach (not every frame while near ground)
        if (isNearGround && isFalling && !wasNearGround)
        {
            playerAnimator.SetTrigger("Grounded");
            isDoubleJump = false;
        }
        wasNearGround = isNearGround;

        if (Input.GetMouseButtonDown(0))
        {
            if (IsGround())
            {
                // Jump ครั้งแรก
                isDoubleJump = false;
                Jump();
            }
            else if (!isDoubleJump)
            {
                // Double Jump
                isDoubleJump = true;
                Jump();
            }
        }
    }


    void Jump()
    {
        PlayerSoundManager.instance.JumpSound();
        playerAnimator.SetBool("Jump", true);

        Vector3 velocity = rid.velocity;
        velocity.y = 0;
        rid.velocity = velocity;

        rid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        Transform vfx = Instantiate(jumpEffect, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), transform.rotation);
    }

    // Actual contact with the ground — used for jump input
    public bool IsGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    // Slightly larger check — used to trigger the landing animation a bit early
    public bool IsNearGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, nearGroundDistance, groundLayer);
    }

    private void FixedUpdate()
    {
        if (!isStart) return;

        movementSpeed += increaseSpeed * Time.fixedDeltaTime;
        playerAnimator.SetFloat("speed", movementSpeed / 4);
        transform.Translate(
            Vector3.forward * movementSpeed * Time.fixedDeltaTime
        );
    }
}