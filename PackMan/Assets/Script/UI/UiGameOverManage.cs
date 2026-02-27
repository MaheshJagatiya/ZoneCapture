using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//Display gameover and level compete image with event

public class UiGameOverManage : MonoBehaviour
{
    [SerializeField] RectTransform gameOverText;
    [SerializeField] RectTransform levelCompleteText;

    [SerializeField] float targetY = 0f;

    void OnEnable()
    {
        GameEvents.OnGameOver += ShowGameOver;
        GameEvents.OnLevelComplete += ShowLevelComplete;
    }

    void OnDisable()
    {
        gameOverText.DOKill();
        levelCompleteText.DOKill();
        GameEvents.OnGameOver -= ShowGameOver;
        GameEvents.OnLevelComplete -= ShowLevelComplete;
    }

    public void ShowGameOver()
    {
        gameOverText.DOKill();
        AnimateText(gameOverText);
        Invoke(nameof(RestartGame), 2.5f);
    }

    public void ShowLevelComplete()
    {
        levelCompleteText.DOKill();
        AnimateText(levelCompleteText);
        Invoke(nameof(RestartGame), 3.0f);
    }

    void AnimateText(RectTransform rect)
    {
        if (rect == null) return;

        rect.DOKill();

        rect.gameObject.SetActive(true);

        rect.anchoredPosition = new Vector2(0, 800); 

        rect.DOAnchorPosY(targetY, 1.2f)
            .SetEase(Ease.OutBounce);
    }
    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
