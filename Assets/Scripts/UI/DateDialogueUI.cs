using UnityEngine;
using UnityEngine.UI;

public class DateDialogueUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_NextButton;
    [SerializeField] private Text Text_Description;

    

    private void OnEnable()
    {
        DialogueLoad();
        Btn_NextButton.BindOnClickButtonEvent(Onclick_NextButton);
    }

    public void Onclick_NextButton()
    {
        Debug.Log("다음으로 넘어가기");
        DaniTechUIManager.Instance.OpenNomalDialogueUI();
        DaniTechUIManager.Instance.CloseDateDialogueUI();

    }

    public void DialogueLoad()
    {
        var dateData = GameDataManager.Instance.GetDateDialogueData("dateDialogue_Opening_1_1_100");
        if (dateData == null)
        {
            Debug.LogWarning("불러올 데이터가 없는데요?");
            return;
        }

        Text_Description.text = dateData.Description;

    }

}
