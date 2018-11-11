using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// [Resources/Sound]ファルダにあるすべての音源アセットを管理します
/// </summary>
public class SoundManager : MonoBehaviour
{
    #region Singleton
    /// <summary>
    /// Singleton instance.
    /// </summary>
    private static SoundManager mInstance;

    /// <summary>
    /// [instance] property.
    /// </summary>
    public static SoundManager Instance
    {
        get
        {
            if(mInstance == null)
            {
                GameObject obj = new GameObject("SoundManagerObj");
                DontDestroyOnLoad(obj);
                obj.AddComponent<AudioSource>();
                mInstance = obj.AddComponent<SoundManager>();
                mInstance.Init();
            }

            return mInstance;
        }
    }
    #endregion

    private Dictionary<string, AudioClip> clipDictionary = new Dictionary<string, AudioClip>();
    private Dictionary<string, GameObject> seObjectDictionary = new Dictionary<string, GameObject>();
    private bool isInit = false;
    
    /// <summary>
    /// Play BGM
    /// </summary>
    /// <param name="bgmName"> BGM Name.(Asset Name)</param>
    /// <param name="loop"> True = Loop BGM</param>
    /// <param name="volume"> BGM Volume</param>
    public void PlayBGM(string bgmName, bool loop, float volume)
    {
        if(!clipDictionary.ContainsKey(bgmName))
        {
            return;
        }

        AudioSource source = gameObject.GetComponent<AudioSource>();
        if(source.isPlaying)
        {
            StopBGM();
        }

        source.clip = clipDictionary[bgmName];
        source.loop = loop;
        source.volume = volume;
        source.Play();
    }

    /// <summary>
    /// Play SE
    /// </summary>
    /// <param name="seName"> SE Name.(Asset Name)</param>
    /// <param name="loop"> True = Loop SE.</param>
    /// <param name="pos"> SE sound source Point.</param>
    /// <param name="volume"> SE volume.</param>
    public void PlaySE(string seName, bool loop, Vector3 pos, float volume = 1.0f)
    {
        if(!clipDictionary.ContainsKey(seName))
        {
            return;
        }

        if(!seObjectDictionary.ContainsKey(seName))
        {
            seObjectDictionary[seName] = new GameObject();
            DontDestroyOnLoad(seObjectDictionary[seName]);
            seObjectDictionary[seName].AddComponent<AudioSource>();
        }

        AudioSource source = seObjectDictionary[seName].GetComponent<AudioSource>();
        source.gameObject.transform.position = pos;
        if(!source.loop)
        {
            source.loop = loop;
            source.clip = clipDictionary[seName];
            source.volume = volume;
            source.Play();
        }
    }

    /// <summary>
    /// Stop BGM
    /// </summary>
    public void StopBGM()
    {
        AudioSource source = gameObject.GetComponent<AudioSource>();
        if(source.isPlaying)
        {
            source.Stop();
        }
    }

    /// <summary>
    /// Stop SE
    /// </summary>
    /// <param name="seName"> SE Name.</param>
    public void StopSE(string seName)
    {
        if(seObjectDictionary.ContainsKey(seName))
        {
            AudioSource source = seObjectDictionary[seName].GetComponent<AudioSource>();
            source.loop = false;
            source.Stop();
        }
    }

    /// <summary>
    /// Initialize all assets in [Resources/Sounds] to SoundClip.
    /// </summary>
    public void Init()
    {
        if(isInit)
        {
            return;
        }

        AudioClip[] obj = Resources.LoadAll<AudioClip>("Sounds/");
        foreach(var clip in obj)
        {
            clipDictionary.Add(clip.name, clip);
        }

        isInit = true;
    }

}
