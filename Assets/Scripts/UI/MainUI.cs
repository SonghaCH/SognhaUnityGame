using UnityEngine;
using UnityEngine.UI;

public class MainUI : DaniTechUIBase
{
    [SerializeField] private Image Image_Time;
    [SerializeField] private Text Text_Time;

    [SerializeField] private DaniTechUIButton Button_Option;
    [SerializeField] private DaniTechUIButton Button_Map;


    [SerializeField] private Text Text_Health;
    [SerializeField] private Text Text_Int;
    [SerializeField] private Text Text_Charm;
    [SerializeField] private Text Text_Money;


    private void OnEnable()
    {
        Button_Option.BindOnClickButtonEvent(Onclick_Option);
        Button_Map.BindOnClickButtonEvent(Onclick_Map);
    }


    private void Onclick_Option()
    {
        Debug.Log("설정 열기");
    }

    private void Onclick_Map()
    {
        Debug.Log("지도 열기");
    }
}
