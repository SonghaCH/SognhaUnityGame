using UnityEngine;

public class ActivityManager : MonoBehaviour
{
    public static ActivityManager Instance { get; private set; }

    private void Awake() => Instance = this;

    // UI 버튼이나 애니메이션 끝나는 시점에서 호출
    public void ExecuteActivity(string activityId)
    {
        // 1. 데이터 가져오기 (엑셀 데이터 창고 이용)
        ActivityData data = GameDataManager.Instance.GetActivityData(activityId);
        if (data == null)
        {
            Debug.LogError($"[ActivityManager] 데이터를 찾을 수 없습니다: {activityId}");
            return;
        }

        // 2. 횟수 제한 체크 (TimeManager에게 확인)
        if (!TimeManager.Instance.CanDoActivity(activityId))
        {
            Debug.Log("오늘은 이미 이 행동을 2번 다 했습니다!");
            return;
        }

        // 3. 재화 확인 (StatManager에 돈이 충분한지)
        if (StatManager.Instance.Money < data.MoneyCost)
        {
            Debug.Log("돈이 부족합니다!");
            return;
        }

        // 4. 로직 실행
        StatManager.Instance.AddMoney(-data.MoneyCost); // 비용 차감
        TimeManager.Instance.AddTime(data.TimeCost);    // 시간 흐름
        TimeManager.Instance.AddActivityCount(activityId); // 횟수 기록
        StatManager.Instance.ApplyActivityEffect(data); // 스탯 변화

        // 5. [복권 전용] 결과 처리 로직
        if (activityId == "activity_LuckyDraw_01")
        {
            ProcessLotteryResult();
        }

        Debug.Log($"[ActivityManager] {data.Name} 실행 완료!");
    }

    // 복권 확률 및 당첨금 로직
    private void ProcessLotteryResult()
    {
        float random = UnityEngine.Random.Range(0f, 100f);
        int prize = 0;
        int ballIndex = 0;
        string colorName = "";

        // 확률: 빨강(10%), 파랑(20%), 초록(30%), 흰색(30%), 노랑(9.9%), 검은색(0.1%)
        if (random < 10f) { prize = 1000; ballIndex = 0; colorName = "빨강"; }
        else if (random < 30f) { prize = 3000; ballIndex = 1; colorName = "파랑"; }
        else if (random < 60f) { prize = 5000; ballIndex = 2; colorName = "초록"; }
        else if (random < 90f) { prize = 10000; ballIndex = 3; colorName = "흰색"; }
        else if (random < 99.9f) { prize = 50000; ballIndex = 4; colorName = "노랑"; }
        else { prize = 500000000; ballIndex = 5; colorName = "검은색"; } // 5억 당첨!

        // 당첨금 지급 및 UI 갱신
        StatManager.Instance.AddMoney(prize);

        // 결과 팝업 오픈 (데이터 전달)
        DaniTechUIManager.Instance.OpenLuckyDrawResultPopupUI(ballIndex, prize, colorName);

        Debug.Log($"당첨 결과: {colorName} 구슬 당첨, {prize}원 획득!");
    }
}