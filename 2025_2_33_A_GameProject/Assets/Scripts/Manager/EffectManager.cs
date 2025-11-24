using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;

    [System.Serializable]

    public class EffectData
    {
        public string effectName;
        public GameObject effectPrefabs;
        public float duration = 2f;
    }

    [Header("이펙트 목록")]
    [SerializeField] private List<EffectData> effectDataList;
    private Dictionary<string, EffectData> effectDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitialDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitialDictionary()
    {
        effectDataList.Clear();
        foreach (var effect in effectDataList)
        {
            if (!effectDictionary.ContainsKey(effect.effectName))
            {
                effectDictionary.Add(effect.effectName, effect);
            }
            else
            {
                Debug.Log($"중복됨{effect.effectName}");
            }
        }
    }

    public GameObject PlayEffect(string effectName, Vector3 pos, Quaternion rot)
    {
        if (effectDictionary.TryGetValue(effectName, out EffectData data))
        {
            GameObject effect = Instantiate(data.effectPrefabs, pos, rot);
            Destroy(effect, data.duration);
            return effect;
        }
        else
        {
            Debug.LogWarning($"이펙트를 찾을수 없습니다{effectName}");
            return null;
        }
    }
    public GameObject PlayEffect(string effectName, Vector3 pos, Quaternion rot, float duration)
    {
        if (effectDictionary.TryGetValue(effectName, out EffectData data))
        {
            GameObject effect = Instantiate(data.effectPrefabs,pos,rot);
            Destroy(effect,duration);
            return effect;
        }
        else
        {
            Debug.LogWarning($"이펙트를 찾을수 없습니다{effectName}");
            return null;
        }
    }

    public GameObject PlayeEffect(string effectname, Vector3 pos)
    {
        return PlayEffect(effectname, pos, Quaternion.identity);
    }

    public GameObject PlayeEffect(string effectname, Vector3 pos,float duraction)
    {
        return PlayEffect(effectname, pos, Quaternion.identity,duraction);
    }


    public void PlayEffectWithDelay(string effecName,Vector3 pos, Quaternion rot, float delay,float duration)
    {
        StartCoroutine(PlayEffectWithDelayed(effecName,pos,rot,delay,duration));
    }

    IEnumerator PlayEffectWithDelayed(string effecName, Vector3 pos, Quaternion rot, float delay, float duration)
    {
        yield return new WaitForSeconds(delay);

        if(duration > 0)
        {
            PlayEffect(effecName, pos, rot, duration);
        }
        else
        {
            PlayEffect(effecName,pos, rot);
        }
    }

}
