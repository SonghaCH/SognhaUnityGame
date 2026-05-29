using UnityEngine;
using UnityEngine.UI;

public class MainUI : DaniTechUIBase
{
    [SerializeField] private Image Image_Time;
    [SerializeField] private Text Text_Time;

    [SerializeField] private DaniTechUIButton Button_Option;

    [SerializeField] private Text Text_Health;
    [SerializeField] private Text Text_Int;
    [SerializeField] private Text Text_Charm;
    [SerializeField] private Text Text_Money;


    private void OnEnable()
    {
        Button_Option.BindOnClickButtonEvent(Onclick_Option);
    }


    private void Onclick_Option()
    {
        Debug.Log("설정 열기");
    }
}
