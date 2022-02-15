using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
    
    public GameObject entryPrefab;
    public GameObject player;
    public MovementInput input;
    public float radius;
    public bool isOpen;
    [SerializeField]
    List<Texture> icons;
    public List<RadialMenuEntry> entries;
    List<GameObject> pSeals;
    

    // Start is called before the first frame update
    void Start()
    {
        entries = new List<RadialMenuEntry>();
        isOpen = false;
        
        input = player.GetComponent<MovementInput>();


    }

    // Update is called once per frame
    void AddEntry(string pLabel, Texture pIcon, GameObject pSeal, RadialMenuEntry.RadialMenuEntryDelegate pCallback)
    {
        GameObject entry = Instantiate(entryPrefab,transform);
        RadialMenuEntry rme = entry.GetComponent<RadialMenuEntry>();
        rme.SetLabel(pLabel);
        rme.SetIcon(pIcon);
        rme.SetSeal(pSeal);
        rme.SetCallback(pCallback);
        entries.Add(rme);
    }
    bool recast;
    public void SetList(List<GameObject> assignedListSeals, bool shouldcastTwice)
    {
        pSeals = assignedListSeals;
        recast = shouldcastTwice;
    }
    public void Open()
    {
        isOpen = true;
        for (int i = 0; i < pSeals.Count; i++)
        {
            //AddEntry("Button" + i.ToString(), icons[0]);
            Debug.Log(pSeals[i].name);
            if (pSeals[i].name == "PlayerSeal")
            {
                AddEntry("Button" + i.ToString(), icons[0], pSeals[i].gameObject, WarpToSeal);
            }
            else
            {
                AddEntry("Button" + i.ToString(), icons[1], pSeals[i].gameObject, WarpToSeal);
            }


        }
        Rearrange();
        
    }
    //public void OpenPlayerSeals()
    //{
    //    for (int i = 0; i < input.listFlyingRaijinSeals.Count; i++)
    //    {
    //        //AddEntry("Button" + i.ToString(), icons[0]);
    //        Debug.Log(input.listFlyingRaijinSeals[i].name);
    //        if (input.listFlyingRaijinSeals[i].name == "PlayerSeal")
    //        {
    //            AddEntry("Button" + i.ToString(), icons[0], input.listFlyingRaijinSeals[i].gameObject, WarpToSeal);
    //        }
    //        //else
    //        //{
    //        //    AddEntry("Button" + i.ToString(), icons[1], input.listFlyingRaijinSeals[i].gameObject, WarpToSeal);
    //        //}


    //    }
    //    Rearrange();
    //    isOpen = true;
    //}
    //public void OpenNormalSeals()
    //{
    //    for (int i = 0; i < input.listFlyingRaijinSeals.Count; i++)
    //    {
    //        //AddEntry("Button" + i.ToString(), icons[0]);
    //        Debug.Log(input.listFlyingRaijinSeals[i].name);
    //        if (input.listFlyingRaijinSeals[i].name == "PlayerSeal")
    //        {
    //            //AddEntry("Button" + i.ToString(), icons[0], input.listFlyingRaijinSeals[i].gameObject, WarpToSeal);
    //        }
    //        else
    //        {
    //            AddEntry("Button" + i.ToString(), icons[1], input.listFlyingRaijinSeals[i].gameObject, WarpToSeal);
    //        }


    //    }
    //    Rearrange();
    //    isOpen = true;
    //}

    public Vector2 normalisedMousePosition;
    public float currentAngle;
    public int selection;
    private void Update()
    {
        
        if (isOpen)
        {
            if (pSeals.Count < entries.Count)
            {
                Close();
                Open();
                Rearrange();
            }
        }
        //    if (isOpen)
        //{
        //    if (pSeals.Count < entries.Count)
        //    {
        //        Close();
        //        Open();
        //        Rearrange();
        //    }
        //    normalisedMousePosition = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
        //    currentAngle = Mathf.Atan2(normalisedMousePosition.y, normalisedMousePosition.x) * Mathf.Rad2Deg;
        //    currentAngle = (currentAngle + 360) % 360;
        //    selection = (int) currentAngle / 45;
        //    Debug.Log(selection);
        //}
        
    }
    public void Close()
    {
        for (int i = 0; i < entries.Count; i++)
        {
            //AddEntry("Button" + i.ToString(), icons[0]);
            RectTransform rect = entries[i].GetComponent<RectTransform>();
            GameObject entry = entries[i].gameObject; 
            rect.DOAnchorPos(Vector3.zero, .3f).SetEase(Ease.OutQuad).onComplete =
                delegate ()
                {
                    Destroy(entry);
                };

        }
        entries.Clear();
        isOpen = false;
    }
    public void Toggle()
    {
        if (entries.Count == 0) {
            Open();
        }
        else
        {
            Close();
        }
    }
    public void Rearrange()
    {
        float radiansOfSeparation = (Mathf.PI * 2) / entries.Count;
        Debug.Log(entries);
        for (int i = 0; i<9; i++)
        {
            float x = Mathf.Sin(radiansOfSeparation*i) * radius;
            float y = Mathf.Cos(radiansOfSeparation * i) * radius;
            RectTransform rect = entries[i].GetComponent<RectTransform>();
            rect.localScale = Vector3.zero;
            rect.DOScale(Vector3.one, .3f).SetEase(Ease.OutQuad).SetDelay(0.025f * i);
            rect.DOAnchorPos(new Vector3(x,y,0),.3f).SetEase(Ease.OutQuad).SetDelay(0.025f*i);


        }
    }
    public void WarpToSeal(RadialMenuEntry pEntry)
    {
        if (recast)
        {
            input.SetMark(pEntry.GetSeal(), recast);
        }
        else
        {
            input.SetMark(pEntry.GetSeal(), false);
        }
        
        Close();
        //input.TeleportToKunai(pEntry.GetSeal());
        
    }
}
