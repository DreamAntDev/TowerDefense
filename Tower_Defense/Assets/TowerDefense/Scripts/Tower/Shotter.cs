﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotter : MonoBehaviour
{
    int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Shot(MonsterType target) // Default Hit-Scan
    {
        Debug.Log(damage);
    }
}
