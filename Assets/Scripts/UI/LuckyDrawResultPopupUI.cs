using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class LuckyDrawResultPopupUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_Close;
    [SerializeField] private Image Image_Result;
    [SerializeField] private Text Text_AddMoney;

    // [핵심] 여기서 인스펙터에 드래그 앤 드롭으로 에셋을 할당합니다.
    [SerializeField] private AssetReferenceSprite[] BallReferences;

    private void OnEnable()
    {
        Btn_Close.BindOnClickButtonEvent(Onclick_Close);
    }

    public void SetResult(int ballIndex, int prize, string colorName)
    {
        Text_AddMoney.text = $"당첨 구슬: {colorName}색, {prize}원 획득!";

        if (ballIndex >= 0 && ballIndex < BallReferences.Length)
        {
            // 드래그해서 넣은 에셋을 불러옵니다.
            BallReferences[ballIndex].LoadAssetAsync<Sprite>().Completed += (handle) =>
            {
                if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                {
                    Image_Result.sprite = handle.Result;
                }
            };
        }
    }

    private void Onclick_Close()
    {
        DaniTechUIManager.Instance.CloseLuckyDrawResultPopupUI();
    }
}