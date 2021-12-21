using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUI : MonoBehaviour
{
    public InputField NameInput;
    public Text BestScoreText;
    public void StartButtonClick()
    {
        if (!string.IsNullOrEmpty(NameInput.text))
        {
            DataManager.Instance.SetName(NameInput.text);
            SceneManager.LoadScene("main");
        }
    }

    public void ExitButtonClick()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit(); // original code to quit Unity player
        #endif
    }

    void Start()
    {
        var parameter = DataManager.Instance.CurrentParameters;
        if (parameter != null)
        {
            parameter.BestList.Sort(parameter.ScoreSort);
            NameInput.text = parameter.PlayerName;
            if (parameter.BestList.Count > 0)
            {
                var first = parameter.BestList[0];
                BestScoreText.text = "Best Score : "+ first.PlayerName+ " : "+ first.ScorePoint;
            }
            else
            {
                BestScoreText.text = "Best Score : -------";
            }
                
        }
        
    }
}
