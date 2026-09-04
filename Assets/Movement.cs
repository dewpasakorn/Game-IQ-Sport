using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float speedMultiplay = 1;
    [SerializeField] private float tickIncreaseSpeed = 0.1f;
    [SerializeField] private float increaseSpeed = 0.1f;
    [SerializeField] private float jumpForce = 3;
    [SerializeField] private float downForce = 3;
    [SerializeField] private Rigidbody rid;
    [SerializeField] private float groundCheckDistance = 1f; // ระยะตรวจพื้น
    [SerializeField] private LayerMask groundLayer; // เลือก Layer ของพื้น
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float delayJump =  .5f;
    [SerializeField] private StartGame startGame;
    private float _tickIncreaseSpeed;
    private float _delayJump;
    private bool isJump;

    private bool isStart = false;

    private void OnEnable()
    {
        startGame.OnClickEvent += StartGame;
    }

    private void OnDisable()
    {
        startGame.OnClickEvent -= StartGame;
    }

    private void  StartGame()
    {
        isStart = true;
        playerAnimator.SetTrigger("StartGame");
    }

    void Start()
    {
        rid.GetComponent<Rigidbody>();
        _delayJump = delayJump;
        _tickIncreaseSpeed = tickIncreaseSpeed;
    }
    void Update()
    {
        // ตรวจสอบสถานะการกระโดด
        if (isJump)
        {
            _delayJump = Mathf.Max(0, _delayJump - Time.deltaTime);
            if (_delayJump == 0)
            {
                isJump = false;
                    _delayJump = delayJump;
            }
        }

        // เช็คระยะจากพื้น
        if (GroundDistance() > groundCheckDistance && !isJump)  // ถ้าตัวละครไม่ได้กระโดด และไม่อยู่บนพื้น
        {
            // เรียก Fall ถ้าตัวละครตกจากพื้นจริงๆ
            playerAnimator.SetTrigger("Fall");
        }
        else if (IsGround() && !isJump)  // ถ้าตัวละครอยู่บนพื้นและไม่ได้กระโดด
        {
            // ถ้าตัวละครอยู่บนพื้นและไม่ได้กระโดด ก็ไม่ต้องเรียก Fall
            playerAnimator.ResetTrigger("Fall");  // รีเซ็ต trigger "Fall" เพื่อไม่ให้เกิดการเปลี่ยนแปลงสถานะโดยไม่จำเป็น
        }

        // กระโดดได้เฉพาะตอนติดพื้น
        if (Input.GetMouseButtonDown(0) && IsGround())
        {
            if (!isStart) return;

            playerAnimator.SetTrigger("Jump");
            isJump = true;
            rid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public bool IsGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, groundLayer))
        {
            return true;
        }
        else
        { 
            return false;
    }
}

    public float GroundDistance()
    {
        RaycastHit hit;
 
        if (Physics.Raycast(transform.position, Vector3.down, out hit,Mathf.Infinity, groundLayer))
        {
            return hit.distance;
        }
        return groundCheckDistance ;
    }

    private void FixedUpdate()
    {
        if(!isStart)  return;

        _tickIncreaseSpeed = Mathf.Max(0, _tickIncreaseSpeed - Time.deltaTime);

        if(_tickIncreaseSpeed == 0)
        {
            speedMultiplay += increaseSpeed;
            _tickIncreaseSpeed = tickIncreaseSpeed;
        }

        transform.Translate(Vector3.forward * movementSpeed * speedMultiplay * Time.deltaTime);
    }
}
