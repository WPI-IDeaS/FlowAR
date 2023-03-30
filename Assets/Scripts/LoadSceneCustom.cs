// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.SceneSystem;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit.Diagnostics;
using Microsoft.MixedReality.Toolkit.Extensions.SceneTransitions;
using Microsoft.MixedReality.Toolkit;

public class LoadSceneCustom : MonoBehaviour
{
    //public int objId = 0;
    //public float[] objScale = new float[3] {1,1,1};
    // Start is called before the first frame update
    /// <summary>
    /// Utility class to load scenes through MRTK Scene System using a scene transition.
    /// Otherwise, it uses Scene System's LoadContent()
    /// </summary>
    //[AddComponentMenu("Scripts/MRTK/Extensions/LoadContentScene")]
    [SerializeField]
    private LoadSceneMode loadSceneMode = LoadSceneMode.Single;
    [SerializeField]
    private SceneInfo contentScene = SceneInfo.Empty;
    [SerializeField]
    private bool loadOnStartup = false;

    private void Start()
    {
        if (loadOnStartup)
        {
            LoadContent();
        }
    }

    /// <summary>
    /// Load a scene with contentScene.Name
    /// </summary>
    public void LoadContent()
    {
        if (!contentScene.Equals(SceneId.currentScene))
        {
            ISceneTransitionService transitions = MixedRealityToolkit.Instance.GetService<ISceneTransitionService>();
            if (transitions.TransitionInProgress)
            {
                return;
            }
            transitions.DoSceneTransition(() => CoreServices.SceneSystem.LoadContent(contentScene.Name, loadSceneMode));
            SceneId.currentScene = contentScene;
        }
        
    }
   

    /*public void SetObj()
    {
        ObjectIDBoard.object_id = objId;
        ObjectIDBoard.scale = objScale;
    }*/


}
