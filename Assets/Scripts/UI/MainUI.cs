using UnityEngine;
using UnityEngine.UI;

public class MainUI : DaniTechUIBase
{
    // [중요] 다른 매니저에서 이 UI를 찾기 위한 싱글톤 인스턴스
    public static MainUI Instance { get; private set; }

    [SerializeField] private Image Image_Time;
    [SerializeField] private Text Text_Date;
    [SerializeField] private Text Text_Day;
    [SerializeField] private Text Text_Time;

    [SerializeField] private DaniTechUIButton Button_Option;
    [SerializeField] private DaniTechUIButton Button_Map;

    [SerializeField] private Text Text_Health;
    [SerializeField] private Text Text_Int;
    [SerializeField] private Text Text_Charm;
    [SerializeField] private Text Text_Money;
    [SerializeField] private Text Text_Stress;

    private void Awake()
    {
        // 인스턴스 등록
        Instance = this;
    }

    private void OnEnable()
    {
        Button_Option.BindOnClickButtonEvent(Onclick_Option);
        Button_Map.BindOnClickButtonEvent(Onclick_Map);

        // UI가 켜질 때 현재 데이터를 한번 반영
        RefreshUI();
    }

    // UI 갱신을 담당하는 통합 메서드
    public void RefreshUI()
    {
        // 시간 정보 갱신
        if (TimeManager.Instance != null)
        {
            Text_Date.text = TimeManager.Instance.GetFormattedDate();
            Text_Day.text = TimeManager.Instance.GetFormattedDay();
            Text_Time.text = TimeManager.Instance.GetFormattedTime();
        }

        // [활성화] StatManager 데이터 연동
        if (StatManager.Instance != null)
        {
            Text_Health.text = StatManager.Instance.Health.ToString("F1");
            Text_Int.text = StatManager.Instance.Intel.ToString("F1");
            Text_Charm.text = StatManager.Instance.Charm.ToString("F1");
            Text_Money.text = StatManager.Instance.Money.ToString("N0") + "원";
            Text_Stress.text = StatManager.Instance.Stress.ToString("F1");
        }
    }

    private void Onclick_Option()
    {
        Debug.Log("설정 열기");
        DaniTechUIManager.Instance.OpenHomeUI();
    }

    private void Onclick_Map()
    {
        DaniTechUIManager.Instance.OpenMapPopupUI();
    }
}
