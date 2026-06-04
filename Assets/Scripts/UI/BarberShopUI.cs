using UnityEngine;
using UnityEngine.UI;

public class BarberShopUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_Exit;
    [SerializeField] private DaniTechUIButton Btn_Charm;
    [SerializeField] private DaniTechUIButton Btn_Help;
    [SerializeField] private Text Text_Money;

    private void OnEnable()
    {
        Btn_Exit.BindOnClickButtonEvent(OnClick_Exit);
        Btn_Charm.BindOnClickButtonEvent(OnClick_Charm);
        Btn_Help.BindOnClickButtonEvent(OnClick_Help);
    }

    private void OnClick_Exit()
    {
        DaniTechUIManager.Instance.CloseBarberShopUI();
    }

    private void OnClick_Charm()
    {
        // 횟수 체크
        if (TimeManager.Instance.CanDoActivity("activity_BarerShop_01"))
        {
            DaniTechUIManager.Instance.OpenBaberShopCharmPopupUI();
        }
        else
        {
            DaniTechUIManager.Instance.OpenMiniPopupUI("금일 관리 횟수 도달");
        }
    }

    private void OnClick_Help()
    {
        DaniTechUIManager.Instance.OpenBarberShopHelpPopupUI();
    }
}
