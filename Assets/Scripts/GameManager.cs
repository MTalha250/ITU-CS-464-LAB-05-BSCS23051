using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] int targetScore = 10;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject winPanel;

    [Header("Red coin")]
    [SerializeField] GameObject redCoinPrefab;
    [SerializeField] Transform player;
    [SerializeField] float area = 7f;

    [Header("Bonus coins")]
    [SerializeField] GameObject blueCoinPrefab;
    [SerializeField] int bonusCount = 50;
    [SerializeField] float bonusRadius = 5f;
    [SerializeField] float arenaEdge = 9f;         // keep the ring inside the walls
    [SerializeField] TMP_Text bonusText;           // hidden until the red coin is collected

    int score, redCoinScore, bonusCoins;

    void Awake() => Instance = this;

    void Start()
    {
        redCoinScore = Random.Range(1, targetScore);   // 1 to 9, so it comes before the win at 10
        Debug.Log($"Red coin will spawn at score {redCoinScore}");
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
        if (score == redCoinScore) SpawnRedCoin();     // score only goes up by 1, so this happens once
        if (score >= targetScore) Win();
    }

    void SpawnRedCoin()
    {
        Vector3 pos;
        do pos = new Vector3(Random.Range(-area, area), 0.5f, Random.Range(-area, area));
        while (Vector3.Distance(pos, player.position) < 2f);
        Instantiate(redCoinPrefab, pos, Quaternion.identity);
    }

    public void CollectRedCoin()
    {
        // a ring of blue coins around the player
        for (int i = 0; i < bonusCount; i++)
        {
            float angle = i * Mathf.PI * 2f / bonusCount;
            Vector3 pos = player.position + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * bonusRadius;
            pos.x = Mathf.Clamp(pos.x, -arenaEdge, arenaEdge);
            pos.z = Mathf.Clamp(pos.z, -arenaEdge, arenaEdge);
            pos.y = 0.5f;
            Instantiate(blueCoinPrefab, pos, Quaternion.identity);
        }
        bonusText.gameObject.SetActive(true);
        UpdateUI();
    }

    public void CollectBonusCoin()
    {
        bonusCoins++;
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = $"Score: {score} / {targetScore}";
        bonusText.text = $"BonusCoins: {bonusCoins}/{bonusCount}";
    }

    void Win()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
