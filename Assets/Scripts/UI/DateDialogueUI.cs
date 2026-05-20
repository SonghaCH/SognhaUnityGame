using UnityEngine;

public class DateDialogueUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_NextButton;

    

    private void OnEnable()
    {
        Btn_NextButton.BindOnClickButtonEvent(Onclick_NextButton);
    }

    public void Onclick_NextButton()
    {
        Debug.Log("다음으로 넘어가기");
        DaniTechUIManager.Instance.CloseDateDialogueUI();
    }

}
