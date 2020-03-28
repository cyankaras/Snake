using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodMaker : MonoBehaviour
{
    private static FoodMaker _instance;
    public static FoodMaker Instance
    {
        get
        {
            return _instance;
        }
    }
    public int xLimit = 21;
    public int yLimit = 11;
    public int xOffset = 7;
    public GameObject footPrefab;
    public GameObject rewardPrefab;
    public Sprite[] foodSprites;
    public Transform foodHolder;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        foodHolder = GameObject.Find("FoodHolder").transform;
        MakeFood(false);
    }
    public void MakeFood(bool isReward)
    {
        int index = Random.Range(0, foodSprites.Length);
        GameObject food = Instantiate(footPrefab);
        food.GetComponent<Image>().sprite = foodSprites[index];
        food.transform.SetParent(foodHolder, false);
        int x = Random.Range(-xLimit + xOffset, xLimit);
        int y = Random.Range(-yLimit, yLimit);
        food.transform.localPosition = new Vector3(x * 30, y * 30);
        if (isReward)
        {
            GameObject reward = Instantiate(rewardPrefab);
            reward.transform.SetParent(foodHolder, false);
            x = Random.Range(-xLimit + xOffset, xLimit);
            y = Random.Range(-yLimit, yLimit);
            reward.transform.localPosition = new Vector3(x * 30, y * 30);
        }
    }
}
