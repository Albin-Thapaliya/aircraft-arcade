using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text playerTurnText;
    public Button endTurnButton;

    void Start()
    {
        endTurnButton.onClick.AddListener(GameManager.Instance.EndTurn);
    }

    public void SetPlayerTurnUI(bool isPlayerTurn)
    {
        playerTurnText.text = isPlayerTurn ? "Your Turn" : "Enemy Turn";
    }
}