using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TruthDareItem
{
    public string id;
    public string level;
    public string summary;
    public string time;
    public string turns;
    public string type; // "Truth" or "Dare"
}

[Serializable]
public class TruthDareWrapper
{
    public List<TruthDareItem> items;
}

public class TruthDareLoader : MonoBehaviour
{
    public List<TruthDareItem> Items;

    void Awake()
    {
        LoadData();
    }

    void LoadData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("truth_or_dare");
        if (jsonFile == null)
        {
            Debug.LogError("truth_or_dare.json not found in Resources folder!");
            return;
        }

        string wrapped = "{\"items\":" + jsonFile.text + "}";
        TruthDareWrapper wrapper = JsonUtility.FromJson<TruthDareWrapper>(wrapped);
        Items = wrapper.items;

        Debug.Log($"Loaded {Items.Count} truth/dare entries.");
    }

    public List<TruthDareItem> GetByExactLevel(int level, string type = null)
    {
        return Items.FindAll(item =>
        {
            int.TryParse(item.level, out int lvl);
            return lvl == level && (type == null || item.type == type);
        });
    }

    public TruthDareItem GetRandomExact(int level, string type = null)
    {
        var pool = GetByExactLevel(level, type);
        if (pool.Count == 0) return null;
        return pool[UnityEngine.Random.Range(0, pool.Count)];
    }

    public List<TruthDareItem> GetByLevel(int maxLevel, string type = null)
    {
        return Items.FindAll(item =>
        {
            int.TryParse(item.level, out int lvl);
            return lvl <= maxLevel && (type == null || item.type == type);
        });
    }

    public TruthDareItem GetRandom(int maxLevel, string type = null)
    {
        var pool = GetByLevel(maxLevel, type);
        if (pool.Count == 0) return null;
        return pool[UnityEngine.Random.Range(0, pool.Count)];
    }
}