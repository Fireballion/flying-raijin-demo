using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine;

public class RadialMenuEntry : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    public delegate void RadialMenuEntryDelegate(RadialMenuEntry pEntry);

    [SerializeField]
    TextMeshProUGUI label;
    [SerializeField]
    RawImage icon;
    RawImage background;
    RectTransform rect;

    RadialMenuEntryDelegate Callback;
    GameObject assignedSeal;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    public void SetLabel(string pText)
    {
        label.text = pText;
    }
    public void SetBackground(Texture pBackGround)
    {
        background.texture = pBackGround;
    }
    public void SetIcon(Texture pIcon)
    {
        icon.texture = pIcon;
    }
    public void SetSeal(GameObject pseal)
    {
        assignedSeal = pseal;
    }
    public GameObject GetSeal()
    {
        return (assignedSeal);
    }
    public Texture GetIcon()
    {
        return (icon.texture);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Callback?.Invoke(this);
    }

    public void SetCallback(RadialMenuEntryDelegate pCallback)
    {
        Callback = pCallback;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        rect.DOComplete();
        rect.DOScale(Vector3.one, .3f).SetEase(Ease.OutQuad);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rect.DOComplete();
        rect.DOScale(Vector3.one*1.5f, .3f).SetEase(Ease.OutQuad);
    }
}
