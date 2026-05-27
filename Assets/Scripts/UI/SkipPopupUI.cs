using UnityEngine;

public class SkipPopupUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_SkipYes;
    [SerializeField] private DaniTechUIButton Btn_SkipNo;

    private void OnEnable()
    {
        Btn_SkipYes.BindOnClickButtonEvent(Onclick_QuitYes);
        Btn_SkipNo.BindOnClickButtonEvent(Onclick_QuitNo);

    }


    public void Onclick_QuitYes()
    {
        Debug.Log("스킵 하였습니다.");

        DaniTechUIManager.Instance.ClosePopupUI(DaniTechUIType.SkipPopupUI);

        DialogueManager.Instance.StartDialogueFlow("dateDialogue_Opening_1_1_286");
    }

    public void Onclick_QuitNo()
    {
        Debug.Log("되돌아가기");
        DaniTechUIManager.Instance.ClosePopupUI(DaniTechUIType.SkipPopupUI);
    }
}
