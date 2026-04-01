using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private int _score;
    [SerializeField] private TMP_Text _scoreText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        _score += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (_scoreText != null)
            _scoreText.text = $"Pontos: {_score}";
    }
}