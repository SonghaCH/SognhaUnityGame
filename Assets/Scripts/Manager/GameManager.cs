using UnityEngine;

public enum GameState { Play, Dialogue, Paused }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("현재 게임 상태")]
    public GameState CurrentState = GameState.Dialogue;

    private bool isEndingTriggered = false; // 중복 호출 방지

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeState(GameState.Dialogue);
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;

        switch (newState)
        {
            case GameState.Dialogue:
                if (TimeManager.Instance != null) TimeManager.Instance.IsPaused = true;
                break;

            case GameState.Play:
                if (TimeManager.Instance != null) TimeManager.Instance.IsPaused = false;
                break;

            case GameState.Paused:
                if (TimeManager.Instance != null) TimeManager.Instance.IsPaused = true;
                break;
        }

        Debug.Log($"[GameManager] 상태 변경: {newState}");
    }

    // 오프닝 중엔 false, 플레이 중엔 true 반환
    public bool CanProcessInput() => CurrentState == GameState.Play;

    // --- 추가된 엔딩 로직 ---
    public void EnterEndingScene()
    {
        if (isEndingTriggered) return;
        isEndingTriggered = true;

        // 1. 상태를 Dialogue로 변경하여 시간 정지 및 입력 제어
        ChangeState(GameState.Dialogue);

        // 2. 기존 대화창 싹 청소 (DialogueManager의 CloseAllDialogueUIs가 public이어야 함)
        DialogueManager.Instance.CloseAllDialogueUIs();
        DaniTechUIManager.Instance.CloseMainUI();


        // 3. 스탯 기반 엔딩 ID 결정
        bool isSuccess = CheckEndingConditions();

        // [수정된 부분] 요청하신 ID로 적용
        string endingId = isSuccess ? "dateDialogue_Ending_1_2_1" : "dateDialogue_Ending_1_1_1";

        // 4. 엔딩 다이얼로그 시작
        DialogueManager.Instance.StartDialogueFlow(endingId);

        Debug.Log($"[GameManager] 엔딩 진입: {endingId}");
    }

    private bool CheckEndingConditions()
    {
        return StatManager.Instance.Health >= 50f &&
               StatManager.Instance.Money >= 500000 &&
               StatManager.Instance.Charm >= 30f &&
               StatManager.Instance.Intel >= 30f;
    }
}