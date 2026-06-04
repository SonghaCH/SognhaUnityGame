using UnityEngine;
using UnityEngine.UI;

public class PCUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_Exit;
    [SerializeField] private DaniTechUIButton Btn_Game;
    [SerializeField] private DaniTechUIButton Btn_Help;
    [SerializeField] private Text Text_Money;

    private void OnEnable()
    {
        Btn_Exit.BindOnClickButtonEvent(OnClick_Exit);
        Btn_Game.BindOnClickButtonEvent(OnClick_Game);
        Btn_Help.BindOnClickButtonEvent(OnClick_Help);
    }

    private void OnClick_Exit()
    {
        DaniTechUIManager.Instance.ClosePCUI();
    }

    private void OnClick_Game()
    {
        // 횟수 체크
        if (TimeManager.Instance.CanDoActivity("activity_GameRoom_01"))
        {
            DaniTechUIManager.Instance.OpenPCGamePlayPopupUI();
        }
        else
        {
            DaniTechUIManager.Instance.OpenMiniPopupUI("금일 게임 최대 할댱량 도달  ");
        }
    }

    private void OnClick_Help()
    {
        DaniTechUIManager.Instance.OpenPCHelpPopupUI();
    }
}
