using UnityEngine;
using UnityEngine.UI;

public class GymUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_Exit;
    [SerializeField] private DaniTechUIButton Btn_Exercise;
    [SerializeField] private DaniTechUIButton Btn_Help;
    [SerializeField] private Text Text_Money;

    private void OnEnable()
    {
        Btn_Exit.BindOnClickButtonEvent(OnClick_Exit);
        Btn_Exercise.BindOnClickButtonEvent(OnClick_Exercise);
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
        DaniTechUIManager.Instance.CloseGymUI();
    }

    private void OnClick_Exercise()
    {
        // 횟수 체크
        if (TimeManager.Instance.CanDoActivity("activity_Gym_01"))
        {
            DaniTechUIManager.Instance.OpenGymExercisingPopupUI();
        }
        else
        {
            DaniTechUIManager.Instance.OpenMiniPopupUI("금일 운동 최대 횟수 도달");
        }
    }

    private void OnClick_Help()
    {
        DaniTechUIManager.Instance.OpenGymHelpPopupUI();
    }
}
