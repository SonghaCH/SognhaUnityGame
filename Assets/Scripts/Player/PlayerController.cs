using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;

    private int currentDir = 0;
    private int currentState = 0;

    private int lastSentDir = -1;
    private int lastSentState = -1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        animator.SetInteger("State", 0);
        animator.SetInteger("Dir", 0);
        lastSentDir = 0;
        lastSentState = 0;
    }

    void Update()
    {

        // 오프닝 중이면 moveInput을 0으로 만들어 이동 차단
        if (GameManager.Instance != null && !GameManager.Instance.CanProcessInput())
        {
            moveInput = Vector2.zero;
        }
        else
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            if (x != 0) y = 0;
            moveInput = new Vector2(x, y);
        }



        // 1. 입력 여부에 따라 '상태(State)'와 '방향(Dir)' 결정
        if (moveInput != Vector2.zero)
        {
            currentState = 1;

            if (moveInput.y < 0) currentDir = 0;
            else if (moveInput.y > 0) currentDir = 1;
            else if (moveInput.x < 0) currentDir = 2;
            else if (moveInput.x > 0) currentDir = 3;
        }
        else
        {
            currentState = 0;
        }

        // 2. 상태(정지/이동)나 방향이 "실제로 변경되었을 때만" 애니메이터에 신호 전달
        if (currentState != lastSentState || currentDir != lastSentDir)
        {
            animator.SetInteger("State", currentState);
            animator.SetInteger("Dir", currentDir);

            lastSentState = currentState;
            lastSentDir = currentDir;
        }

    }

    void FixedUpdate()
    {
        // 원래 쓰시던 MovePosition 그대로 유지! 
        // 오프닝 중엔 moveInput이 0이므로 이동하지 않습니다.
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}