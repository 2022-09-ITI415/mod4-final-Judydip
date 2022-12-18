using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIReturnToMainMenu : MonoBehaviour
{

    public GameObject returnMenu;
    //public GameObject fpsController;


    public void Awake()
    {
       
        //returnMenu.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    // Update is called once per frame
    public void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Mouse0)))
        {
            returnMenu.SetActive(false);
            //fpsController.SetActive(!fpsController.activeSelf);

        }
        if ((Input.GetKeyDown(KeyCode.Escape)))
        {
            ReturnToMainMenu();
        }
    }
}
