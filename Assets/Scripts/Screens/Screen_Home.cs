using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screen_Home : BaseScreen
{
    [Space]
    [SerializeField] Toggle[] toggle_modes;
    [SerializeField] Button btn_play;

    [Space]
    [SerializeField] TextMeshProUGUI txt_best_easy;
    [SerializeField] TextMeshProUGUI txt_best_medium;
    [SerializeField] TextMeshProUGUI txt_best_hard;

    private void Start()
    {
        btn_play.onClick.AddListener(OnClick_Play);
        toggle_modes[(int)DataManager.Instance.GameMode].isOn = true;
    }


    public override void Show()
    {
        txt_best_easy.text = string.Format("<size=40>Best Turns</size>\n{0}", DataManager.Instance.Get_Highscore(GAME_MODES.EASY));
        txt_best_medium.text = string.Format("<size=40>Best Turns</size>\n{0}", DataManager.Instance.Get_Highscore(GAME_MODES.MEDIUM));
        txt_best_hard.text = string.Format("<size=40>Best Turns</size>\n{0}", DataManager.Instance.Get_Highscore(GAME_MODES.HARD));

        base.Show();
    }



    private void OnClick_Play()
    {
        ScreenManager.instance.Switch_Screen(SCREEN_TYPE.GAME, this.screen_type);
    }
    public void OnChangeToggle_Modes(bool value)
    {
        if (!value)
            return;

        DataManager.Instance.GameMode = (GAME_MODES)System.Array.FindIndex(toggle_modes, x => x.isOn);
    }
}
