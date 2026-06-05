using UnityEngine;

public class Interaction_BarberShop : MonoBehaviour
{
    // [중요] 여기에 Hierarchy에 있는 "F 복권방 입장" 스프라이트 오브젝트를 드래그해서 넣으세요!
    [SerializeField] private GameObject interactionHint;

    private void Start()
    {
        // 시작할 때 무조건 꺼져있게 설정
        if (interactionHint != null) interactionHint.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어가 다가오면 알림창 켜기
            if (interactionHint != null) interactionHint.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어가 멀어지면 알림창 끄기
            if (interactionHint != null) interactionHint.SetActive(false);
        }
    }

    private void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.CanProcessInput())
        {
            return;
        }
        // 범위 내에 있을 때만 F키 입력 감지
        // (isPlayerInRange 변수를 대신하여 간단하게 체크)
        if (interactionHint.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            DaniTechUIManager.Instance.OpenBarberShopUI();
        }
    }
}
