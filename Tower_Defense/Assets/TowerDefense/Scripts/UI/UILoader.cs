using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Util;

public sealed class UILoader : SingletonBehaviour<UILoader>
{
    [System.Serializable]
    struct UIObject
    {
        public string name;
        public GameObject obj;
    }

    [SerializeField]
    private List<UIObject> UIList = new List<UIObject>();
    private Dictionary<string, GameObject> UIObjectDictionary;
    private Dictionary<string, GameObject> LoadedUIObjectDictionary;
    private new void Awake()
    {
        base.Awake();
        UIObjectDictionary = new Dictionary<string, GameObject>();
        LoadedUIObjectDictionary = new Dictionary<string, GameObject>();
        for (int i=0;i<UIList.Count;i++)
        {
            var item = UIList[i];
            if(string.IsNullOrEmpty(item.name) == true || item.obj == null)
            {
                Debug.LogErrorFormat("[UILoader][{0}]{1} 가 오류", i);
                continue;
            }
            if(UIObjectDictionary.Keys.Any((obj) => obj.Equals(item.name)) == true)
            {
                Debug.LogErrorFormat("[UILoader][{0}]{1} 가 중복 이름입니다.", i,item.name);
                continue;
            }
            if (UIObjectDictionary.Values.Any((obj) => obj.Equals(item.obj)) == true)
            {
                Debug.LogErrorFormat("[UILoader][{0}]{1} 가 중복 오브젝트입니다.", i, item.name);
                continue;
            }
            UIObjectDictionary.Add(item.name, item.obj);   
        }
    }
    public GameObject Load(string name)
    {
        GameObject obj;
        if (this.LoadedUIObjectDictionary.TryGetValue(name, out obj) == false)
        {
            GameObject prefab;
            if(UIObjectDictionary.TryGetValue(name,out prefab) == false)
            {
                Debug.LogErrorFormat("{0} 은 UILoader에 Regist되지 않았습니다.", name);
                return null;
            }
            obj = UnityEngine.Object.Instantiate<GameObject>(prefab);
            if (obj != null)
                LoadedUIObjectDictionary.Add(name, obj);
            else
                Debug.LogErrorFormat("UI Load Fail {0}", name);
        }
        else
            Debug.LogErrorFormat("Already Loaded {0}", name);

        return obj;
    }
    public void Unload(GameObject obj)
    {
        if (obj == null)
            return;

        if (this.LoadedUIObjectDictionary.ContainsValue(obj) == false)
        {
            Debug.LogErrorFormat("NotExist {0}", obj.name);
            return;
        }

        var removePair = this.LoadedUIObjectDictionary.Where(pair => pair.Value.Equals(obj)).First();
        GameObject.Destroy(removePair.Value);
        this.LoadedUIObjectDictionary.Remove(removePair.Key);
    }
    public void Unload(string name)
    {
        GameObject obj;
        if (this.LoadedUIObjectDictionary.TryGetValue(name, out obj) == false)
        {
            return;
        }
        GameObject.Destroy(obj);
        this.LoadedUIObjectDictionary.Remove(name);
    }
    public GameObject GetUI(string name)
    {
        GameObject ret = null;
        if(this.LoadedUIObjectDictionary.TryGetValue(name, out ret) == false)
        {

        }
        return ret;
    }
}
