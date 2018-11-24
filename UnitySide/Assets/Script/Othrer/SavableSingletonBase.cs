using UnityEngine;
using System;
using System.IO;
using System.Security.Cryptography;

/// <summary>
/// ローカルストレージにファイルとして、シリアライズしたデータを保存するシングルトン
/// (iosの場合は、該当ファイルはiCloudバックアップ対象から除外)
/// 下記URLを参照
/// https://qiita.com/Ijoru/items/b68692fe4c8065041a86
/// </summary>
abstract public class SavableSingletonBase<T> where T : SavableSingletonBase<T>, new()
{
    protected static T instance;
    bool isLoaded = false;
    protected bool isSaving = false;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                var json = File.Exists(GetSavePath()) ? File.ReadAllText(GetSavePath()) : "";
                if(json.Length > 0)
                {
                    LoadFromJson(json);
                }
                else
                {
                    instance = new T();
                    instance.isLoaded = true;
                    instance.PostLoad();
                }
            }

            return instance;
        }
    }

    protected virtual void PostLoad()
    {

    }

    public void Save()
    {
        if(isLoaded)
        {
            isSaving = true;
            var path = GetSavePath();
            File.WriteAllText(path, JsonUtility.ToJson(this));
#if UNITY_IOS
            //iosでデータをiCloudにバックアップさせない設定
            UnityEngine.iOS.Device.SetNoBackupFlag(path);
#endif
            isSaving = false;
        }
    }

    public void Reset()
    {
        instance = null;
    }

    public void Clear()
    {
        if(File.Exists(GetSavePath()))
        {
            File.Delete(GetSavePath());
        }
        instance = null;
    }

    public static void LoadFromJson(string json)
    {
        try
        {
            instance = JsonUtility.FromJson<T>(json);
            instance.isLoaded = true;
            instance.PostLoad();
        }
        catch(Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    static string GetSavePath()
    {
        return string.Format("{0}/{1}", Application.persistentDataPath, GetSaveKey());
    }

    static string GetSaveKey()
    {
        var provider = new SHA1CryptoServiceProvider();
        var hash = provider.ComputeHash(System.Text.Encoding.ASCII.GetBytes(typeof(T).FullName));
        return BitConverter.ToString(hash);
    }

}
