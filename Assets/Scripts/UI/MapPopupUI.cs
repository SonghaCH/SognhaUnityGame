using UnityEngine;

public class MapPopupUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_Clsoe;



    private void OnEnable()
    {
        Btn_Clsoe.BindOnClickButtonEvent(Onclick_Close);

    }

    private void Onclick_Close()
    {
        DaniTechUIManager.Instance.CloseMapPopupUI();
    }
}
