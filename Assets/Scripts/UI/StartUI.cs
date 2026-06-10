using UnityEngine;

public class StartUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_Start;
    [SerializeField] private DaniTechUIButton Btn_Quit;
    [SerializeField] private string bgmName = "BGM_Start";
    private void OnEnable()
    {
        Btn_Start.BindOnClickButtonEvent(OnClick_GameStart);
        Btn_Quit.BindOnClickButtonEvent(OnClick_GameQuit);

        if (!string.IsNullOrEmpty(bgmName))
        {
            SoundManager.Instance.PlayBGM("BGM_Start");
        }
    }


    public void OnClick_GameStart()
    {
        Debug.Log("게임 시작 버튼 ");
        DaniTechUIManager.Instance.CloseContentUI(DaniTechUIType.StartUI);
        DialogueManager.Instance.StartDialogueFlow("dateDialogue_Opening_1_1_100");
        DaniTechUIManager.Instance.OpenSkipUI();
    }

    public void OnClick_GameQuit()
    {
        Debug.Log("게임 종료 버튼 ");
        DaniTechUIManager.Instance.OpenQuitPopup();
    }

}
