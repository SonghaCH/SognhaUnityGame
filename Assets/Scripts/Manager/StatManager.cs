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

    private void Awake() { Instance = this; InitializeStats(); }
    private void InitializeStats() { Health = 50f; Intel = 10f; Charm = 10f; Stress = 0f; Money = 300000; }

    public void DecreaseStress(float amount)
    {
        Stress = Mathf.Clamp(Stress - amount, 0, MAX_STAT);
        if (MainUI.Instance != null) MainUI.Instance.RefreshUI();
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
        if (MainUI.Instance != null) MainUI.Instance.RefreshUI();
    }

    public void ApplyActivityEffect(ActivityData data) { ApplyActivityEffect(data, data.ChangeStress); }

    public void ApplyActivityEffect(ActivityData data, float finalStress)
    {
        if (data == null) return;
        float multiplier = GetStressMultiplier();
        Health = Mathf.Clamp(Health + (data.HealthGain * multiplier), 0, MAX_STAT);
        Intel = Mathf.Clamp(Intel + (data.IntGain * multiplier), 0, MAX_STAT);
        Charm = Mathf.Clamp(Charm + (data.CharmGain * multiplier), 0, MAX_STAT);
        Stress = Mathf.Clamp(Stress + finalStress, 0, MAX_STAT);
        Money = Mathf.Max(0, Money - data.MoneyCost + data.MoneyGain);
        if (MainUI.Instance != null) MainUI.Instance.RefreshUI();
    }
}