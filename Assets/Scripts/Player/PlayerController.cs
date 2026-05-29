using UnityEngine;


public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;

    private int currentDir = 0;      // 현재 방향 (0:하, 1:상, 2:좌, 3:우)
    private int currentState = 0;    // 현재 상태 (0:Idle, 1:Walk)

    private int lastSentDir = -1;    // 애니메이터에 마지막으로 보낸 방향
    private int lastSentState = -1;  // 애니메이터에 마지막으로 보낸 상태

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 부모 오브젝트나 자식 오브젝트 어디에 Animator가 있든 안전하게 가져오도록 기존 뼈대 유지
        animator = GetComponentInChildren<Animator>();

        // 시작할 때 최초 한 번 애니메이터 기본값 세팅
        animator.SetInteger("State", 0);
        animator.SetInteger("Dir", 0);
        lastSentDir = 0;
        lastSentState = 0;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x != 0) y = 0; // 대각선 차단

        moveInput = new Vector2(x, y);

        // 1. 입력 여부에 따라 '상태(State)'와 '방향(Dir)' 결정
        if (moveInput != Vector2.zero)
        {
            currentState = 1; // 키를 누르고 있으므로 걷기 상태(Walk = 1)

            if (moveInput.y < 0) currentDir = 0; // 아래
            else if (moveInput.y > 0) currentDir = 1; // 위
            else if (moveInput.x < 0) currentDir = 2; // 왼쪽
            else if (moveInput.x > 0) currentDir = 3; // 오른쪽
        }
        else
        {
            currentState = 0; // 키에서 손을 뗐으므로 정지 상태(Idle = 0)
            // 방향(currentDir)은 마지막에 바라보던 방향을 그대로 유지합니다.
        }

        // 2. ★ [핵심] 상태(정지/이동)나 방향이 "실제로 변경되었을 때만" 애니메이터에 딱 한 번 신호 전달
        if (currentState != lastSentState || currentDir != lastSentDir)
        {
            animator.SetInteger("State", currentState);
            animator.SetInteger("Dir", currentDir);

            // 현재 보낸 값을 저장하여 매 프레임 무한 호출되는 것을 방지 (꾹 누르기 버그 해결)
            lastSentState = currentState;
            lastSentDir = currentDir;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}
