using UnityEngine;
using UnityEngine.Video;

public class RoomManager : MonoBehaviour
{
    [Header("Wall Display")]
    public Renderer wallRenderer;
    public Material imageMaterial;
    public Material videoMaterial;

    [Header("Video")]
    public VideoPlayer videoPlayer;

    [Header("Content")]
    public WallDisplayItem[] items;

    private int currentIndex = 0;

    private void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.playOnAwake = false;
            videoPlayer.isLooping = true;
            videoPlayer.prepareCompleted += OnVideoPrepared;
        }

        ShowCurrentItem();
    }

    private void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.prepareCompleted -= OnVideoPrepared;
        }
    }

    public void ShiftRoom()
    {
        if (items == null || items.Length == 0)
            return;

        currentIndex++;
        if (currentIndex >= items.Length)
            currentIndex = 0;

        ShowCurrentItem();
    }

    private void ShowCurrentItem()
    {
        if (items == null || items.Length == 0 || wallRenderer == null)
            return;

        WallDisplayItem item = items[currentIndex];

        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            videoPlayer.clip = null;
        }

        if (item.videoClip != null)
        {
            wallRenderer.material = videoMaterial;
            videoPlayer.clip = item.videoClip;
            videoPlayer.Prepare();
            return;
        }

        if (item.imageTexture != null)
        {
            wallRenderer.material = imageMaterial;
            wallRenderer.material.mainTexture = item.imageTexture;
        }
    }

    private void OnVideoPrepared(VideoPlayer source)
    {
        source.Play();
    }
}