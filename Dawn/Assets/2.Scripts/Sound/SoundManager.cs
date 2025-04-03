using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public Dictionary<BGMType, AudioClip> bgmDict = new Dictionary<BGMType, AudioClip>();
    public Dictionary<SFXType, AudioClip> sfxDict = new Dictionary<SFXType, AudioClip>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitSoundManager()
    {
        GameObject obj = new GameObject("SoundManager");
        Instance = obj.AddComponent<SoundManager>();
        DontDestroyOnLoad(obj);

        //Set BGM
        GameObject bgmObj = new GameObject("BGM");
        SoundManager.Instance.bgmSource = bgmObj.AddComponent<AudioSource>();
        bgmObj.transform.SetParent(obj.transform);
        SoundManager.Instance.bgmSource.loop = true;
        SoundManager.Instance.bgmSource.volume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);

        //Set SFX
        GameObject sfxObj = new GameObject("SFX");
        SoundManager.Instance.sfxSource = sfxObj.AddComponent<AudioSource>();
        sfxObj.transform.SetParent(obj.transform);
        SoundManager.Instance.sfxSource.loop = false;
        SoundManager.Instance.bgmSource.volume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);

        //Load
        AudioClip[] bgmClips = Resources.LoadAll<AudioClip>("Sound/BGM");
        foreach (AudioClip clip in bgmClips)
        {
            try
            {
                BGMType type = (BGMType)Enum.Parse(typeof(BGMType), clip.name);
                SoundManager.Instance.bgmDict.Add(type, clip);
            }
            catch 
            {
                Debug.LogWarningFormat($"BGM enum 필요 : {clip.name}");
            }
        }

        AudioClip[] sfxClips = Resources.LoadAll<AudioClip>("Sound/SFX");
        foreach (AudioClip clip in sfxClips)
        {
            try
            {
                SFXType type = (SFXType)Enum.Parse(typeof(SFXType), clip.name);
                SoundManager.Instance.sfxDict.Add(type, clip);
            }
            catch
            {
                Debug.LogWarningFormat($"SFX enum 필요 : {clip.name}");
            }
        }

        SceneManager.sceneLoaded += SoundManager.Instance.OnSceneLoadComplete;
    }

    private void OnSceneLoadComplete(Scene scene, LoadSceneMode mode)
    {
        //@tk 이건 추후에 바뀔 수 있음. 동일 씬에서 맵 로드할 때 호출하게 하는 것이 좋을듯.
        if(scene.name == "Tutorial")
        {
            PlayBGM(BGMType.BGM_Tutorial, 1f);
        }
        else if (scene.name == "Title")
        {
            PlayBGM(BGMType.BGM_Title, 1f);
        }
        else if (scene.name == "Boss")
        {
            PlayBGM(BGMType.BGM_Boss, 1f);
        }
    }
    public void PlayBGM(BGMType bgmType, float fadeTime = 0f)
    {
        if (bgmSource.clip != null) 
        {
            if (bgmSource.clip.name == bgmType.ToString())
            {
                return;
            }

            if (fadeTime == 0)
            {
                bgmSource.clip = bgmDict[bgmType];
                bgmSource.Play();
            }
            else
            {
                StartCoroutine(FadeOutBGM(() =>
                {
                    bgmSource.clip = bgmDict[bgmType];
                    bgmSource.Play();
                    StartCoroutine(FadeInBGM(fadeTime));
                }, fadeTime));
            }
        }
        else
        {
            if (fadeTime == 0)
            {
                bgmSource.clip = bgmDict[bgmType];
                bgmSource.Play();
            }
            else
            {
                bgmSource.volume = 0;
                bgmSource.clip = bgmDict[bgmType];
                bgmSource.Play();
                StartCoroutine(FadeInBGM(fadeTime));
            }
        }

    }
    public void PlaySFX(SFXType sfxType)
    {
        sfxSource.PlayOneShot(sfxDict[sfxType]);
    }

    private IEnumerator FadeOutBGM(Action onComplete, float duration)
    {
        float startVolume = bgmSource.volume;
        float time = 0;

        while(time < duration)
        {
            bgmSource.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        bgmSource.volume = 0f;
        onComplete?.Invoke();
    }
    private IEnumerator FadeInBGM(float duration = 1.0f)
    {
        float targetVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        float time = 0;

        while (time < duration)
        {
            bgmSource.volume = Mathf.Lerp(0f, targetVolume, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        bgmSource.volume = targetVolume;
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}
