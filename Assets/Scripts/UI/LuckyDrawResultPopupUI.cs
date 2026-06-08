using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class LuckyDrawResultPopupUI : DaniTechUIBase
{
    [SerializeField] private DaniTechUIButton Btn_Close;
    [SerializeField] private Image Image_Result;
    [SerializeField] private Text Text_AddMoney;
    [SerializeField] private string openSoundName = "SFX_Result";


    // [핵심] 여기서 인스펙터에 드래그 앤 드롭으로 에셋을 할당합니다.
    [SerializeField] private AssetReferenceSprite[] BallReferences;

    private void OnEnable()
    {
        Btn_Close.BindOnClickButtonEvent(Onclick_Close);
        SoundManager.Instance.PlaySFX(openSoundName, 0.7f);

    }

    public void SetResult(int ballIndex, int prize, string colorName)
    {
        Text_AddMoney.text = $"당첨 구슬: {colorName}색, {prize}원 획득!";

        if (ballIndex >= 0 && ballIndex < BallReferences.Length)
        {
            var assetRef = BallReferences[ballIndex];

            // 1. 이미 로드되었는지 확인 (Handle이 유효하고 성공 상태인지)
            if (assetRef.OperationHandle.IsValid() && assetRef.OperationHandle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                Image_Result.sprite = (Sprite)assetRef.OperationHandle.Result;
            }
            else
            {
                // 2. 로드된 적이 없다면 새로 로드
                assetRef.LoadAssetAsync<Sprite>().Completed += (handle) =>
                {
                    if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                    {
                        Image_Result.sprite = handle.Result;
                    }
                };
            }
        }
    }

    private void Onclick_Close()
    {
        DaniTechUIManager.Instance.CloseLuckyDrawResultPopupUI();
    }
}