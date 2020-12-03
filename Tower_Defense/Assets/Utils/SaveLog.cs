using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLog : MonoBehaviour
{
    public static SaveLog _Instance;

    public static SaveLog instance
    {
        get
        {
            if (_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.

                _Instance = FindObjectOfType(typeof(SaveLog)) as SaveLog;
            }

            // If it is still null, create a new instance
            if (_Instance == null)
            {
                GameObject obj = new GameObject("SaveLog");
                _Instance = obj.AddComponent(typeof(SaveLog)) as SaveLog;

                // Debug.Log("Could not locate an AManager object.  AManager was Generated Automaticly.");
                //Debug.Log(obj);
            }

            return _Instance;
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Log("지진 체험 시작");
    }
    
    public string GetDateTime()
    {
        DateTime NowDate = DateTime.Now;
        return NowDate.ToString("yyyy-MM-dd HH:mm:ss") + ":" + NowDate.Millisecond.ToString("000");
    }



    /// 로그내용
    public void Log(String msg)
    {

        string FilePath = Directory.GetCurrentDirectory() + @"\Logs\" + DateTime.Today.ToString("yyyyMMdd") + ".log";

        string DirPath = Directory.GetCurrentDirectory() + @"\Logs";

        string temp;



        DirectoryInfo di = new DirectoryInfo(DirPath);
        FileInfo fi = new FileInfo(FilePath);


        try{
            if (di.Exists != true) Directory.CreateDirectory(DirPath);
            if (fi.Exists != true)
            {
                using (StreamWriter sw = new StreamWriter(FilePath))
                {
                    temp = string.Format("[{0}] {1}", GetDateTime(), msg);
                    sw.WriteLine(temp);
                    sw.Close();
                }
            }else {

                using (StreamWriter sw = File.AppendText(FilePath))
                {
                    temp = string.Format("[{0}] {1}", GetDateTime(), msg);
                    sw.WriteLine(temp);
                    sw.Close();
                }
            }
        }

        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
