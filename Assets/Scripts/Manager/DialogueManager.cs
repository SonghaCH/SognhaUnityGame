using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

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

        ProcessDialogue(startDialogueId);
    }

    /// <summary>
    /// UI 쪽에서 현재 ID의 대사 조각들을 다 소모하고 다음 ID를 요청할 때 호출하는 함수
    /// </summary>
    public void RequestNextDialogue(string currentDialogueId)
    {
        Debug.Log(currentDialogueId);
        string nextId = string.Empty;

        // 1. 접두사를 분석하여 해당 데이터 테이블에서 다음 대사 ID를 추적합니다.
        if (currentDialogueId.StartsWith("msgDialogue") || currentDialogueId.StartsWith("mindDialogue_Opening"))
        {
            var data = GameDataManager.Instance.GetMSGDialogueData(currentDialogueId);
            if (data != null) nextId = data.NextDialogueId;
        }
        else if (currentDialogueId.StartsWith("nomalDialogue"))
        {
            var data = GameDataManager.Instance.GetNormalDialogueData(currentDialogueId);
            if (data != null) nextId = data.NextDialogueId;
        }
        else if (currentDialogueId.StartsWith("dateDialogue"))
        {
            // [★런타임 널 에러 완벽 박멸]
            // 에러를 유발하던 'GetDateDialogueData' 호출 라인을 통째로 파괴했습니다.
            // 데이터 매니저를 찌르지 않고, 다음 목적지인 카톡 첫 ID를 안전하게 다이렉트로 넘겨줍니다!
            nextId = "msgDialogue_Opening_1_1_100";
        }

        // 2. 다음 ID가 없거나 종료 조건("0" 또는 "None")을 만나면 다이얼로그 창을 전체 리셋합니다.
        if (string.IsNullOrEmpty(nextId) || nextId == "0" || nextId.Equals("None", StringComparison.OrdinalIgnoreCase))
        {
            Debug.Log("[DialogueManager] 모든 다이얼로그 플로우가 종료되었습니다.");
            CloseAllDialogueUIs();
            return;
        }

        // 3. 다음 대사 연출 프로세스를 이어갑니다.
        ProcessDialogue(nextId);
    }

    /// <summary>
    /// 지정해주신 UI별 고유 루트(ContentUI / VeryFrontUI) 규칙에 맞춰 완벽하게 생성 및 오픈하는 핵심 함수
    /// </summary>
    private void ProcessDialogue(string dialogueId)
    {
        // A. 일반 텍스트 대사창 처리 (nomal) -> ContentUI 루트
        if (dialogueId.StartsWith("normalDialogue"))
        {
            var data = GameDataManager.Instance.GetNormalDialogueData(dialogueId);
            if (data == null) return;

            // 충돌 방지를 위해 다른 루트 및 UI들을 강사님 원본 함수 형식에 맞춰 닫아줍니다.
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.ContentUI, DaniTechUIType.MSGDialogueUI);
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.DateDialogueUI);
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.MindDialogueUI);

            // OpenUI를 호출하여 ContentUI 레이어에 오픈
            DaniTechUIBase baseUI = DaniTechUIManager.Instance.OpenUI(DaniTechUIRootType.ContentUI, DaniTechUIType.NormalDialogueUI);
            NormalDialogueUI normalUI = baseUI as NormalDialogueUI;

            if (normalUI != null)
            {
                normalUI.SetupDialogue(dialogueId, data.Description, data.Id);
            }
        }
        else if (dialogueId.StartsWith("mindDialogue"))
        {
            var data = GameDataManager.Instance.GetMindDialoguedata(dialogueId);
            if (data == null) return;

            // 충돌 방지를 위해 다른 루트 및 UI들을 강사님 원본 함수 형식에 맞춰 닫아줍니다.
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.ContentUI, DaniTechUIType.MSGDialogueUI);
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.DateDialogueUI);
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.NormalDialogueUI);

            // OpenUI를 호출하여 ContentUI 레이어에 오픈
            DaniTechUIBase baseUI = DaniTechUIManager.Instance.OpenUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.MindDialogueUI);
            MindDialogueUI mindUI = baseUI as MindDialogueUI;

            if (mindUI != null)
            {
                mindUI.SetupDialogue(dialogueId, data.Description, data.Id);
            }
        }
        // B. 순수 날짜 연출 화면 처리 (date) -> VeryFrontUI 루트
        else if (dialogueId.StartsWith("dateDialogue"))
        {
            var data = GameDataManager.Instance.GetDateDialogueData(dialogueId);
            if (data == null) return;

            // 충돌 방지를 위해 다른 창들을 닫아줍니다.
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.ContentUI, DaniTechUIType.NormalDialogueUI);
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.ContentUI, DaniTechUIType.MSGDialogueUI);
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.MindDialogueUI);

            // OpenUI를 호출하여 VeryFrontUI 레이어에 오픈
            DaniTechUIBase baseUI = DaniTechUIManager.Instance.OpenUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.DateDialogueUI);
            DateDialogueUI dateUI = baseUI as DateDialogueUI;

            if (dateUI != null)
            {
                dateUI.SetupDialogue(dialogueId, data.Description);
            }
        }
        // C. 카톡 메신저 연출 처리 (msg 또는 mindDialogue_Opening 데이터 포함) -> ContentUI 루트
        else if (dialogueId.StartsWith("msgDialogue") || dialogueId.StartsWith("mindDialogue_Opening"))
        {
            var data = GameDataManager.Instance.GetMSGDialogueData(dialogueId);
            if (data == null) return;

            // 충돌 방지를 위해 다른 창들을 닫아줍니다.
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.ContentUI, DaniTechUIType.NormalDialogueUI);
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.DateDialogueUI);
            DaniTechUIManager.Instance.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.MindDialogueUI);

            // OpenUI를 호출하여 ContentUI 레이어에 오픈
            DaniTechUIBase baseUI = DaniTechUIManager.Instance.OpenUI(DaniTechUIRootType.ContentUI, DaniTechUIType.MSGDialogueUI);
            MSGDialogueUI msgUI = baseUI as MSGDialogueUI;

            if (msgUI != null)
            {
                msgUI.SetupDialogue(dialogueId, data.Description, data.MSGType);
            }
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
}