﻿using CloudData;
using Loading;
using ObjectCreation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Controllers {

    /* This enabled loading several PointClouds at the same time. The configured options work globally.
     * This class uses a ConcurrentOneTimeRenderer, so only when the user presses "X", the cloud is reloaded
     */
    public class PointCloudSetOneTimeController : AbstractPointSetController {

        public uint pointBudget;
        public int minNodeSize;
        //Defines the type of PointCloud (Points, Quads, Circles)
        public MeshConfiguration meshConfiguration;
        
        private Camera userCamera;

        // Use this for initialization
        protected override void Start() {
            userCamera = Camera.main;
            pRenderer = new ConcurrentOneTimeRenderer(minNodeSize, pointBudget, userCamera);
            base.Start();
        }
        
        // Update is called once per frame
        void Update() {
            if (!CheckReady()) return;
            if (!pRenderer.IsLoadingPoints() && Input.GetKey(KeyCode.X) && !pRenderer.HasNodesToRender()/* && !pRenderer.HasNodesToDelete()*/) {
                Debug.Log("Updating!");
                pRenderer.UpdateRenderingQueue(meshConfiguration);
                pRenderer.StartUpdatingPoints();
            } else {
                pRenderer.UpdateGameObjects(meshConfiguration);
            }
        }
    }
}