using UnityEngine;

public class PCHelpPopupUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_Close;


    private void OnEnable()
    {
        Btn_Close.BindOnClickButtonEvent(Onclick_Close);
    }


    private void Onclick_Close()
    {
        DaniTechUIManager.Instance.ClosePCHelpPopupUI();
        Debug.Log("도움말 닫기");
    }
}
