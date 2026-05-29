using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _moveSpeed = 5f;
    private Rigidbody2D _rb;
    private Vector2 moveInput;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (moveInput.sqrMagnitude > 1)
        {
            moveInput.Normalize();
        }
    }

    void FixedUpdate()
    {
        // 물리적인 이동 처리
        _rb.MovePosition(_rb.position + moveInput * _moveSpeed * Time.fixedDeltaTime);
    }
}
