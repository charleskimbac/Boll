using UnityEngine;

public class CreditMenu : MonoBehaviour
{
    public GameObject creditMenu;
    
    private void Start() {
        creditMenu.SetActive(false);
    }

    public void showCredits(){
        creditMenu.SetActive(true);
    }

    public void hideCredits(){
        creditMenu.SetActive(false);
    }
}
