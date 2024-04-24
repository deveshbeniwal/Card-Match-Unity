using UnityEngine;


[ExecuteAlways]
public class CamSizeHandler : MonoBehaviour
{
    [SerializeField] Camera maincamera;
    [SerializeField] float sensitivity;

    [Space]
    [SerializeField] float min_size;

    public void Initialize(Vector2 updated_pos)
    {
        transform.position = updated_pos;

        switch (DataManager.Instance.GameMode)
        {
            case GAME_MODES.EASY:
                Update_Values(2f, 3f);
                break;
            case GAME_MODES.MEDIUM:
                Update_Values(2.5f, 4f);
                break;
            case GAME_MODES.HARD:
                Update_Values(3.5f, 6.2f);
                break;
        }
    }
    void Update()
    {
        float unitsPerPixel = sensitivity / Screen.width;
        float desiredHalfHeight = unitsPerPixel * Screen.height;
        maincamera.orthographicSize = Mathf.Clamp(desiredHalfHeight, min_size, Mathf.Infinity);
    }


    private void Update_Values(float _senstivity, float _min_size)
    {
        sensitivity = _senstivity;
        min_size = _min_size;
    }
}