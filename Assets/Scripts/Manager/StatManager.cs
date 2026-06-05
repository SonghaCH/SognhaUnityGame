using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static StatManager Instance { get; private set; }

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

    private float GetStressMultiplier()
    {
        if (Stress >= 300) return 0.5f;
        if (Stress >= 200) return 0.6f;
        if (Stress >= 100) return 0.7f;
        if (Stress >= 50) return 0.95f;
        return 1.0f;
    }

    public void AddMoney(int amount)
    {
        Money = Mathf.Max(0, Money + amount);
        if (MainUI.Instance != null)
        {
            MainUI.Instance.RefreshUI();
        }
    }

    public void DecreaseStress(float amount)
    {
        // Stress에서 amount를 뺀 뒤, 0 이하로 내려가지 않게 Clamp 처리
        Stress = Mathf.Clamp(Stress - amount, 0, MAX_STAT);

        // UI 갱신 (MainUI가 연결되어 있다면)
        if (MainUI.Instance != null)
        {
            MainUI.Instance.RefreshUI();
        }
    }

    // [기존 UI 호환용] 기존 호출부들이 에러 나지 않도록 유지
    public void ApplyActivityEffect(ActivityData data)
    {
        // 기존 방식은 그대로 패널티 없이 적용
        ApplyActivityEffect(data, data.ChangeStress);
    }

    // [ActivityManager 전용] 새벽 패널티가 계산된 finalStress를 받음
    public void ApplyActivityEffect(ActivityData data, float finalStress)
    {
        if (data == null) return;

        float multiplier = GetStressMultiplier();

        Health = Mathf.Clamp(Health + (data.HealthGain * multiplier), 0, MAX_STAT);
        Intel = Mathf.Clamp(Intel + (data.IntGain * multiplier), 0, MAX_STAT);
        Charm = Mathf.Clamp(Charm + (data.CharmGain * multiplier), 0, MAX_STAT);

        // ActivityManager에서 계산된 패널티 적용값 사용
        Stress = Mathf.Clamp(Stress + finalStress, 0, MAX_STAT);

        Money = Mathf.Max(0, Money - data.MoneyCost + data.MoneyGain);

        if (MainUI.Instance != null)
        {
            MainUI.Instance.RefreshUI();
        }
    }
}