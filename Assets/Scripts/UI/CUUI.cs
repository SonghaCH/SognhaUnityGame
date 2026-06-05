using UnityEngine;
using UnityEngine.UI;

public class CUUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_Exit;
    [SerializeField] private DaniTechUIButton Btn_Work;
    [SerializeField] private DaniTechUIButton Btn_Help;
    [SerializeField] private Text Text_Money;

    private void OnEnable()
    {
        Btn_Exit.BindOnClickButtonEvent(OnClick_Exit);
        Btn_Work.BindOnClickButtonEvent(OnClick_Work);
        Btn_Help.BindOnClickButtonEvent(OnClick_Help);


    }
    private void Update()
    {
        UpdateMoneyUI();
    }
    private void OnClick_Exit()
    {
        DaniTechUIManager.Instance.CloseCUUI();
    }

    public void UpdateMoneyUI()
    {
        if (Text_Money != null && StatManager.Instance != null)
        {
            Text_Money.text = $"{StatManager.Instance.Money:N0} 원";
        }
    }
    private void OnClick_Work()
    {
        // 횟수 체크
        if (TimeManager.Instance.CanDoActivity("activity_CUWork_01"))
        {
            DaniTechUIManager.Instance.OpenCUWorkingPopupUI();
        }
        else
        {
            DaniTechUIManager.Instance.OpenMiniPopupUI("금일 알바 최대 횟수 도달 ");
        }
    }

    private void OnClick_Help()
    {
        DaniTechUIManager.Instance.OpenCUHelpPopupUI();
    }


}
