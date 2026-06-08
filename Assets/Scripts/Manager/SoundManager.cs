using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    private Dictionary<string, AudioClip> _audioCache = new Dictionary<string, AudioClip>();
    private Stack<string> _bgmHistory = new Stack<string>(); // BGM 상태 기억용
    private Coroutine _fadeCoroutine;

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

    // --- BGM 제어 ---
    public void PlayBGM(string fileName, bool saveHistory = false, float fadeDuration = 0.5f)
    {
        if (saveHistory && bgmSource.clip != null) _bgmHistory.Push(bgmSource.clip.name);

        bgmSource.volume = 0.15f;

        if (_fadeCoroutine != null) StopCoroutine(_fadeCoroutine);
        _fadeCoroutine = StartCoroutine(FadeBGM(fileName, fadeDuration));
    }

    public void StopBGMAndRestore(float fadeDuration = 0.5f)
    {
        if (_bgmHistory.Count > 0) PlayBGM(_bgmHistory.Pop(), false, fadeDuration);
    }

    private IEnumerator FadeBGM(string fileName, float duration)
    {
        AudioClip nextClip = LoadAudio("Sounds/BGM/" + fileName);
        if (bgmSource.clip == nextClip) yield break;

        // 서서히 줄이기
        float startVolume = bgmSource.volume;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(startVolume, 0, t / duration);
            yield return null;
        }

        bgmSource.clip = nextClip;
        bgmSource.Play();

        // 서서히 키우기
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(0, startVolume, t / duration);
            yield return null;
        }
        bgmSource.volume = startVolume;
    }

    // --- SFX 제어 ---
    public void PlaySFX(string fileName, float volume = 0.5f)
    {
        AudioClip clip = LoadAudio("Sounds/SFX/" + fileName);
        if (clip != null) sfxSource.PlayOneShot(clip, volume);
    }

    // 캐릭터 ID 기반 소리 (파일명: Typing_캐릭터ID)
    public void PlayCharacterTypingSound(string characterId, float volume = 0.3f)
    {
        PlaySFX("Typing_" + characterId, volume);
    }

    // --- 유틸리티 ---
    private AudioClip LoadAudio(string path)
    {
        if (!_audioCache.TryGetValue(path, out AudioClip clip))
        {
            clip = Resources.Load<AudioClip>(path);
            if (clip == null) Debug.LogWarning($"[SoundManager] 사운드 파일을 찾을 수 없음: {path}");
            _audioCache[path] = clip;
        }
        return clip;
    }
}