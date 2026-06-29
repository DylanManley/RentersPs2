using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class cutscenePlayer : MonoBehaviour
{
    [SerializeField] private VideoPlayer player;

    void Start()
    {
        if (string.IsNullOrEmpty(CutsceneRouter.VideoPath))
        {
            UnityEngine.Debug.Log("no video path found");
            CutsceneEnd(player);
            return;
        }

        VideoClip cutscene = Resources.Load<VideoClip>(CutsceneRouter.VideoPath);

        if (cutscene == null)
        {
            UnityEngine.Debug.Log("no video clip found at" + CutsceneRouter.VideoPath);
            CutsceneEnd(player);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player.clip = cutscene;
        player.loopPointReached += CutsceneEnd;
        player.Play();
    }

    void CutsceneEnd(VideoPlayer player)
    {
        SceneManager.LoadScene(CutsceneRouter.NextSceneName);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CutsceneEnd(player);
        }
    }
}
