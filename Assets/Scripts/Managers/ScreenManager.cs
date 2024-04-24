using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;

    [SerializeField] BaseScreen[] all_screens;
    [SerializeField] SCREEN_TYPE init_screen;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        Switch_Screen(init_screen);
    }

    private BaseScreen Get_Screen(SCREEN_TYPE _screen)
    {
        return System.Array.Find(all_screens, x => x.screen_type == _screen);
    }
    public void Switch_Screen(SCREEN_TYPE _toshow, SCREEN_TYPE _tohide = SCREEN_TYPE.none)
    {
        Get_Screen(_tohide)?.Hide();
        Get_Screen(_toshow)?.Show();
    }
}