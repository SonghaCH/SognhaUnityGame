using UnityEngine;
using UnityEngine.UI;

public class LuckyDrawUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_Exit;
    [SerializeField] private DaniTechUIButton Btn_LuckyPlay;
    [SerializeField] private DaniTechUIButton Btn_Help;
    [SerializeField] private Text Text_Money;

    private void OnEnable()
    {
        Btn_Exit.BindOnClickButtonEvent(OnClick_Exit);
        Btn_LuckyPlay.BindOnClickButtonEvent(OnClick_LuckyPlay);
        Btn_Help.BindOnClickButtonEvent(OnClick_Help);
    }
    private void Update()
    {
        UpdateMoneyUI();
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
        DaniTechUIManager.Instance.CloseLuckyDrawUI();
    }

    private void OnClick_LuckyPlay()
    {
        // 횟수 체크
        if (TimeManager.Instance.CanDoActivity("activity_LuckyDraw_01"))
        {
            DaniTechUIManager.Instance.OpenLuckyDrawingPopupUI();
        }
        else
        {
            DaniTechUIManager.Instance.OpenMiniPopupUI("금일 복권 추첨 최대 횟수 도달");
        }
    }

    private void OnClick_Help()
    {
        DaniTechUIManager.Instance.OpenLuckyDrawHelpPopupUI();
    }
}