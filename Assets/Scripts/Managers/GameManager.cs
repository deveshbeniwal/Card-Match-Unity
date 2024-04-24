using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Grid Settings")]
    [SerializeField] Grid grid;
    [SerializeField] Card grid_card_prefab;
    [SerializeField] Transform grid_card_parent;

    [Header("Camera Settings")]
    [SerializeField] CamSizeHandler cam_handler;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void Initialize()
    {
        Vector2Int grid_size = DataManager.Instance.GameMode switch
        {
            GAME_MODES.EASY => new Vector2Int(2, 3),
            GAME_MODES.MEDIUM => new Vector2Int(3, 4),
            GAME_MODES.HARD => new Vector2Int(5, 6),
            _ => new Vector2Int(2, 3)
        };

        Generate_Cards(grid_size);
    }
    private async void Generate_Cards(Vector2Int _grid_size)
    {
        float centre_x = (grid.GetCellCenterWorld(new Vector3Int(0, 0)).x + grid.GetCellCenterWorld(new Vector3Int(_grid_size.y - 1, 0)).x) * 0.5f;
        float centre_y = (grid.GetCellCenterWorld(new Vector3Int(0, 0)).y + grid.GetCellCenterWorld(new Vector3Int(0, _grid_size.x - 1)).y) * 0.5f;

        cam_handler.Initialize(new Vector2(centre_x, centre_y));

        for (int i = 0; i < _grid_size.y; i++)
        {
            for (int j = 0; j < _grid_size.x; j++)
            {
                Card card = Instantiate(grid_card_prefab, grid_card_parent);
                card.transform.position = grid.GetCellCenterWorld(new Vector3Int(i, j));

                await Task.Delay(100);
            }
        }
    }
}
