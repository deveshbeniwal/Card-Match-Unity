using UnityEngine;

public class DataManager
{
    private static DataManager instance;
    public static DataManager Instance 
    {
        get 
        {
            if (instance == null)
                instance = new DataManager();

            return instance;
        }
    }


    const string KEY_GAMEMODE = "GM";
    const string KEY_HIGHSCORE = "HS";


    public GAME_MODES GameMode
    {
        get { return (GAME_MODES)PlayerPrefs.GetInt(KEY_GAMEMODE, 0); }
        set { PlayerPrefs.SetInt(KEY_GAMEMODE, (int)value); }
    }

    public int Get_Highscore(GAME_MODES _mode)
    {
        return PlayerPrefs.GetInt(string.Format("{0}_{1}", KEY_HIGHSCORE, (int)_mode), 0);
    }
    public void Set_Highscore(GAME_MODES _mode, int value)
    {
        PlayerPrefs.SetInt(string.Format("{0}_{1}", KEY_HIGHSCORE, (int)_mode), value);
    }
}


public enum SCREEN_TYPE
{
    none,
    HOME,
    GAME
}
public enum GAME_MODES
{
    EASY,
    MEDIUM,
    HARD
}