using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public event System.Action OnScoreChange;

    int score = 0;
    public int Score {
        get { return score; }
        private set {
            score = value;
            Debug.Log(score);
            OnScoreChange?.Invoke();
        }
    }

    public void ChangeScore(int score)
    {
        Score = score;
    }

    public void AddPoint()
    {
        Score += 1;
    }
}
