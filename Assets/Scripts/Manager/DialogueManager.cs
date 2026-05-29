using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    public event Action<Sprite> _OnChangeBackgroundImage;
    public string BeforeImagePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 외부(예: StartUI 게임 시작 버튼)에서 특정 대사 ID를 던져 스토리를 가동하는 시작점 함수
    /// </summary>
    public void StartDialogueFlow(string startDialogueId)
    {
        if (string.IsNullOrEmpty(startDialogueId))
        {
            Debug.LogWarning("[DialogueManager] 시작 다이얼로그 ID가 비어있습니다.");
            return;
        }

        DaniTechUIManager.Instance.OpenBackgroundUI();
        ProcessDialogue(startDialogueId);
    }

    /// <summary>
    /// UI 쪽에서 현재 ID의 대사 조각들을 다 소모하고 다음 ID를 요청할 때 호출하는 함수
    /// </summary>
    public void RequestNextDialogue(string currentDialogueId)
    {
        if (string.IsNullOrEmpty(currentDialogueId)) return;


        Debug.Log($"현재 다이얼로그ID: {currentDialogueId}");
        string nextId = string.Empty;

        // 1. 접두사를 한 번만 검사하여 깔끔하게 분기 처리 (switch문 활용)
        if (currentDialogueId.StartsWith("msgDialogue"))
        {
            var data = GameDataManager.Instance.GetMSGDialogueData(currentDialogueId);
            if (data != null) nextId = data.NextDialogueId;
        }
        else if (currentDialogueId.StartsWith("normalDialogue"))
        {
            var data = GameDataManager.Instance.GetNormalDialogueData(currentDialogueId);
            if (data != null) nextId = data.NextDialogueId;
        }
        else if (currentDialogueId.StartsWith("mindDialogue"))
        {
            var data = GameDataManager.Instance.GetMindDialogueData(currentDialogueId);
            if (data != null) nextId = data.NextDialogueId;
        }
        else if (currentDialogueId.StartsWith("dateDialogue"))
        {
            var data = GameDataManager.Instance.GetDateDialogueData(currentDialogueId);
            if (data != null) nextId = data.NextDialogueId;
        }
        else
        {
            Debug.LogWarning($"[DialogueManager] 알 수 없는 다이얼로그 ID 형식입니다: {currentDialogueId}");
        }

        Debug.Log($"다음 들어올 ID: {nextId}");

        // 2. 다음 ID가 없거나 종료 조건("0" 또는 "None")을 만나면 다이얼로그 창을 전체 리셋합니다.
        if (string.IsNullOrEmpty(nextId) || nextId == "0" || nextId.Equals("None", StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log($"넥스트아이디: {nextId}");
            Debug.Log("[DialogueManager] 모든 다이얼로그 플로우가 종료되었습니다.");
            CloseAllDialogueUIs();
            return;
        }


        if (currentDialogueId == "mindDialogue_Opening_1_1_303")
        {
            DaniTechUIManager.Instance.CloseSkipUI();
        }

        if (currentDialogueId == "mindDialogue_Opening_1_1_307")
        {
            DaniTechUIManager.Instance.OpenChoicePopupUI();
        }

        if (currentDialogueId == "mindDialogue_Opening_1_1_307_2")
        {
            DaniTechUIManager.Instance.OpenStartLoadingUI();
            DaniTechUIManager.Instance.CloseBackgroundUI();
            DaniTechUIManager.Instance.CloseMindDialogueUI();
            DaniTechUIManager.Instance.OpenMainUI();
        }
        

        ProcessDialogue(nextId);
    }

    /// <summary>
    /// 지정해주신 UI별 고유 루트(ContentUI / VeryFrontUI) 규칙에 맞춰 완벽하게 생성 및 오픈하는 핵심 함수
    /// </summary>
    private void ProcessDialogue(string dialogueId)
    {

        // A. 일반 텍스트 대사창 처리 (normal) -> ContentUI 루트
        if (dialogueId.StartsWith("normalDialogue"))
        {
            var data = GameDataManager.Instance.GetNormalDialogueData(dialogueId);
            if (data == null) return;

            // [★교정] 일반 대사창이 열릴 때도 마인드와 카톡창이 주소 꼬임 없이 깔끔하게 청소되도록 완벽 매칭!
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.ContentUI, DaniTechUIType.MSGDialogueUI);
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.DateDialogueUI);
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.MindDialogueUI);



            DaniTechUIBase baseUI = DaniTechUIManager.Instance.OpenUI(DaniTechUIRootType.ContentUI, DaniTechUIType.NormalDialogueUI);
            NormalDialogueUI normalUI = baseUI as NormalDialogueUI;
            if (normalUI != null)
            {
                normalUI.SetupDialogue(dialogueId, data.Description, data.CharacterDataId);
                CheckBGImage(data.BGImagePath);
            }
        }


        // B. 마인드 다이얼로그 처리 -> VeryFrontUI 루트에 온전히 보존하며 오픈!
        else if (dialogueId.StartsWith("mindDialogue"))
        {
            var data = GameDataManager.Instance.GetMindDialogueData(dialogueId);
            if(data == null)
            {
                Debug.LogWarning("데이터 없음");
                return;
            }

            DaniTechUIBase baseUI = DaniTechUIManager.Instance.OpenUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.MindDialogueUI);
            MindDialogueUI mindUI = baseUI as MindDialogueUI;

            // 1. 다른 겹치는 방해꾼 UI들만 먼저 꺼줍니다. (날짜창은 아직 살려둡니다!)
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.ContentUI, DaniTechUIType.NormalDialogueUI);
            //DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.ContentUI, DaniTechUIType.MSGDialogueUI);
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.DateDialogueUI);


            // 2. 목적지인 마인드 UI를 '먼저' 안전하게 생성합니다.
            

            if (mindUI != null)
            {
                mindUI.SetupDialogue(dialogueId, data.Description);
                CheckBGImage(data.BGImagePath);

            }

            // 3. [★완벽 방어선] 이제 소명을 다한 날짜창을 화면에서 완전히 지워버립니다! 
            // 이 줄이 실행되어야 날짜창이 꺼지면서 99번 마인드창이 눈앞에 나타납니다.
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.DateDialogueUI);
        }

        // C. 순수 날짜 연출 화면 처리 (date) -> VeryFrontUI 루트
        else if (dialogueId.StartsWith("dateDialogue"))
        {
            var data = GameDataManager.Instance.GetDateDialogueData(dialogueId);
            if (data == null) return;

            DaniTechUIBase baseUI = DaniTechUIManager.Instance.OpenUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.DateDialogueUI);
            DateDialogueUI dateUI = baseUI as DateDialogueUI;
            // 날짜창이 새로 켜질 때는 화면에 찌꺼기가 남지 않도록 마인드창을 포함해 싹 다 청소합니다.
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.ContentUI, DaniTechUIType.NormalDialogueUI);
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.ContentUI, DaniTechUIType.MSGDialogueUI);
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.MindDialogueUI);

          

            if (dateUI != null)
            {
                dateUI.SetupDialogue(dialogueId, data.Description);
                CheckBGImage(data.BGImagePath);

            }
        }
        // D. 카톡 메신저 연출 처리 -> 카톡창을 '먼저' 띄우고 마인드를 '나중에' 안전하게 닫습니다!
        else if (dialogueId.StartsWith("msgDialogue"))
        {
            var data = GameDataManager.Instance.GetMSGDialogueData(dialogueId);
            if (data == null) return;
            Debug.Log(data.characterDataId);
            var chacterData = GameDataManager.Instance.GetSHCharacterData(data.characterDataId);

            // 1. 일반 대사창은 먼저 닫아줍니다.
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.ContentUI, DaniTechUIType.NormalDialogueUI);

            // 2. 목적지인 카톡 UI를 메모리에 '먼저' 안전하게 활성화합니다.
            DaniTechUIBase baseUI = DaniTechUIManager.Instance.OpenUI(DaniTechUIRootType.ContentUI, DaniTechUIType.MSGDialogueUI);
            MSGDialogueUI msgUI = baseUI as MSGDialogueUI;

            if (msgUI != null)
            {
                msgUI.SetupDialogue(dialogueId, data.Description, data.MSGType,chacterData.Name);
                CheckBGImage(data.BGImagePath);

            }

            // 3. 목적지인 카톡창 세팅이 완전히 끝났으므로, 이제 소명을 다한 마인드 창을 '나중에' 깔끔하게 닫아줍니다!
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.DateDialogueUI);
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.MindDialogueUI);
        }
    }

    /// <summary>
    /// 대사 플로우가 완전히 끝났을 때 각자의 루트에 맞춰 깔끔하게 클로즈해주는 청소 함수
    /// </summary>
    private void CloseAllDialogueUIs()
    {
        DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.ContentUI, DaniTechUIType.NormalDialogueUI);
        DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.ContentUI, DaniTechUIType.MSGDialogueUI);
        DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.DateDialogueUI);
        DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.MindDialogueUI);
    }

    private void ChangeBackgroundImage(string BGpath)
    {
        DaniTechResourceManager.Inst.LoadSprite(BGpath, (sprite) =>
        {
            if (sprite == null)
                return;
            _OnChangeBackgroundImage?.Invoke(sprite);

        });
        
    }

    private void CheckBGImage(string nextImagePath)
    {

        if (BeforeImagePath != nextImagePath)
        {
            ChangeBackgroundImage(nextImagePath);
        }

        BeforeImagePath = nextImagePath;
    }

    


}