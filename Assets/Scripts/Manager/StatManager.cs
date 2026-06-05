using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static StatManager Instance { get; private set; }

    // 최대값 설정 (필요에 따라 조절하세요)
    private const float MAX_STAT = 1000f;

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

    public void AddMoney(int amount)
    {
        // 돈은 최소 0원까지만 가능
        Money = Mathf.Max(0, Money + amount);

        if (MainUI.Instance != null)
            MainUI.Instance.RefreshUI();

        Debug.Log($"돈이 {amount}만큼 변했습니다. 현재 잔액: {Money}");
    }

    public void ApplyActivityEffect(ActivityData data)
    {
        if (data == null) return;

        // 모든 스탯은 0에서 MAX_STAT 사이로 제한
        Health = Mathf.Clamp(Health + data.HealthGain, 0, MAX_STAT);
        Intel = Mathf.Clamp(Intel + data.IntGain, 0, MAX_STAT);
        Charm = Mathf.Clamp(Charm + data.CharmGain, 0, MAX_STAT);
        Stress = Mathf.Clamp(Stress + data.ChangeStress, 0, MAX_STAT);

        // 돈은 최소 0원 이상만 유지
        Money = Mathf.Max(0, Money - data.MoneyCost + data.MoneyGain);

        if (MainUI.Instance != null)
            MainUI.Instance.RefreshUI();

        Debug.Log($"{data.Name} 적용 완료: 건강 {Health}, 최종 잔액 {Money}");
    }
}