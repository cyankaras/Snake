using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUIcontroller : MonoBehaviour
{
    public Text text;
    public Toggle blue;
    public Toggle yellow;
    public Toggle border;
    public Toggle withOutBorder;

    private void Awake()
    {
        text.text = "Last length: " + PlayerPrefs.GetInt("lastLength", 0) + "\nLast score: " + PlayerPrefs.GetInt("lastScore", 0)+ "\nBest length: " + PlayerPrefs.GetInt("bestLength", 0) + "\nBest score: " + PlayerPrefs.GetInt("bestScore", 0);
    }
    private void Start()
    {
        if (PlayerPrefs.GetString("SnakeHead", "SnakeHead01") == "SnakeHead01")
        {
            blue.isOn = true;
            SelectBlue(blue.isOn);
        }
        else
        {
            yellow.isOn = true;
            SelectYellow(yellow.isOn);
        }
        if (PlayerPrefs.GetInt("border",1) == 1)
        {
            border.isOn = true;
            SelectBorder(border.isOn);
        }
        else
        {
            withOutBorder.isOn = true;
            SelectWithOutBorder(withOutBorder.isOn);
        }
    }
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void SelectBlue(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetString("SnakeHead", "SnakeHead01");
            PlayerPrefs.SetString("SnakeBody01", "SnakeBody0101");
            PlayerPrefs.SetString("SnakeBody02", "SnakeBody0102");
        }
    }
    public void SelectYellow(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetString("SnakeHead", "SnakeHead02");
            PlayerPrefs.SetString("SnakeBody01", "SnakeBody0201");
            PlayerPrefs.SetString("SnakeBody02", "SnakeBody0202");
        }
    }
    public void SelectBorder(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("border", 1);
        }
    }
    public void SelectWithOutBorder(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("border", 0);
        }
    }
}
