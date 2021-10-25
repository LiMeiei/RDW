using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogTest : MonoBehaviour
{
    [HideInInspector]
    public ILogger logger;
    public ILogger logger2;

    void Start()
    {
        this.logger = FileAppender.Create("logfile.txt", true);
        this.logger2 = FileAppender.Create("logfile2.txt", true);
    }

    void OnDestroy()
    {
        if (this.logger.logHandler is FileAppender)
            ((FileAppender)this.logger.logHandler).Close();
    }

    void Update()
    {
    }
}