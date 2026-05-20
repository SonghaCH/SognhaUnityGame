using UnityEngine;

public enum DaniTechUIRootType
{
    None = 0,
    BackgroundUI,
    MainUI,
    ContentUI,
    PopupUI,
    VeryFrontUI
}

public enum DaniTechUIType
{
    StartUI,
    StartLoadingUI,
    QuitPopupUI,
    MSGDialogueUI,
    DateDialogueUI,
    NomalDialogueUI,

    DNSimplePopup,
    DNMainUI,
    DNMyProfilePopup, 
    DNInventory,
    DNLoadingUI,
    DNDialogueUI,
    DNInfoBookUI
}

public static class DaniTechUIManagerExtension
{
    public static string GetUIPath(this DaniTechUIManager uiManager, DaniTechUIRootType uiRootType, DaniTechUIType uiType)
    {
        string path = string.Empty; // "" == string.Empty

        // 신규UI추가 2) Resources.Load를 할 경로를 직접 명시한다
        // 해당 경로는 프로젝트창에서 Resources/Prefabs/UI폴더 내에 있는 RootType 폴더명과 UIType 프리팹 이름과 동일해야 한다! (ex. ContentUI/DNMyProfilePopup)
        path = $"Prefabs/UI/{uiRootType}/{uiType}";
        return path;
    }

    public static void ShowStartupUIOnGameStart(this DaniTechUIManager uiManager)
    {
        uiManager.OpenStartLoadingUI();
        uiManager.OpenStartUI();


        // 게임 로비 UI를 여기서 오픈해주자 -> uiManager.
        // MainUI도
    }

    public static void OpenQuitPopup(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenPopupUI(DaniTechUIType.QuitPopupUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
       
    }

    public static void OpenStartUI(this DaniTechUIManager uiManger)
    {
        var uiBase = uiManger.OpenStartUI(DaniTechUIType.StartUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }

    public static void OpenStartLoadingUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.StartLoadingUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }
    public static void CloseLoadingUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.StartLoadingUI);
    }

    public static void OpenQuitPopupUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenUI(DaniTechUIRootType.PopupUI, DaniTechUIType.QuitPopupUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }

    public static void OpenMSGDialogueUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenUI(DaniTechUIRootType.ContentUI, DaniTechUIType.MSGDialogueUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }
    public static void CloseMSGDialogueUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.ContentUI, DaniTechUIType.MSGDialogueUI);
    }

    public static void OpenDateDialogueUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.DateDialogueUI,false);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }

    public static void CloseDateDialogueUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.DateDialogueUI);
    }

    public static void OpenNomalDialogueUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenUI(DaniTechUIRootType.ContentUI, DaniTechUIType.NomalDialogueUI, false);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }

    public static void CloseNomalDialogueUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.ContentUI, DaniTechUIType.NomalDialogueUI);
    }





    //public static void OpenDialogueUI(this DaniTechUIManager uiManager, string startDialogueId)
    //{
    //    var uiBase = uiManager.OpenContentUI(DaniTechUIType.DNDialogueUI);
    //    if(uiBase == null)
    //    {
    //        Debug.LogWarning($"UI가 생성되지 않았습니다");
    //        return;
    //    }

    //    if (uiBase is DaniTech_DialogueUI dialogueUi)
    //    {
    //        dialogueUi.StartDialogue(startDialogueId);
    //    }
    //}
}

