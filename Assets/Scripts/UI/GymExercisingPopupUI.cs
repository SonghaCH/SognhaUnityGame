using UnityEngine;
using UnityEngine.UI;

public class GymExercisingPopupUI : DaniTechUIBase
{
    // 이제 targetCount도 데이터에서 가져올 수 있다면 더 좋겠지만, 
    // 우선 로직상 필요한 50번은 그대로 유지합니다.
    public int targetCount = 50;
    private int currentCount = 0;
    private bool isTraining = false;

    // 활동 ID를 상수로 정의하여 오타 방지
    private const string ACTIVITY_ID = "activity_Gym_01";

    [SerializeField] private Text countText;

    private void OnEnable()
    {
        StartTraining();
    }

    public void StartTraining()
    {
        currentCount = 0;
        isTraining = true;
        UpdateUI();
    }

    private void Update()
    {
        if (isTraining && Input.GetKeyDown(KeyCode.Space))
        {
            currentCount++;
            UpdateUI();

            if (currentCount >= targetCount)
            {
                FinishTraining();
            }
        }
    }

    private void UpdateUI()
    {
        countText.text = $"{currentCount} / {targetCount}개";
    }

    private void FinishTraining()
    {
        isTraining = false;

        // 1. DataManager를 통해 엑셀(JSON) 데이터 불러오기
        ActivityData data = GameDataManager.Instance.GetActivityData(ACTIVITY_ID);

        if (data != null)
        {
            // 2. 행동 횟수 증가
            TimeManager.Instance.AddActivityCount(ACTIVITY_ID);

            // 3. 데이터에 정의된 값을 사용하여 스탯/돈 반영
            // 하드코딩된 값 대신 data.HealthGain, data.MoneyCost 등을 사용합니다.
            StatManager.Instance.ApplyActivityEffect(data);

            // 4. 데이터에 정의된 소요 시간 적용
            TimeManager.Instance.AddTime(data.TimeCost);

            Debug.Log($"{data.Name} 완료! 데이터 드리븐 적용 완료.");
        }
        else
        {
            Debug.LogError($"데이터를 찾을 수 없습니다: {ACTIVITY_ID}");
        }

        DaniTechUIManager.Instance.OpenMiniPopupUI("운동 완료!");
        DaniTechUIManager.Instance.CloseGymExercisingPopupUI();
    }
}
