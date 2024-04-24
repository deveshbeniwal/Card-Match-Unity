using UnityEngine;

public class BaseScreen : MonoBehaviour
{
    public SCREEN_TYPE screen_type;
    [SerializeField] Animation screen_anim;


    public virtual void Show()
    {
        if (gameObject.activeInHierarchy)
            return;

        gameObject.SetActive(true);
        if (screen_anim != null && screen_anim.GetClip("Show") != null)
        {
            screen_anim.clip = screen_anim.GetClip("Show");
            screen_anim.Play();
        }
    }
    public virtual void Hide()
    {
        if (!gameObject.activeInHierarchy)
            return;

        float switchTime = 0;
        if (screen_anim != null && screen_anim.GetClip("Hide") != null)
        {
            screen_anim.clip = screen_anim.GetClip("Hide");
            screen_anim.Play();

            switchTime = screen_anim.clip.averageDuration;
        }

        Timer.Schedule(this, switchTime, () =>
        {
            gameObject.SetActive(false);
        });
    }
}