using UnityEngine;
using UnityEngine.UI;

public class AcademyUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_Exit;
    [SerializeField] private DaniTechUIButton Btn_Study;
    [SerializeField] private DaniTechUIButton Btn_Help;
    [SerializeField] private Text Text_Money;

    private void OnEnable()
    {
        Btn_Exit.BindOnClickButtonEvent(OnClick_Exit);
        Btn_Study.BindOnClickButtonEvent(OnClick_Study);
        Btn_Help.BindOnClickButtonEvent(OnClick_Help);
    }

    private void OnClick_Exit()
    {
        DaniTechUIManager.Instance.CloseCUUI();
    }

    private void OnClick_Study()
    {
        // 횟수 체크
        if (TimeManager.Instance.CanDoActivity("activity_Academy_01"))
        {
            DaniTechUIManager.Instance.OpenCUWorkingPopupUI();
        }
        else
        {
            DaniTechUIManager.Instance.OpenMiniPopupUI("금일 공부 최대 할댱량 도달  ");
        }
    }

    private void OnClick_Help()
    {
        DaniTechUIManager.Instance.OpenCUHelpPopupUI();
    }
}
