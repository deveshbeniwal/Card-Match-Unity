using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] BoxCollider2D box_collider;

    [Space]
    [SerializeField] SpriteRenderer sr_card;
    [SerializeField] Sprite spr_card_back;

    Sprite card_picture;
    int card_picture_index;

    bool isShowingCard;
    bool isAnimProcessing;

    public void Initialize(Vector3 _position, Sprite _card_picture, int _card_picture_index)
    {
        transform.position = _position;
        gameObject.name = _card_picture_index.ToString();

        anim.Play("Init");

        card_picture = _card_picture;
        card_picture_index = _card_picture_index;

        GameManager.instance.Event_ShowCards += Instance_Event_ShowCards;
        GameManager.instance.Event_HideCards += Instance_Event_HideCards;
        GameManager.instance.Event_StartGame += Instance_Event_StartGame;
    }
    private void OnDestroy()
    {
        GameManager.instance.Event_ShowCards -= Instance_Event_ShowCards;
        GameManager.instance.Event_HideCards -= Instance_Event_HideCards;
        GameManager.instance.Event_StartGame -= Instance_Event_StartGame;
    }



    private void Instance_Event_ShowCards()
    {
        isShowingCard = true;
        anim.Play("Flip");
    }
    private void Instance_Event_HideCards()
    {
        isShowingCard = false;
        anim.Play("Flip");
    }
    private void Instance_Event_StartGame()
    {
        box_collider.enabled = true;
    }

    private void Anim_Event_Flipcard()
    {
        sr_card.sprite = isShowingCard ? card_picture : spr_card_back;
    }
    private void Anim_Event_Anim_Started()
    {
        isAnimProcessing = true;
    }
    private void Anim_Event_Anim_Stopped()
    {
        isAnimProcessing = false;
    }



    private void OnMouseDown()
    {
        if (isAnimProcessing)
            return;

        if (isShowingCard)
            return;

        Debug.Log(gameObject.name);
    }
}
