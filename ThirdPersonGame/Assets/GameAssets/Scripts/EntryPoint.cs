using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private CanvasGroup _menu;
    
    void Start()
    {
        Debug.Log("Start Initialised");
        _menu.alpha = 0;
        _menu.blocksRaycasts = false;
        
        
        Debug.Log("Game Initialized");
        _menu.alpha = 1;
        _menu.blocksRaycasts = true;
    }

   
}
