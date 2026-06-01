using UnityEngine;

public class StatManager : MonoBehaviour
{
    // 다른 매니저에서 쉽게 접근하기 위한 싱글톤
    public static StatManager Instance { get; private set; }

    // 메인 UI에 표시할 스탯들
    public float Health { get; private set; }
    public float Intel { get; private set; }
    public float Charm { get; private set; }
    public float Stress { get; private set; }
    public int Money { get; private set; }

    private void Awake()
    {
        Instance = this;
        InitializeStats();
    }

    private void InitializeStats()
    {
        Health = 50f;
        Intel = 10f;
        Charm = 10f;
        Stress = 0f;
        Money = 300000;
    }

    // [추가] 외부에서 돈을 더하거나 뺄 때 사용하는 공용 함수
    public void AddMoney(int amount)
    {
        Money += amount;

        // 돈이 바뀌었으므로 즉시 UI 갱신
        if (MainUI.Instance != null)
        {
            MainUI.Instance.RefreshUI();
        }

        Debug.Log($"돈이 {amount}만큼 변했습니다. 현재 잔액: {Money}");
    }

    // ActivityManager에서 이 함수를 호출하여 데이터를 전달함
    public void ApplyActivityEffect(ActivityData data)
    {
        if (data == null) return;

        // 스탯 반영
        Health += data.HealthGain;
        Intel += data.IntGain;
        Charm += data.CharmGain;
        Stress += data.ChangeStress;

        // 기존 돈 계산 방식 유지 (비용 및 보상 반영)
        Money = Money - data.MoneyCost + data.MoneyGain;

        // 모든 수치가 반영된 후 UI 갱신 요청
        if (MainUI.Instance != null)
        {
            MainUI.Instance.RefreshUI();
        }

        Debug.Log($"{data.Name} 적용 완료: 건강 {data.HealthGain}, 최종 잔액 {Money}");
    }
}