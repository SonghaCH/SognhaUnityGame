using UnityEngine;

public class LuckyDrawingPopupUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_Close;


    private void OnEnable()
    {
        Btn_Close.BindOnClickButtonEvent(Onclick_Close);
    }

    private void Onclick_Close()
    {
        DaniTechUIManager.Instance.CloseLuckyDrawingPopupUI();
    }
}
