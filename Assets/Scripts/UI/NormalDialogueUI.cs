using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalDialogueUI : DaniTechUIBase
{
    [SerializeField] private GameObject Layout_CharacterName;
    [SerializeField] private Text Text_Character;
    [SerializeField] private Text Text_Description;
    [SerializeField] private DaniTechUIButton Btn_Next;

    private string _currentDialogueId;

    // <np>로 쪼개진 대사 조각들을 순서대로 담아둘 큐
    //private Queue<string> _descriptionQueue = new Queue<string>();

    private void OnEnable()
    {
        Btn_Next.BindOnClickButtonEvent(OnClick_Next);
    }

    /// <summary>
    /// 다이얼로그에서 Next 버튼이 눌러질 때 호출된다
    /// </summary>
    public void OnClick_Next()
    {
        // 1. 현재 대사 ID 내부에 <np>로 쪼개진 다음 조각이 남았는지 체크하고 출력한다
        //bool isNextDescriptionExist = CheckAndSetDescription();

        // 만약 다음 조각이 있어서 화면에 방금 뿌려줬다면, 함수를 여기서 끝낸다 (다음 ID로 넘어가지 않음)
        //if (isNextDescriptionExist)
        //{
        //    return;
        //}

        // 2. <np> 조각을 모두 읽은 상태에서 Next를 또 눌렀다면, 매니저에게 다음 ID 처리를 요청한다!
        DialogueManager.Instance.RequestNextDialogue(_currentDialogueId);
    }

    /// <summary>
    /// [매니저 연동 함수] DialogueManager가 이 프리팹 UI를 켜면서 데이터를 심어줄 때 호출하는 함수
    /// </summary>
    public void SetupDialogue(string dialogueId, string description, string characterId)
    {
        _currentDialogueId = dialogueId;

        // 새로운 대사를 시작하므로 이전 대사의 찌꺼기를 깨끗하게 비워준다
        //_descriptionQueue.Clear();

        //// 엑셀에 적힌 대사 원문을 <np> 태그 기준으로 쪼갠다
        //string[] dialogueDescriptionList = description.Split(new string[] { "<np>" }, System.StringSplitOptions.None);

        //// 쪼개진 조각들을 대사 큐에 차곡차곡 채워 넣는다
        //foreach (string desc in dialogueDescriptionList)
        //{
        //    _descriptionQueue.Enqueue(desc);
        //}

        // 큐에 채워 넣었으니, 첫 번째 대사 조각을 즉시 꺼내서 화면에 보여준다
        //CheckAndSetDescription();

        // 화자(캐릭터) 이름을 세팅한다
        SetCharacterName(characterId);
    }

    /// <summary>
    /// 큐에서 다음 대사 조각을 꺼내 화면에 보여주는 함수
    /// </summary>
    //private bool CheckAndSetDescription()
    //{
    //    if (_descriptionQueue.Count > 0)
    //    {
    //        string desc = _descriptionQueue.Dequeue();
    //        SetCurrentDialogueDescription(desc);
    //        return true;
    //    }
    //    return false;
    //}

    /// <summary>
    /// 캐릭터 정보가 있다면 말하는 이의 이름을 표기해주는 함수
    /// </summary>
    private void SetCharacterName(string characterDataId)
    {
        bool isActive = (string.IsNullOrEmpty(characterDataId) == false);
        Layout_CharacterName.SetActive(isActive);

        if (isActive)
        {
            var characterData = GameDataManager.Instance.GetSHCharacterData(characterDataId);
            if (characterData != null)
            {
                Text_Character.text = characterData.Name;
            }
        }
    }

    /// <summary>
    /// 실제 유니티 UI 텍스트 컴포넌트에 글자를 반영하는 함수
    /// </summary>
    private void SetCurrentDialogueDescription(string description)
    {
        if (Text_Description != null)
        {
            Text_Description.text = description;
        }
    }
}