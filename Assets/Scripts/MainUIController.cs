using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
    private static MainUIController _instance;
    public static MainUIController Instance
    {
        get { return _instance; }
    }

    public int score = 0;
    public int length = 0;
    public Text msgText;
    public Text scoreText;
    public Text lengthText;
    public Image bgImage;
    public bool hasBorder = true;

    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("border",1) == 0)
        {
            hasBorder = false;
            foreach (Transform t in bgImage.gameObject.transform)
            {
                t.gameObject.GetComponent<Image>().enabled = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        switch (score/100)
        {
            case 0:
            case 1:
                break;
            case 2:
            case 3:
                bgImage.color = new Color(0.85f, 0.85f, 0.85f); 
                break;
            case 4:
            case 5:
                bgImage.color = new Color(0.65f, 0.65f, 0.65f);
                break;
            case 6:
            case 7:
                bgImage.color = new Color(0.45f, 0.45f, 0.45f);
                break;
            case 8:
            case 9:
                bgImage.color = new Color(0.25f, 0.25f, 0.25f);
                break;
            case 10:
            case 11:
                break;
            default:
                bgImage.color = new Color(0.05f, 0.05f, 0.05f);
                break;
        }
    }

    public void UpdateUI(int score = 50, int length = 50)
    {
        this.score += score;
        this.length += length;
        scoreText.text = "得分\n" + this.score;
        lengthText.text = "长度\n" + this.length;
    }

    public Image pauseImage;
    
    public Sprite[] pauseAndPlaySprite;
    public bool isPause = false;
    public void Pause()
    {
        isPause = !isPause;
        if (isPause)
        {
            Time.timeScale = 0;
            pauseImage.GetComponent<Image>().sprite = pauseAndPlaySprite[1];
        }
        else
        {
            Time.timeScale = 1;
            pauseImage.GetComponent<Image>().sprite = pauseAndPlaySprite[0];
        }
    }
    public void Home()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
