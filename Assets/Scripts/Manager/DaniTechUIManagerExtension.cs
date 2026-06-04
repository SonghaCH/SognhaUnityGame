using System;
using UnityEngine;
using UnityEngine.U2D;

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

    SkipUI,
    SkipPopupUI,

    ChoicePopupUI,
    
    MSGDialogueUI,
    Slot_MyChatUI,
    Slot_OtherUI,
    Slot_DateBoxUI,

    MainUI,




    DateDialogueUI,
    NormalDialogueUI,
    MindDialogueUI,
    
    BackgroundUI,
    MapPopupUI,

    LuckyDrawUI,
    LuckyDrawResultPopupUI,
    LuckyDrawHelpPopupUI,
    LuckyDrawingPopupUI,

    GymUI,
    GymHelpPopupUI,
    GymExercisingPopupUI,

    CUUI,
    CUHelpPopupUI,
    CUWorkingPopupUI,


    AcademyUI,
    AcademyHelpPopupUI,
    AcademyStudyPopupUI,

    BarberShopUI,
    BarberShopHelpPopupUI,
    BaberShopCharmPopupUI,

    MiniPopupUI,


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

    public static void OpenSkipUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenPopupUI(DaniTechUIType.SkipUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }
    public static void CloseSkipUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.SkipUI);
    }



    public static void OpenSkipPopupUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenPopupUI(DaniTechUIType.SkipPopupUI);
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

    public static void OpenNormalDialogueUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenUI(DaniTechUIRootType.ContentUI, DaniTechUIType.NormalDialogueUI, false);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }

    public static void CloseNomalDialogueUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.ContentUI, DaniTechUIType.NormalDialogueUI);
    }

    public static void OpenMindDialogueUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.MindDialogueUI, false);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }
    public static void CloseMindDialogueUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.MindDialogueUI);
    }

    public static void OpenBackgroundUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenUI(DaniTechUIRootType.BackgroundUI, DaniTechUIType.BackgroundUI, false);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }
    public static void CloseBackgroundUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.BackgroundUI, DaniTechUIType.BackgroundUI);
    }



    public static void OpenChoicePopupUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenPopupUI(DaniTechUIType.ChoicePopupUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }

    public static void CloseChoiceUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.PopupUI, DaniTechUIType.ChoicePopupUI);
    }


    public static void OpenMainUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenUI(DaniTechUIRootType.MainUI, DaniTechUIType.MainUI, false);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }

    public static void CloseMainUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.MainUI, DaniTechUIType.MainUI);
    }
    
    public static void OpenMapPopupUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenPopupUI(DaniTechUIType.MapPopupUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }

    }

    public static void CloseMapPopupUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.PopupUI, DaniTechUIType.MapPopupUI);
    }


    public static void OpenLuckyDrawHelpPopupUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenPopupUI(DaniTechUIType.LuckyDrawHelpPopupUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }

    }

    public static void CloseLuckyDrawHelpPopupUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.PopupUI, DaniTechUIType.LuckyDrawHelpPopupUI);
    }



    public static void OpenLuckyDrawingPopupUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenPopupUI(DaniTechUIType.LuckyDrawingPopupUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }

    }

    public static void CloseLuckyDrawingPopupUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.PopupUI, DaniTechUIType.LuckyDrawingPopupUI);
    }


    public static void OpenLuckyDrawResultPopupUI(this DaniTechUIManager uiManager, int ballIndex, int prize, string colorName)
    {
        var uiBase = uiManager.OpenPopupUI(DaniTechUIType.LuckyDrawResultPopupUI);
        if (uiBase == null)
        {
            Debug.LogWarning("UI가 생성되지 않았습니다");
            return;
        }

        // [핵심] 생성된 UI에 데이터 전달하기
        // uiBase를 LuckyDrawResultPopupUI 타입으로 변환(Cast)해서 SetResult를 호출합니다.
        var resultPopup = uiBase as LuckyDrawResultPopupUI;
        if (resultPopup != null)
        {
            resultPopup.SetResult(ballIndex, prize, colorName);
        }
    }

    public static void CloseLuckyDrawResultPopupUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.PopupUI, DaniTechUIType.LuckyDrawResultPopupUI);
    }


    public static void OpenMiniPopupUI(this DaniTechUIManager uiManager, string message)
    {
        var uiBase = uiManager.OpenPopupUI(DaniTechUIType.MiniPopupUI);
        if (uiBase == null)
        {
            Debug.LogWarning("UI가 생성되지 않았습니다");
            return;
        }

        // [핵심] 생성된 팝업이 우리가 만든 NotificationPopupUI 타입이라면 메시지 전달!
        if (uiBase is MiniPopupUI miniPopup)
        {
            miniPopup.SetMessage(message);
        }
    }
    public static void CloseMiniPopupUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.PopupUI, DaniTechUIType.MiniPopupUI);
    }





    public static void OpenLuckyDrawUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.LuckyDrawUI, false);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }

    public static void CloseLuckyDrawUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.LuckyDrawUI);
    }

    public static void OpenGymUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.GymUI, false);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }

    public static void CloseGymUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.GymUI);
    }




    public static void OpenGymHelpPopupUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenPopupUI(DaniTechUIType.GymHelpPopupUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }

    }
    public static void CloseGymHelpPopupUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.PopupUI, DaniTechUIType.GymHelpPopupUI);
    }




    public static void OpenGymExercisingPopupUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenPopupUI(DaniTechUIType.GymExercisingPopupUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }

    }

    public static void CloseGymExercisingPopupUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.PopupUI, DaniTechUIType.GymExercisingPopupUI);
    }





    public static void OpenCUUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.CUUI, false);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }

    public static void CloseCUUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.CUUI);
    }

    public static void OpenCUHelpPopupUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenPopupUI(DaniTechUIType.CUHelpPopupUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }

    }

    public static void CloseCUHelpPopupUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.PopupUI, DaniTechUIType.CUHelpPopupUI);
    }


    public static void OpenCUWorkingPopupUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenPopupUI(DaniTechUIType.CUWorkingPopupUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }

    }

    public static void CloseCUWorkingPopupUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.PopupUI, DaniTechUIType.CUWorkingPopupUI);
    }

    public static void OpenAcademyUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.AcademyUI, false);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }

    public static void CloseAcademyUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.AcademyUI);
    }


    public static void OpenAcademyHelpPopupUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenPopupUI(DaniTechUIType.AcademyHelpPopupUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }

    }

    public static void CloseAcademyHelpPopupUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.PopupUI, DaniTechUIType.AcademyHelpPopupUI);
    }

    public static void OpenAcademyStudyPopupUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenPopupUI(DaniTechUIType.AcademyStudyPopupUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }

    }
    public static void CloseAcademyStudyPopupUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.PopupUI, DaniTechUIType.AcademyStudyPopupUI);
    }

    public static void OpenBarberShopUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.BarberShopUI, false);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }
    }

    public static void CloseBarberShopUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.VeryFrontUI, DaniTechUIType.BarberShopUI);
    }



    public static void OpenBarberShopHelpPopupUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenPopupUI(DaniTechUIType.BarberShopHelpPopupUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }

    }

    public static void CloseBarberShopHelpPopupUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.PopupUI, DaniTechUIType.BarberShopHelpPopupUI);
    }

    public static void OpenBaberShopCharmPopupUI(this DaniTechUIManager uiManager)
    {
        var uiBase = uiManager.OpenPopupUI(DaniTechUIType.BaberShopCharmPopupUI);
        if (uiBase == null)
        {
            Debug.LogWarning($"UI가 생성되지 않았습니다");
            return;
        }

    }

    public static void CloseBaberShopCharmPopupUI(this DaniTechUIManager uiManager)
    {
        uiManager.CloseUI(DaniTechUIRootType.PopupUI, DaniTechUIType.BaberShopCharmPopupUI);
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

