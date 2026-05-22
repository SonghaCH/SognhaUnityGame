using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MindDialogueUI : DaniTechUIBase
{
    [SerializeField] private Text Text_Description;
    [SerializeField] private DaniTechUIButton Btn_Next;

    private string _currentDialogueId;

    // <np>로 쪼개진 나레이션 조각들을 담아둘 큐
    //private Queue<string> _descriptionQueue = new Queue<string>();

    private void OnEnable()
    {
        Btn_Next.BindOnClickButtonEvent(OnClick_Next);
    }

    /// <summary>
    /// 내면 다이얼로그에서 Next 버튼이 눌러질 때 호출된다
    /// </summary>
    public void OnClick_Next()
    {
        DialogueManager.Instance.RequestNextDialogue(_currentDialogueId);
    }

    /// <summary>
    /// [매니저 연동 함수] DialogueManager가 내면 나레이션 프리팹을 켜면서 데이터를 주입할 때 호출하는 함수
    /// </summary>
    public void SetupDialogue(string dialogueId, string description)
    {
        _currentDialogueId = dialogueId;
        Text_Description.text = description;
    }
    

    
}