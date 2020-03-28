using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SnakeHead : MonoBehaviour
{
    public List<Transform> bodyList = new List<Transform>();
    public int step = 30;
    public float velocity = 0.35f;
    private int x;
    private int y;
    private Vector3 SHPosition;
    public GameObject bodyPrefab;
    public Sprite[] bodySprite = new Sprite[2];
    public Transform canvas;
    public bool isDie;
    public GameObject dieEffect;
    public AudioClip eat;
    public AudioClip die;

    public void Awake()
    {
        //通过Resources.Load(string path)方法加载资源，path的书写不需要加”Resources/“，以及文件扩展名。
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("SnakeHead", "SnakeHead02"));
        bodySprite[0] = Resources.Load<Sprite>(PlayerPrefs.GetString("SnakeBody01", "SnakeBody0101"));
        bodySprite[1] = Resources.Load<Sprite>(PlayerPrefs.GetString("SnakeBody02", "SnakeBody0102"));
        canvas = GameObject.Find("Canvas").transform;
        

    }

    private void Start()
    {
        InvokeRepeating("Move", 0, velocity);
        x = 0;
        y = step;
    }

    private void Update()
    {
        ////位置差，方向
        //Vector3 dir = target.position - transform.position;      
        ////返回值为正，在前方
        //Vector3.Dot(transform.forward, dir);

        if (!MainUIController.Instance.isPause || isDie)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                CancelInvoke();
                InvokeRepeating("Move", 0, velocity);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CancelInvoke();
                InvokeRepeating("Move", 0, velocity - 0.2f);
            }
            if (Input.GetKey(KeyCode.UpArrow) && y != -step)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                x = 0;
                y = step;
            }
            if (Input.GetKey(KeyCode.DownArrow) && y != step)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
                x = 0;
                y = -step;
            }
            if (Input.GetKey(KeyCode.RightArrow) && x != -step)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
                x = step;
                y = 0;
            }
            if (Input.GetKey(KeyCode.LeftArrow) && x != step)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
                x = -step;
                y = 0;
            }
        }
        
    }

    private void Move()
    {
        SHPosition = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(SHPosition.x + x, SHPosition.y + y, SHPosition.z);
        if (bodyList.Count > 0)
        {
            for (int i = bodyList.Count-2; i >=0 ; i--)
            {
                bodyList[i + 1].localPosition = bodyList[i].localPosition;
            }
            bodyList[0].localPosition = SHPosition;

            //移动方式一：蛇尾移动到身体最前一个，身体其余位置保持不变。(不适合奇偶花色不同的)
            //bodyList.Last().localPosition = SHPosition;
            //bodyList.Insert(0, bodyList.Last());
            //bodyList.RemoveAt(bodyList.Count - 1);
        }
    }
    void Die()
    {
        AudioSource.PlayClipAtPoint(die, Vector3.zero);
        CancelInvoke();
        isDie = true;
        Instantiate(dieEffect);
        PlayerPrefs.SetInt("lastLength", MainUIController.Instance.length);
        PlayerPrefs.SetInt("lastScore", MainUIController.Instance.score);
        if (PlayerPrefs.GetInt("bestScore", 0) <MainUIController.Instance.score)
        {
            PlayerPrefs.SetInt("bestLength", MainUIController.Instance.length);
            PlayerPrefs.SetInt("bestScore", MainUIController.Instance.score);

        }
        StartCoroutine(GameOver(1.5f));
    }
    IEnumerator GameOver(float t)
    {
        yield return new WaitForSeconds(t);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    void BodyGrow()
    {
        AudioSource.PlayClipAtPoint(eat, Vector3.zero);
        int index = (bodyList.Count % 2 == 0) ? 0 : 1;
        GameObject body = Instantiate(bodyPrefab, new Vector3(2000f, 2000f), Quaternion.identity);
        body.GetComponent<Image>().sprite = bodySprite[index];
        body.transform.SetParent(canvas, false);
        bodyList.Add(body.transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Destroy(collision.gameObject);
            MainUIController.Instance.UpdateUI();
            FoodMaker.Instance.MakeFood(Random.Range(0, 100) < 60 ? true : false);
            BodyGrow();
        }
        else if (collision.CompareTag("Reward"))
        {
            Destroy(collision.gameObject);
            MainUIController.Instance.UpdateUI(Random.Range(5, 20) * 10);
            BodyGrow();
        }
        else if (collision.CompareTag("Body"))
        {
            Die();
        }
        else
        {
            if (MainUIController.Instance.hasBorder)
            {
                Die();
            }
            switch (collision.name)
            {
                case "Up":
                    transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y + 30, transform.localPosition.z);
                        break;
                case "Down":
                    transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y - 30, transform.localPosition.z);
                    break;
                case "Right":
                    transform.localPosition = new Vector3(-transform.localPosition.x + 120, transform.localPosition.y, transform.localPosition.z);
                    break;
                case "Left":
                    transform.localPosition = new Vector3(-transform.localPosition.x + 60, transform.localPosition.y, transform.localPosition.z);
                    break;
                default:
                    break;
            }
        }
    }
}
