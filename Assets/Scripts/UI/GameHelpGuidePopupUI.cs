using UnityEngine;

public class GameHelpGuidePopupUI : DaniTechUIBase
{
    [SerializeField] DaniTechUIButton Btn_Close;

    private void OnEnable()
    {
        Btn_Close.BindOnClickButtonEvent(Onclick_Close);
    }

    private void Onclick_Close()
    {
        DaniTechUIManager.Instance.CloseGameHelpGuidePopupUI();
    }
}
