using UnityEngine;

public class HomeHelpPopupUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_Close;

    private void OnEnable()
    {
        Btn_Close.BindOnClickButtonEvent(Onclick_Close);
    }

    private void Onclick_Close()
    {
        DaniTechUIManager.Instance.CloseHomeHelpPopupUI();
        Debug.Log("도움말 닫기");
    }
}
