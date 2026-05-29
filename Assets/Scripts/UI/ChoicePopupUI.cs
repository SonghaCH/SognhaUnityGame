using UnityEngine;

public class ChoicePopupUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_ChoiceYes;
    [SerializeField] private DaniTechUIButton Btn_ChoiceNo;

    private void OnEnable()
    {
        Btn_ChoiceYes.BindOnClickButtonEvent(Onclick_ChoiceYes);
        Btn_ChoiceNo.BindOnClickButtonEvent(Onclick_ChoiceNo);

    }




    private void Onclick_ChoiceYes()
    {
        Debug.Log("메인 게임 시작!");
        DaniTechUIManager.Instance.CloseChoiceUI();
        DialogueManager.Instance.StartDialogueFlow("mindDialogue_Opening_1_1_307_2");

    }

    private void Onclick_ChoiceNo()
    {
        Debug.Log("돌아가기 버튼 작동");
        DaniTechUIManager.Instance.CloseChoiceUI();
        DialogueManager.Instance.StartDialogueFlow("mindDialogue_Opening_1_1_307_3");


    }
}
