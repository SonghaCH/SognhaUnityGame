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

    private void OnClick_Exit()
    {
        Debug.Log("sfasfsaf");
        DaniTechUIManager.Instance.CloseLuckyDrawUI();
    }

    private void OnClick_LuckyPlay()
    {
        Debug.Log("시작버튼 누름");
        DaniTechUIManager.Instance.OpenLuckyDrawingPopupUI();
    }

    private void OnClick_Help()
    {
        DaniTechUIManager.Instance.OpenLuckyDrawHelpPopupUI();
    }


}
