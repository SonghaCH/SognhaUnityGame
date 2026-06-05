using UnityEngine;
using UnityEngine.UI;

public class HomeUI : DaniTechUIBase
{
    [SerializeField] private Text Text_Date;
    [SerializeField] private Text Text_Day;
    [SerializeField] private Text Text_Time;


    [SerializeField] private DaniTechUIButton Btn_Exit;
    [SerializeField] private DaniTechUIButton Btn_Rest;
    [SerializeField] private DaniTechUIButton Btn_Help;
    [SerializeField] private Text Text_Money;

    private void OnEnable()
    {
        Btn_Exit.BindOnClickButtonEvent(OnClick_Exit);
        Btn_Rest.BindOnClickButtonEvent(OnClick_Rest);
        Btn_Help.BindOnClickButtonEvent(OnClick_Help);


    }
    public void Update()
    {
        RefreshUI();
        UpdateMoneyUI();
    }



    public void RefreshUI()
    {
        // Null 체크: 매니저들이 아직 초기화되지 않았을 경우를 대비
        if (TimeManager.Instance != null)
        {
            Text_Date.text = TimeManager.Instance.GetFormattedDate();
            Text_Day.text = TimeManager.Instance.GetFormattedDay();
            Text_Time.text = TimeManager.Instance.GetFormattedTime();
        }

        
    }
   

    public void UpdateMoneyUI()
    {
        if (Text_Money != null && StatManager.Instance != null)
        {
            Text_Money.text = $"{StatManager.Instance.Money:N0} 원";
        }
    }

    private void OnClick_Exit()
    {
        DaniTechUIManager.Instance.CloseHomeUI();
    }

    private void OnClick_Rest()
    {
        // 횟수 체크
        if (TimeManager.Instance.CanDoActivity("activity_HomeRest_01"))
        {
            DaniTechUIManager.Instance.OpenHomeRestPopupUI();

        }
        else
        {
            DaniTechUIManager.Instance.OpenMiniPopupUI("금일 최대 휴식 시간 도달  ");
        }
    }

    private void OnClick_Help()
    {
        DaniTechUIManager.Instance.OpenHomeHelpPopupUI();
    }
}
