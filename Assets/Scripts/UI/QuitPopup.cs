using UnityEngine;

public class QuitPopup : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_QuitYes;
    [SerializeField] private DaniTechUIButton Btn_QuitNO;

    private void OnEnable()
    {
        Btn_QuitYes.BindOnClickButtonEvent(Onclick_QuitYes);
        Btn_QuitNO.BindOnClickButtonEvent(Onclick_QuitNo);

    }


    public void Onclick_QuitYes()
    {
        Debug.Log("게임종료");
        Application.Quit();
    }

    public void Onclick_QuitNo()
    {
        Debug.Log("되돌아가기");
        DaniTechUIManager.Instance.ClosePopupUI(DaniTechUIType.QuitPopupUI);
    }
}
