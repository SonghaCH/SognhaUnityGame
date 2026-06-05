using UnityEngine;

public class ActivityManager : MonoBehaviour
{
    public static ActivityManager Instance { get; private set; }

    private void Awake() => Instance = this;

    // UI 버튼이나 애니메이션 끝나는 시점에서 호출
    public void ExecuteActivity(string activityId)
    {
        // [추가] 오프닝 중에는 행동 불가 (GameManager 연동)
        if (GameManager.Instance != null && !GameManager.Instance.CanProcessInput())
        {
            Debug.Log("오프닝 중에는 활동을 수행할 수 없습니다.");
            return;
        }

        // 1. 데이터 가져오기
        ActivityData data = GameDataManager.Instance.GetActivityData(activityId);
        if (data == null)
        {
            Debug.LogError($"[ActivityManager] 데이터를 찾을 수 없습니다: {activityId}");
            return;
        }

        // 2. 횟수 제한 체크 (TimeManager에게 확인)
        if (!TimeManager.Instance.CanDoActivity(activityId))
        {
            // 수정: 기존 로그 대신 UI 팝업 오픈 (기존에 작성하셨던 방식)
            DaniTechUIManager.Instance.OpenMiniPopupUI("오늘은 이미 이 행동을 2번 다 했습니다!");
            return;
        }

        // 3. 재화 확인 (StatManager에 돈이 충분한지)
        if (StatManager.Instance.Money < data.MoneyCost)
        {
            DaniTechUIManager.Instance.OpenBigPopupUI("돈이 부족합니다!");
            return;
        }

        // 4. 로직 실행
        StatManager.Instance.AddMoney(-data.MoneyCost);
        TimeManager.Instance.AddTime(data.TimeCost);
        TimeManager.Instance.AddActivityCount(activityId);
        StatManager.Instance.ApplyActivityEffect(data);

        // 5. 복권 결과 처리
        if (activityId == "activity_LuckyDraw_01")
        {
            ProcessLotteryResult();
        }

        Debug.Log($"[ActivityManager] {data.Name} 실행 완료!");
    }

    private void ProcessLotteryResult()
    {
        float random = UnityEngine.Random.Range(0f, 100f);
        int prize = (random < 10f) ? 1000 : (random < 30f) ? 3000 : (random < 60f) ? 5000 :
                    (random < 90f) ? 10000 : (random < 99.9f) ? 50000 : 500000000;

        StatManager.Instance.AddMoney(prize);
        DaniTechUIManager.Instance.OpenLuckyDrawResultPopupUI(0, prize, "당첨"); // index 처리 등은 기존 로직 유지
        Debug.Log($"당첨 결과: {prize}원 획득!");
    }
}