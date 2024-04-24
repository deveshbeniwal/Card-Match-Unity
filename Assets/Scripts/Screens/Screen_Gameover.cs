using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screen_Gameover : BaseScreen
{
    [Space]
    [SerializeField] TextMeshProUGUI txt_matches;
    [SerializeField] TextMeshProUGUI txt_turns;
    [SerializeField] TextMeshProUGUI txt_mode;
    [SerializeField] TextMeshProUGUI txt_best;

    [Space]
    [SerializeField] Button btn_home;
    [SerializeField] Button btn_replay;

    private void Start()
    {
        btn_home.onClick.AddListener(OnClick_Home);
        btn_replay.onClick.AddListener(OnClick_Replay);
    }


    public override void Show()
    {
        txt_matches.text = GameManager.instance.Current_Matches.ToString();
        txt_turns.text = GameManager.instance.Current_Turns.ToString();
        txt_mode.text = string.Format("<size=40>Mode</size>\n{0}", DataManager.Instance.GameMode.ToString());
        txt_best.text = string.Format("<size=40>Best Turns</size>\n{0}", DataManager.Instance.Get_Highscore(DataManager.Instance.GameMode));

        base.Show();
    }



    private void OnClick_Home()
    {
        ScreenManager.instance.Switch_Screen(SCREEN_TYPE.HOME, this.screen_type);
    }
    private void OnClick_Replay()
    {
        ScreenManager.instance.Switch_Screen(SCREEN_TYPE.GAME, this.screen_type);
    }
}
