using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // 1

    public Text sheepSavedText; // 2
    public Text sheepDroppedText; // 3
    public GameObject gameOverWindow; // 4
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake(){
        Instance = this;
    }

    // Update is called once per frame
    public void UpdateSheepSaved(){
        sheepSavedText.text = GameStateManager.Instance.sheepSaved.ToString();
    }

    public void UpdateSheepDropped(){
            sheepDroppedText.text = GameStateManager.Instance.sheepDropped.ToString();
    }
    public void ShowGameOverWindow(){
        gameOverWindow.SetActive(true);
    }

}
