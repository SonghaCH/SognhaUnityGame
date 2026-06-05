using UnityEngine;

public class HomeSleepUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_Sleep;

    private void OnEnable()
    {
        Btn_Sleep.BindOnClickButtonEvent(OnClick_Sleep);
    }

    private void OnClick_Sleep()
    {
        // 1. 시간 스킵 (8시로 이동)
        TimeManager.Instance.SkipToTime(480);

        // 2. 스트레스 50 감소 (StatManager의 새 로직 호출)
        StatManager.Instance.DecreaseStress(50f);

        // 3. UI 닫기
        DaniTechUIManager.Instance.CloseHomeSleepUI();

        Debug.Log("잠을 잡니다. 아침 8시로 이동하며 스트레스가 50 감소했습니다.");
    }
}