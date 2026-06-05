using UnityEngine;

public class Interaction_Home : MonoBehaviour
{
    [SerializeField] private GameObject interactionHint;

    private void Start()
    {
        if (interactionHint != null) interactionHint.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (interactionHint != null) interactionHint.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (interactionHint != null) interactionHint.SetActive(false);
        }
    }

    private void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.CanProcessInput())
        {
            return;
        }

        if (interactionHint.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            // 시간 식별: 새벽 1시(60분) ~ 오전 8시(480분) 사이 확인
            int currentTime = TimeManager.Instance.CurrentMinutes;

            if (currentTime >= 60 && currentTime < 480)
            {
                // 새벽 시간대: 잠들기 UI 호출
                DaniTechUIManager.Instance.OpenHomeSleepUI();
            }
            else
            {
                // 일반 시간대: 기존 집 UI 호출
                DaniTechUIManager.Instance.OpenHomeUI();
            }
        }
    }
}