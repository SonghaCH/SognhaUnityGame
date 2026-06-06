using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class StartLoadingUI : DaniTechUIBase
{
    [SerializeField] private Slider Slider_LoadingBar;

    private CancellationTokenSource _cancelToken;
    float[] _pausePoints = { 0.1f, 0.1f, 0.1f };
    int _pauseIndex = 0;

    private void OnEnable()
    {
        StartLoadResource(3.0f).Forget();
    }

    private async UniTaskVoid StartLoadResource(float duration)
    {
        _cancelToken = new CancellationTokenSource();

        float elapsed = 0f;
        Slider_LoadingBar.value = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float progress = Mathf.Clamp01(elapsed / duration);

            if (_pauseIndex < _pausePoints.Length && progress >= _pausePoints[_pauseIndex])
            {
                float pausePointValue = _pausePoints[_pauseIndex];
                Slider_LoadingBar.value = pausePointValue;
                await UniTask.Delay(TimeSpan.FromSeconds(pausePointValue), cancellationToken: _cancelToken.Token);
                _pauseIndex++;
            }

            Slider_LoadingBar.value = progress;

            await UniTask.Yield(PlayerLoopTiming.Update, _cancelToken.Token);
        }
        Slider_LoadingBar.value = 2.0f;
        DaniTechUIManager.Instance.CloseLoadingUI();


    }

}
