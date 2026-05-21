using UnityEngine;
using UnityEngine.UI;

public class DateDialogueUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_NextButton;
    [SerializeField] private Text Text_Description;

    private string _currentDialogueId;

    private void OnEnable()
    {
        // 넥스트 버튼 클릭 이벤트 바인딩
        Btn_NextButton.BindOnClickButtonEvent(Onclick_NextButton);
    }

    /// <summary>
    /// 버튼을 누르면 자기가 알아서 다른 UI를 여는 게 아니라, 매니저에게 다음 데이터 ID를 달라고 점잖게 요청합니다.
    /// </summary>
    public void Onclick_NextButton()
    {
        Debug.Log("[DateDialogueUI] 날짜 화면 클릭 - 매니저에게 다음 대사 요청");

        // [★정석 반영] 데이터 드리븐 흐름에 따라 매니저에게 바통을 넘깁니다.
        DialogueManager.Instance.RequestNextDialogue(_currentDialogueId);
    }

    /// <summary>
    /// [★매니저 연동] DialogueManager가 이 UI를 열어준 직후, 엑셀에서 읽어온 대사 데이터를 주입해주는 함수입니다.
    /// </summary>
    public void SetupDialogue(string dialogueId, string description)
    {
        _currentDialogueId = dialogueId;

        if (Text_Description != null)
        {
            Text_Description.text = description; // 엑셀에 적어둔 "2026년 4월 6일 월요일"이 주입됩니다.
        }
    }
}