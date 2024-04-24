using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screen_Game : BaseScreen
{
    [Space]
    [SerializeField] TextMeshProUGUI txt_matches;
    [SerializeField] TextMeshProUGUI txt_turns;
    [SerializeField] Button btn_home;


    private void Start()
    {
        btn_home.onClick.AddListener(OnClick_Home);
    }


    public override void Show()
    {
        GameManager.instance.Event_OnMatchChanged += Update_Matches;
        GameManager.instance.Event_OnTurnChanged += Update_Turns;
        GameManager.instance.Event_OnGameFinished += Instance_OnGameFinished;

        Update_Matches(0);
        Update_Turns(0);

        base.Show();

        GameManager.instance.Initialize();
    }
    public override void Hide()
    {
        GameManager.instance.Event_OnMatchChanged -= Update_Matches;
        GameManager.instance.Event_OnTurnChanged -= Update_Turns;
        GameManager.instance.Event_OnGameFinished -= Instance_OnGameFinished;

        base.Hide();
    }


    private void Update_Matches(int _value)
    {
        txt_matches.text = string.Format("{0}", _value);
    }
    private void Update_Turns(int _value)
    {
        txt_turns.text = string.Format("{0}", _value);
    }
    private void Instance_OnGameFinished()
    {
        ScreenManager.instance.Switch_Screen(SCREEN_TYPE.GAMEOVER, this.screen_type);
    }


    private void OnClick_Home()
    {
        GameManager.instance.Reset_Game();
        ScreenManager.instance.Switch_Screen(SCREEN_TYPE.HOME, this.screen_type);
    }
}
