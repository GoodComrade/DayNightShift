using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class Product : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public event UnityAction OnDropProduct;
    public Sprite Sprite { get; private set; }
    public Color Color { get; private set; }

    private Image _image;
    private RectTransform _rect;
    private Transform _dragParent;
    private Transform _dragDestination;

    private Transform _prevParent;

    private void Awake()
    {
        _image = GetComponent<Image>() ?? null;
        _rect = GetComponent<RectTransform>();
    }

    public void Init(Transform dragParent , Transform dragDestination)
    {
        _dragParent = dragParent;
        _dragDestination = dragDestination;
    }

    public void Render(Sprite sprite)
    {
        _image.sprite = sprite;
        _rect.sizeDelta = new Vector2(sprite.rect.size.x / 2f, sprite.rect.size.y / 2f);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _prevParent = transform.parent;
        transform.parent = _dragParent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        TurnImageReycastTarget(false);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var raycastResult = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResult);

        foreach (var item in raycastResult)
        {
            if (item.gameObject.name == _dragDestination.gameObject.name)
            {
                transform.parent = _dragDestination;
                TurnImageReycastTarget(true);
                OnDropProduct.Invoke();
                return;
            }
            else
            {
                Debug.Log(item.gameObject.name);
            }
        }
            
        transform.parent = _prevParent;
        transform.position = _prevParent.position;
        TurnImageReycastTarget(true);
    }

    public void OnDrop(PointerEventData eventData)
    {

    }

    public virtual void TurnImageReycastTarget(bool value)
    {
        if(_image != null)
            _image.raycastTarget = value;
    }
}
