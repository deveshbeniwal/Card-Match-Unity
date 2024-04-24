using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public event Action Event_ShowCards;
    public event Action Event_HideCards;
    public event Action<int> Event_OnMatchChanged;
    public event Action<int> Event_OnTurnChanged;
    public event Action Event_OnGameFinished;


    [Header("Grid Settings")]
    [SerializeField] Grid grid;
    [SerializeField] Card grid_card_prefab;
    [SerializeField] Transform grid_card_parent;

    [Header("Camera Settings")]
    [SerializeField] CamSizeHandler cam_handler;

    [Header("Cards Settings")]
    [SerializeField] Sprite[] all_card_pictures;
    [SerializeField] List<Card> all_card_generated;

    Card primary_card = null;
    int current_matches;
    int current_turns;


    public int Current_Matches { get { return current_matches; } }
    public int Current_Turns { get { return current_turns; } }



    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public async void Initialize()
    {
        current_matches = 0;
        current_turns = 0;

        Vector2Int grid_size = DataManager.Instance.GameMode switch
        {
            GAME_MODES.EASY => new Vector2Int(2, 3),
            GAME_MODES.MEDIUM => new Vector2Int(3, 4),
            GAME_MODES.HARD => new Vector2Int(5, 6),
            _ => new Vector2Int(2, 3)
        };

        await Generate_Cards(grid_size);
        await Splash_Cards();
    }
    public void Reset_Game()
    {
        foreach (var item in all_card_generated)
            Destroy(item.gameObject);

        all_card_generated.Clear();
    }
    private async Task Generate_Cards(Vector2Int _grid_size)
    {
        float centre_x = (grid.GetCellCenterWorld(new Vector3Int(0, 0)).x + grid.GetCellCenterWorld(new Vector3Int(_grid_size.y - 1, 0)).x) * 0.5f;
        float centre_y = (grid.GetCellCenterWorld(new Vector3Int(0, 0)).y + grid.GetCellCenterWorld(new Vector3Int(0, _grid_size.x - 1)).y) * 0.5f;

        cam_handler.Initialize(new Vector2(centre_x, centre_y));

        int current_random_index = 0;
        List<int> random_data = new List<int>();
        for (int i = 0; i < (_grid_size.x * _grid_size.y) / 2; i++)
        {
            int index = UnityEngine.Random.Range(0, all_card_pictures.Length);
            random_data.Add(index);
            random_data.Add(index);
        }
        random_data.Shuffle();

        all_card_generated = new List<Card>();
        for (int i = 0; i < _grid_size.y; i++)
        {
            for (int j = 0; j < _grid_size.x; j++)
            {
                Card card = Instantiate(grid_card_prefab, grid_card_parent);
                card.Initialize(grid.GetCellCenterWorld(new Vector3Int(i, j)), all_card_pictures[random_data[current_random_index]], random_data[current_random_index]);

                all_card_generated.Add(card);

                current_random_index++;
                await Task.Delay(100);
            }
        }
    }
    private async Task Splash_Cards()
    {
        Event_ShowCards?.Invoke();
        await Task.Delay(2000);
        Event_HideCards?.Invoke();
    }


    public void OnCard_Select(Card _card)
    {
        if (primary_card == null)
        {
            primary_card = _card;
            return;
        }

        if (_card.gameObject.name == primary_card.gameObject.name)
        {
            //matching cards, so delete them and add progress
            primary_card.Destroy_Card();
            _card.Destroy_Card();

            all_card_generated.Remove(primary_card);
            all_card_generated.Remove(_card);

            current_matches++;
            Event_OnMatchChanged?.Invoke(current_matches);
        }
        else
        {
            //non-matching cards, flip them again
            primary_card.Flip_Card_Hide();
            _card.Flip_Card_Hide();
        }

        primary_card = null;

        current_turns++;
        Event_OnTurnChanged?.Invoke(current_turns);

        if (all_card_generated.Count <= 0)
            Game_finished();
    }
    private void Game_finished()
    {
        if (current_turns < DataManager.Instance.Get_Highscore(DataManager.Instance.GameMode))
            DataManager.Instance.Set_Highscore(DataManager.Instance.GameMode, current_turns);

        Timer.Schedule(this, 1, () => 
        {
            Reset_Game();
            Event_OnGameFinished?.Invoke();
        });
    }
}
