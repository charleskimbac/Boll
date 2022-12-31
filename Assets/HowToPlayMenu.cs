using UnityEngine;

public class HowToPlayMenu : MonoBehaviour
{
    public GameObject howToPlayMenu;
    
    private void Start() {
        howToPlayMenu.SetActive(false);
    }

    public void showHowToPlay(){
        howToPlayMenu.SetActive(true);
    }

    public void hideHowToPlay(){
        howToPlayMenu.SetActive(false);
    }
}
