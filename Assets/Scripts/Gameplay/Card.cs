using System;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] BoxCollider2D box_collider;

    [Space]
    [SerializeField] SpriteRenderer sr_card;
    [SerializeField] Sprite spr_card_back;

    Sprite card_picture;

    bool isShowingCard;
    bool isAnimProcessing;

    Action flip_anim_callback;

    public void Initialize(Vector3 _position, Sprite _card_picture, int _card_picture_index)
    {
        transform.position = _position;
        gameObject.name = _card_picture_index.ToString();
        card_picture = _card_picture;

        anim.Play("Init");

        GameManager.instance.Event_ShowCards += Flip_Card_Show;
        GameManager.instance.Event_HideCards += Flip_Card_Hide;
    }
    private void OnDestroy()
    {
        GameManager.instance.Event_ShowCards -= Flip_Card_Show;
        GameManager.instance.Event_HideCards -= Flip_Card_Hide;
    }


    public void Flip_Card_Show()
    {
        isAnimProcessing = true;

        isShowingCard = true;
        box_collider.enabled = false;

        anim.Play("Flip");
        SoundManager.instance.PlaySound(SOUND_TYPE.CARD_FLIP);
    }
    public void Flip_Card_Hide()
    {
        isAnimProcessing = true;

        isShowingCard = false;
        anim.Play("Flip");
        SoundManager.instance.PlaySound(SOUND_TYPE.CARD_FLIP);
    }
    public void Destroy_Card()
    {
        isAnimProcessing = true;
        anim.Play("Destroy");
    }


    private void AnimEvent_CardFlip_Completed()
    {
        isAnimProcessing = false;

        box_collider.enabled = !isShowingCard;
        Execute_CardFlip_Callback();
    }
    private void AnimEvent_CardDestroy_Completed()
    {
        Destroy(this.gameObject);
    }
    private void AnimEvent_CardFlip()
    {
        sr_card.sprite = isShowingCard ? card_picture : spr_card_back;
    }
    private void Execute_CardFlip_Callback()
    {
        flip_anim_callback?.Invoke();
        flip_anim_callback = null;
    }



    private void OnMouseDown()
    {
        if (isAnimProcessing)
            return;

        if (isShowingCard)
            return;

        flip_anim_callback = () => { GameManager.instance.OnCard_Select(this); };
        Flip_Card_Show();
    }
}
