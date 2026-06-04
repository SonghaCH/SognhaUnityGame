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

        RefreshUI();

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

        // StatManager는 아직 만드시는 중이겠지만, 연결하면 이런 식이 됩니다.
        /*
        if (StatManager.Instance != null)
        {
            Text_Health.text = StatManager.Instance.Health.ToString("F1");
            Text_Int.text = StatManager.Instance.Int.ToString("F1");
            Text_Charm.text = StatManager.Instance.Charm.ToString("F1");
            Text_Money.text = StatManager.Instance.Money.ToString("N0") + "원";
            Text_Stress.text = StatManager.Instance.Stress.ToString("F1");
        }
        */
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
            DaniTechUIManager.Instance.OpenPCGamePlayPopupUI();
        }
        else
        {
            DaniTechUIManager.Instance.OpenMiniPopupUI("금일 최대 휴식 시간 도달  ");
        }
    }

    private void OnClick_Help()
    {
        DaniTechUIManager.Instance.OpenPCHelpPopupUI();
    }
}
