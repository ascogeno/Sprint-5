using UnityEngine;
using UnityEngine.Video;

public class RoomManager : MonoBehaviour
{
    [Header("Wall Display")]
    public Renderer wallRenderer;

    [Tooltip("Material used when showing normal images")]
    public Material imageMaterial;

    [Tooltip("Material used when showing video via Render Texture")]
    public Material videoMaterial;

    [Header("Video")]
    public VideoPlayer videoPlayer;

    [Header("Content")]
    public WallDisplayItem[] items;

    private int currentIndex = 0;

    private void Start()
    {
        ShowCurrentItem();
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

        // Stop previous video if one was playing
        if (videoPlayer != null && videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }

        // VIDEO MODE
        if (item.videoClip != null)
        {
            wallRenderer.material = videoMaterial;

            videoPlayer.clip = item.videoClip;
            videoPlayer.isLooping = true;
            videoPlayer.Play();

            return;
        }

        // IMAGE MODE
        if (item.imageTexture != null)
        {
            wallRenderer.material = imageMaterial;
            wallRenderer.material.mainTexture = item.imageTexture;
        }
    }
}