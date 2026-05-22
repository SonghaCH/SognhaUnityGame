using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSGDialogueUI : DaniTechUIBase
{
    [Header("[하이라키 내부 컴포넌트 연결]")]
    [SerializeField] private Transform Content_Scroll;    // 카톡 슬롯들이 차례대로 누적될 정렬 컴포넌트 (Content)
    [SerializeField] private ScrollRect Scroll_Rect;      // 메시지 누적 시 최하단 포커싱용 스크롤 컴포넌트
    [SerializeField] private DaniTechUIButton Btn_Next;   // 지정해주신 '진행 버튼' 하나만 상호작용하도록 바인딩

    private string _currentDialogueId;
    private string _msgTypeFromServer; // 엑셀의 B컬럼(MSGType) 문자열을 캐싱할 변수 ("DateMSG", "MyMSG", "OhterMSG")
    private string _msgDescription;
    private string _characterName;





    private void OnEnable()
    {
        // 진행 버튼 클릭 이벤트 연동
        Btn_Next.BindOnClickButtonEvent(OnClick_Next);
    }

    public void OnClick_Next()
    {
        // 현재 ID의 모든 조각 대사를 소모했다면 매니저에게 다음 데이터 라인을 요청합니다.
        DialogueManager.Instance.RequestNextDialogue(_currentDialogueId);
    }

    public void SetupDialogue(string dialogueId, string description, string msgType,string characterName)
    {
        _currentDialogueId = dialogueId;
        _msgTypeFromServer = msgType; // 매니저가 주입해준 엑셀의 기획 타입 문자열 저장
        _msgDescription = description;
        _characterName = characterName;



        SpawnNextChatSlot(_msgDescription, _msgTypeFromServer, _characterName);
    }

    /// <summary>
    /// 강사님의 UI 로드 경로 아키텍처를 순수하게 활용하여, 딕셔너리 캐싱 한계를 우회하고 무한 인스턴스화하는 핵심 로직
    /// </summary>
    private bool SpawnNextChatSlot(string msgDescription, string msgTypeFromServer,string characterName)
    {
        //string currentMsg = _msgQueue.Dequeue();

        // 1. 오직 엑셀 데이터만을 기준으로 타겟 에넘 타입을 정확히 분기합니다.
        DaniTechUIType slotUIType = GetChatSlotUIType();

        // 2. 익스텐션 메서드의 GetUIPath 규칙에 맞춰 리소스 경로를 조합합니다.
        string path = DaniTechUIManager.Instance.GetUIPath(DaniTechUIRootType.ContentUI, slotUIType);

        // 3. 리소스 폴더에서 직접 에셋을 로드한 뒤 스크롤뷰 Content 자식으로 복사 소환합니다.
        GameObject loadedPrefab = Resources.Load<GameObject>(path);

        var chacterData = GameDataManager.Instance.GetCharacterData(characterName);

        if (loadedPrefab != null)
        {
            GameObject slotGo = Instantiate(loadedPrefab, Content_Scroll);
            Slot_OtherUI slot = slotGo.GetComponent<Slot_OtherUI>();

            if(slotUIType == DaniTechUIType.Slot_DateBoxUI)
            {
                slot.Text_Description.text = msgDescription;
            }
            else if (slotUIType == DaniTechUIType.Slot_MyChatUI)
            {
                slot.Text_Description.text = msgDescription;
            }
            else
            {
                slot.Text_Name.text = characterName;
                slot.Text_Description.text = msgDescription;
            }

        }
        else
        {
            Debug.LogError($"[MSGDialogueUI] 프리팹 로드 실패! Resources/{path} 폴더 내 파일명과 에넘 규칙을 확인하세요.");
        }

        // 4. 새로운 슬롯이 생겼으므로 UI 버티컬 레이아웃 시스템을 즉시 강제 갱신하고 스크롤을 맨 밑(0)으로 내립니다.
        Canvas.ForceUpdateCanvases();
        if (Scroll_Rect != null)
        {
            Scroll_Rect.verticalNormalizedPosition = 0f;
        }

        return true;
    }



    /// <summary>
    /// [★데이터 드리븐 완전 동기화] 오직 엑셀 컬럼 정보만 보고 프리팹 슬롯 형식을 일치시켜 주는 함수
    /// </summary>
    private DaniTechUIType GetChatSlotUIType()
    {
        // A. 엑셀에 DateMSG라고 적혀있으면 날짜 박스 프리팹 매칭
        if (_msgTypeFromServer == "DateMSG")
        {
            return DaniTechUIType.Slot_DateBoxUI;
        }
        // B. 엑셀에 MyMSG라고 적혀있으면 주인공 노란색 말풍선 프리팹 매칭
        else if (_msgTypeFromServer == "MyMSG")
        {
            return DaniTechUIType.Slot_MyChatUI;
        }
        // C. 엑셀에 OhterMSG라고 적혀있으면 상대방 흰색 말풍선 프리팹 매칭 (오타 철자 일치 반영)
        else if (_msgTypeFromServer == "OhterMSG")
        {
            return DaniTechUIType.Slot_OtherUI;
        }

        // 어떠한 조건에도 맞지 않을 때 시스템 예외 방어용 기본 슬롯 리턴
        return DaniTechUIType.Slot_OtherUI;
    }
}