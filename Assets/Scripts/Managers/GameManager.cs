using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public event Action Event_ShowCards;
    public event Action Event_HideCards;
    public event Action Event_StartGame;


    [Header("Grid Settings")]
    [SerializeField] Grid grid;
    [SerializeField] Card grid_card_prefab;
    [SerializeField] Transform grid_card_parent;

    [Header("Camera Settings")]
    [SerializeField] CamSizeHandler cam_handler;

    [Header("Cards Settings")]
    [SerializeField] Sprite[] all_card_pictures;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public async void Initialize()
    {
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
    private async Task Generate_Cards(Vector2Int _grid_size)
    {
        float centre_x = (grid.GetCellCenterWorld(new Vector3Int(0, 0)).x + grid.GetCellCenterWorld(new Vector3Int(_grid_size.y - 1, 0)).x) * 0.5f;
        float centre_y = (grid.GetCellCenterWorld(new Vector3Int(0, 0)).y + grid.GetCellCenterWorld(new Vector3Int(0, _grid_size.x - 1)).y) * 0.5f;

        cam_handler.Initialize(new Vector2(centre_x, centre_y));

        int _selected_card_index = UnityEngine.Random.Range(0, all_card_pictures.Length);
        int _same_card_counts = 0;

        for (int i = 0; i < _grid_size.y; i++)
        {
            for (int j = 0; j < _grid_size.x; j++)
            {
                if(_same_card_counts >= 2)
                {
                    _selected_card_index = UnityEngine.Random.Range(0, all_card_pictures.Length);
                    _same_card_counts = 0;
                }

                Card card = Instantiate(grid_card_prefab, grid_card_parent);
                card.Initialize(grid.GetCellCenterWorld(new Vector3Int(i, j)), all_card_pictures[_selected_card_index], _selected_card_index);

                _same_card_counts++;
                await Task.Delay(100);
            }
        }
    }
    private async Task Splash_Cards()
    {
        Event_ShowCards?.Invoke();
        await Task.Delay(2000);
        Event_HideCards?.Invoke();
        await Task.Delay(1000);
        Event_StartGame?.Invoke();
    }
}
