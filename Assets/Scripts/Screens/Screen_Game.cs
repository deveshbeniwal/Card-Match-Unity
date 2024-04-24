using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screen_Game : BaseScreen
{
    [Space]
    [SerializeField] TextMeshProUGUI txt_matches;
    [SerializeField] TextMeshProUGUI txt_turns;
    [SerializeField] Button btn_home;

    int current_matches = 0;
    int current_turns = 0;


    private void Start()
    {
        btn_home.onClick.AddListener(OnClick_Home);
    }


    public override void Show()
    {
        Update_Matches(0);
        Update_Turns(0);

        base.Show();

        GameManager.instance.Initialize();
    }


    private void Update_Matches(int _toadd)
    {
        current_matches += _toadd;
        txt_matches.text = string.Format("{0}", current_matches);
    }
    private void Update_Turns(int _toadd)
    {
        current_turns += _toadd;
        txt_turns.text = string.Format("{0}", current_turns);
    }


    private void OnClick_Home()
    {
        ScreenManager.instance.Switch_Screen(SCREEN_TYPE.HOME, this.screen_type);
    }
}
