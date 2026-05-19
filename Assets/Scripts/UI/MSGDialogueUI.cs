using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MSGDialogueUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_MSGPlaying;
    [SerializeField] private DaniTechUIButton Btn_MindNext;
    [SerializeField] private Text Text_Description;


    private string _currentDialogueId;
    private Queue<string> _descriptionQueue = new Queue<string>();

    private void OnEnable()
    {
        Btn_MSGPlaying.BindOnClickButtonEvent(Onclick_MSGPlay);
        Btn_MindNext.BindOnClickButtonEvent(Onclick_MindNext);
    }



    public void Onclick_MSGPlay()
    {
        Debug.Log("카톡 실행");
    }

    public void Onclick_MindNext()
    {
        Debug.Log("내면의 소리");

    }

}

