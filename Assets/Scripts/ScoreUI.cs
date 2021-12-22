using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public GameObject canvas;
    public GameObject textExample;

    // Start is called before the first frame update
    void Start()
    {
        //

        var list =DataManager.Instance.CurrentParameters.BestList;
        for (int i = 0; i < list.Count; i++)
        {
            var score = list[i];
            string text = score.PlayerName + ": " + score.ScorePoint;
            AddTextToCanvas(text, canvas,i);
        }
        
        //gameObject.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenuButtonClick()
    {
        SceneManager.LoadScene("menu");
    }

    public void FuckOfClick()
    {
        SceneManager.LoadScene("menu");
    }

    public void AddTextToCanvas(string textString, GameObject canvasGameObject,int index)
    {
        if (textExample == null)
        {
            return;
        }
        GameObject newText = new GameObject();

        newText.AddComponent<CanvasRenderer>();
        newText.AddComponent<RectTransform>();
        newText.AddComponent<Text>();

        var x = textExample.transform.position.x;
        var y = textExample.transform.position.y;
        var z = textExample.transform.position.z;
        newText.transform.position = new Vector3(x, y-(32* index), z);
        //newText.GetComponent<RectTransform>().anchoredPosition = textExample.GetComponent<RectTransform>().anchoredPosition;
        newText.GetComponent<RectTransform>().sizeDelta = new Vector2(200,30);

        newText.transform.SetParent(canvasGameObject.transform);
        newText.GetComponent<Text>().text = textString;
        newText.GetComponent<Text>().font = textExample.GetComponent<Text>().font;
        newText.GetComponent<Text>().fontSize = textExample.GetComponent<Text>().fontSize;
        newText.GetComponent<Text>().fontStyle = textExample.GetComponent<Text>().fontStyle;
        newText.GetComponent<Text>().alignment = textExample.GetComponent<Text>().alignment;
        
    }
}
