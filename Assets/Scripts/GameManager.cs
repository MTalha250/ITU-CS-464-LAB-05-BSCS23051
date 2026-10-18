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

    int score, redCoinScore;

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

    public void CollectRedCoin() => Debug.Log("Red coin collected");

    void UpdateUI() => scoreText.text = $"Score: {score} / {targetScore}";

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
