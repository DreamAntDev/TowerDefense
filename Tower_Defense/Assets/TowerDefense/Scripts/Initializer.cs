using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : SingletonBehaviour<Initializer>
{
    public List<GameObject> gameObjects;
    private new void Awake()
    {
        base.Awake();
        foreach(var obj in gameObjects)
        {
            if(obj != null)
                Instantiate(obj);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
