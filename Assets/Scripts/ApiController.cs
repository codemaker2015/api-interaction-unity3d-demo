using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class ApiController : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        string url = "https://retrofit-backend-demo.herokuapp.com/book";
        StartCoroutine(GetBooksUsingWWW(url));
        StartCoroutine(GetBooksUsingUnityWebRequest(url));
    }

    // Update is called once per frame
    void Update() {
        
    }

    IEnumerator GetBooksUsingWWW(string url) {
        using (WWW www = new WWW(url)){
            yield return www;
            Debug.Log(www.text);
            JSONNode jsonNode = JSON.Parse(www.text);
            string title = jsonNode[0]["title"].ToString();
            Debug.Log("Title: " + title);
        }
    }

    IEnumerator GetBooksUsingUnityWebRequest(string url) {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
 
        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            Debug.Log(www.downloadHandler.text);
            JSONNode jsonNode = JSON.Parse(www.downloadHandler.text);
            string title = jsonNode[0]["title"].ToString();
            Debug.Log("Title: " + title);
        }
    }
}
