using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Monster;
using UnityEngine.Networking;
using System.Text;

public class RewardManager : SingletonBehaviour<RewardManager>
{
    public struct Rank {
        public int id;
        public int score;
    }
    public IEnumerator IncrScore(int id, int score, string reason) {
        Debug.Log("IncrScore Reason:" + reason);
        Rank rank = new Rank();
        rank.id = id;
        rank.score = score;
        string data = JsonUtility.ToJson(rank);
        Debug.Log(data);
        UnityWebRequest webRequest = UnityWebRequest.PostWwwForm("http://dev-hojin.shop:8888/incrscore", data);
        webRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(data));
        webRequest.SetRequestHeader("Content-Type", "application/json");
        if(webRequest.isNetworkError || webRequest.isHttpError) {
            Debug.Log(webRequest.error);
        } else {
            Debug.Log("rank score incr");
        }
        yield return webRequest.SendWebRequest();
        Debug.Log(webRequest.downloadHandler.text);
        rank = JsonUtility.FromJson<Rank>(webRequest.downloadHandler.text);
    }
}