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

        // [중요] UI가 열릴 때 즉시 소지금 표시
    }

    private void Update()
    {
        UpdateMoneyUI();
    }

    // 소지금 UI를 갱신하는 함수
    public void UpdateMoneyUI()
    {
        if (Text_Money != null && StatManager.Instance != null)
        {
            // 소지금을 콤마(,)가 포함된 문자열로 표시
            Text_Money.text = $"{StatManager.Instance.Money:N0} 원";
        }
    }

    
    
    private void OnClick_Exit()
    {
        DaniTechUIManager.Instance.CloseAcademyUI();
    }


    private void OnClick_Study()
    {
        if (TimeManager.Instance.CanDoActivity("activity_Academy_01"))
        {
            DaniTechUIManager.Instance.OpenAcademyStudyPopupUI();
        }
        else
        {
            DaniTechUIManager.Instance.OpenMiniPopupUI("금일 공부 최대 할당량 도달");
        }
    }

    private void OnClick_Help()
    {
        DaniTechUIManager.Instance.OpenAcademyHelpPopupUI();
    }
}