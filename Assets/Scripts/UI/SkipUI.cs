using UnityEngine;

public class SkipUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_Skip;

    private void OnEnable()
    {
        Btn_Skip.BindOnClickButtonEvent(Onclick_Skip);
    }



    public void Onclick_Skip()
    {
        DaniTechUIManager.Instance.OpenSkipPopupUI();
        Debug.Log("오프닝 스킵");
    }
}
