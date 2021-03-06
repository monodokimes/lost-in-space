﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementItem : MonoBehaviour
{

    public Element Element { get; set; }
    public int Amount { get; set; }

    public GameObject elementImage;
    
    // Use this for initialization
    void Start()
    {
        if (Element == null)
            return;

        elementImage.SetActive(true);
        elementImage.GetComponent<Image>().sprite = Element.sprite;
        GetComponentInChildren<Text>().text = Amount.ToString();

        var button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            var inv = GameObject.FindGameObjectWithTag(GameTags.Player).GetComponent<Inventory>();
            inv.Remove(Element);

            var panel = GetComponentInParent<InventoryPanel>();
            panel.AddCraftItem(Element);
        });
    }    
}
