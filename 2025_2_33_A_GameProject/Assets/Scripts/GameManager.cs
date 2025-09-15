using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("게임상태")]
    public int playerScore = 0;
    public int itemCollected = 0;

    [Header("UI 참조")]
    public Text scoreTxt;
    public Text itemCountTxt;
    public Text statusTxt;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void CollectItem()
    {
        itemCollected++;
        Debug.Log($"아이템 수집수 : {itemCollected}");
    }

    void UpdateUI()
    {
        if (scoreTxt != null)
        {
            scoreTxt.text = "점수 : " + playerScore;
        }
        if (itemCountTxt != null)
        {
            itemCountTxt.text = "아이템 : " + itemCollected;
        }

    }

}
