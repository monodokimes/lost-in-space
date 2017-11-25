﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour {

    private Dictionary<Element, int> _elements;

    // Called to update UI
    public UnityEvent OnChange { get; private set; }

    void Awake()
    {
        OnChange = new UnityEvent();
        _elements = new Dictionary<Element, int>();
    }

    // Use this for initialization
    void Start () {
        var fac = GameObject.FindGameObjectWithTag(GameTags.Elements).GetComponent<ElementFactory>();
        _elements = fac.GetElements().ToDictionary(e => e, _ => 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public KeyValuePair<Element, int>[] GetContents()
    {
        return _elements
            .Where(e => e.Value > 0)
            .OrderBy(e => e.Key.atomicNumber)
            .ToArray();
    }

    private bool Contains(int[] atomicNumbers)
    {
        return atomicNumbers.Any(n => !_elements.Values.Contains(n));
    }

    public void AddElements(Dictionary<Element, int> elements)
    {
        foreach (var item in elements)
        {
            AddElement(item.Key.atomicNumber, item.Value);
        }
        OnChange.Invoke();
    }

    void AddElement(int atomicNumber, int amount)
    {
        var element = _elements.Keys.Single(e => e.atomicNumber == atomicNumber);
        _elements[element] += amount;
    }
}
