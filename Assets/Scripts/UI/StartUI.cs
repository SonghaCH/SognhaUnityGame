using UnityEngine;

public class StartUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_Start;
    [SerializeField] private DaniTechUIButton Btn_Option;
    [SerializeField] private DaniTechUIButton Btn_Quit;
    
    //ToDo임시 Start 변경
    private void OnEnable()
    {
        Btn_Start.BindOnClickButtonEvent(OnClick_GameStart);
        Btn_Option.BindOnClickButtonEvent(OnClick_OpenOption);
        Btn_Quit.BindOnClickButtonEvent(OnClick_GameQuit);
    }


    public void OnClick_GameStart()
    {
        Debug.Log("게임 시작 버튼 ");
        DaniTechUIManager.Instance.CloseContentUI(DaniTechUIType.StartUI);
        DaniTechUIManager.Instance.OpenMSGDialogueUI();

    }

    public void OnClick_OpenOption()
    {
        Debug.Log("설정 버튼 ");
    }

    public void OnClick_GameQuit()
    {
        Debug.Log("게임 종료 버튼 ");
        DaniTechUIManager.Instance.OpenQuitPopup();
    }

}
