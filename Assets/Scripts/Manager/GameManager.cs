using UnityEngine;

public enum GameState { Play, Dialogue, Paused }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("현재 게임 상태")]
    public GameState CurrentState = GameState.Dialogue; // 오프닝부터 시작

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
        // 게임 시작 시 초기 상태 설정
        ChangeState(GameState.Dialogue);
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;

        switch (newState)
        {
            case GameState.Dialogue:
                // 대화 중: 시간 정지
                if (TimeManager.Instance != null) TimeManager.Instance.IsPaused = true;
                break;

            case GameState.Play:
                // 플레이 중: 시간 흐름 재개
                if (TimeManager.Instance != null) TimeManager.Instance.IsPaused = false;
                break;

            case GameState.Paused:
                // 메뉴 등 일시정지: 시간 정지
                if (TimeManager.Instance != null) TimeManager.Instance.IsPaused = true;
                break;
        }

        Debug.Log($"[GameManager] 상태 변경: {newState}");
    }

    // 오프닝 중엔 false, 플레이 중엔 true 반환
    public bool CanProcessInput() => CurrentState == GameState.Play;
}