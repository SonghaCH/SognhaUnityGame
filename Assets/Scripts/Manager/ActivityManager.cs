using UnityEngine;

public class ActivityManager : MonoBehaviour
{
    public static ActivityManager Instance { get; private set; }

    private void Awake() => Instance = this;

    public void ExecuteActivity(string activityId)
    {
        ActivityData data = GameDataManager.Instance.GetActivityData(activityId);
        if (data == null) return;

        if (!TimeManager.Instance.CanDoActivity(activityId))
        {
            DaniTechUIManager.Instance.OpenBigPopupUI("오늘은 이미 이 행동을 2번 다 했습니다!");
            return;
        }

        if (StatManager.Instance.Money < data.MoneyCost)
        {
            DaniTechUIManager.Instance.OpenBigPopupUI("돈이 부족합니다!");
            return;
        }

        // 새벽 패널티 계산 (원본 로직은 건드리지 않고, 호출 시점만 조절)
        float finalStress = data.ChangeStress;
        if (TimeManager.Instance.IsLateNight())
        {
            finalStress *= 2f;
        }

        StatManager.Instance.AddMoney(-data.MoneyCost);
        TimeManager.Instance.AddTime(data.TimeCost);
        TimeManager.Instance.AddActivityCount(activityId);

        // 오버로딩된 함수를 사용하여 인자 전달
        StatManager.Instance.ApplyActivityEffect(data, finalStress);

        if (activityId == "activity_LuckyDraw_01")
        {
            ProcessLotteryResult();
        }
    }

    // 보내주신 스크린샷의 원본 복권 로직
    private void ProcessLotteryResult()
    {
        float random = UnityEngine.Random.Range(0f, 100f);
        int prize = 0;
        int ballIndex = 0;
        string colorName = "";

        if (random < 10f) { prize = 1000; ballIndex = 0; colorName = "빨강"; }
        else if (random < 30f) { prize = 3000; ballIndex = 1; colorName = "파랑"; }
        else if (random < 60f) { prize = 5000; ballIndex = 2; colorName = "초록"; }
        else if (random < 90f) { prize = 10000; ballIndex = 3; colorName = "흰"; }
        else if (random < 99f) { prize = 50000; ballIndex = 4; colorName = "노랑"; }
        else { prize = 500000000; ballIndex = 5; colorName = "검은"; }

        StatManager.Instance.AddMoney(prize);

        // 원본 로그 및 UI 로직 유지
        DaniTechUIManager.Instance.OpenLuckyDrawResultPopupUI(ballIndex, prize, colorName);
        Debug.Log($"당첨 결과: {colorName} 구슬 당첨, {prize}원 획득!");
    }
}