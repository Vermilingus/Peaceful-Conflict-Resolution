using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadButton : MonoBehaviour {
    public string LevelToLoad;

    private void Start()
    {
        Button thisButton = transform.GetComponent<Button>();
        //thisButton.onClick.AddListener(ButtonPress);
    }

    public void ButtonPress()
    {
        MechGameManager.instance.ChangeLevel(LevelToLoad);
    }
}
