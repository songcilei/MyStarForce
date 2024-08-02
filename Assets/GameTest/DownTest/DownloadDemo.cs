using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameEntry = StarForce.GameEntry;

public class DownloadDemo : MonoBehaviour
{
    private void OnEnable()
    {
        GameEntry.Event.Subscribe(DownloadStartEventArgs.EventId,OnDownLoadStart);        
        GameEntry.Event.Subscribe(DownloadStartEventArgs.EventId,OnDownloadSuccess); 
        GameEntry.Event.Subscribe(DownloadStartEventArgs.EventId,OnDownloadFailure); 
        GameEntry.Event.Subscribe(DownloadStartEventArgs.EventId,OnDownloadUpdate); 
    }

    private void OnDisable()
    {
        GameEntry.Event.Unsubscribe(DownloadStartEventArgs.EventId,OnDownLoadStart);        
        GameEntry.Event.Unsubscribe(DownloadStartEventArgs.EventId,OnDownloadSuccess); 
        GameEntry.Event.Unsubscribe(DownloadStartEventArgs.EventId,OnDownloadFailure); 
        GameEntry.Event.Unsubscribe(DownloadStartEventArgs.EventId,OnDownloadUpdate); 
    }


    private void Start()
    {
        DownloadTest("TestImg.png","http://xxxx.TestImg.png");
        DownloadTest("TestAudio.mp3","http://xxxx.TestAudio.mp3");
        DownloadTest("TestDat.dat","http://xxxx.TestDat.dat");
    }

    protected void DownloadTest(string url,string filename)
    {
        GameEntry.Download.AddDownload(Application.persistentDataPath + "/" + filename, url);
    }

    private void OnDownLoadStart(object sender,GameEventArgs e)
    {
        Debug.Log("下载开始");        
    }

    private void OnDownloadSuccess(object sender,GameEventArgs e)
    {
        Debug.Log("下载成功");    
    }

    private void OnDownloadFailure(object sender,GameEventArgs e)
    {
        Debug.Log("下载失败");    
    }



    private void OnDownloadUpdate(object sender,GameEventArgs e)
    {
        Debug.Log("下载更新进度");    
    }















}
