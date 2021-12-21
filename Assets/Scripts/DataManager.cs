using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public SaveData CurrentParameters=null;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(CurrentParameters);

        File.WriteAllText(Application.persistentDataPath + "/params.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/params.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            if (data != null)
            {
                CurrentParameters = data;
            }
        }

        if (CurrentParameters == null)
        {
            CurrentParameters = new SaveData();
        }

        if(CurrentParameters.BestList==null){
            CurrentParameters.BestList = new List<Score>();
        }
    }

    public void SetName(string name)
    {
        CurrentParameters.PlayerName = name;
        SaveData();
    }

    public void SetScore(int score)
    {
        if (CurrentParameters.BestList.Count > 10)
        {
            CurrentParameters.BestList.Sort(CurrentParameters.ScoreSort);
            for (int i = 0; i < CurrentParameters.BestList.Count; i++)
            {
                var nextScore = CurrentParameters.BestList[i];
                if(score > nextScore.ScorePoint)
                {
                    CurrentParameters.BestList.RemoveAt(CurrentParameters.BestList.Count - 1);
                    CurrentParameters.BestList.Add(new Score
                    {
                        PlayerName = CurrentParameters.PlayerName,
                        ScorePoint = score
                    });
                    SaveData();
                    break;
                }
            }
        }
        else
        {
            CurrentParameters.BestList.Add(new Score
            {
                PlayerName= CurrentParameters.PlayerName,
                ScorePoint= score
            });
            SaveData();
        }
    }
}

[System.Serializable]
public class SaveData
{
    public string PlayerName;
    public List<Score> BestList;

    public int ScoreSort(Score a, Score b)
    {
        return a.ScorePoint.CompareTo(b.ScorePoint)*-1;
    }
}

[System.Serializable]
public class Score
{
    public string PlayerName;
    public int ScorePoint;
}
