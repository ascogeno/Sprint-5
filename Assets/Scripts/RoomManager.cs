using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Renderer wallPictureRenderer;
    public Material[] pictureMaterials;

    private int currentIndex = 0;

    public void ShiftRoom()
    {
        if (pictureMaterials == null || pictureMaterials.Length == 0 || wallPictureRenderer == null)
            return;

        currentIndex++;
        if (currentIndex >= pictureMaterials.Length)
        {
            currentIndex = 0;
        }

        wallPictureRenderer.material = pictureMaterials[currentIndex];
    }
}