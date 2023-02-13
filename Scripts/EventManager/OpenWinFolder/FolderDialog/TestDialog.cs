using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class TestDialog : MonoBehaviour
{
    public Texture2D loadImags;
    public string imagePath;
    public Material material;
    public Renderer render;
    public Image image;
    private void OnGUI()
    {
        if (GUILayout.Button("打开文件夹窗口"))
        {
            var windowDialog = new WindowDialog();

            windowDialog.FilterFileParameters = new FilterFileParameters()
            {
                iSMultiSelect = false,
                WindowTitle = "Open Folder Window",
                firstOpenPath = "D:\\",
                RestoreDirectory = false,
                filterIndex = 1,
                SelectFileType = "Image files (*.jpg,*.jpeg,*exr,*.png,*.tga) | *.jpg; *.exr; *.jpeg; *.png; *.tga",
            };

            windowDialog.OpenWindowFolder((value) =>
            {
                imagePath = value;
                if (File.Exists(value))
                {
                    byte[] bytes = File.ReadAllBytes(value);
                    loadImags = new Texture2D(1, 1);
                    ImageConversion.LoadImage(loadImags, bytes, true);
                    loadImags.LoadImage(bytes);
                    Debug.Log(loadImags.width + "  " + loadImags.height);

                    material = new Material(Shader.Find("Standard"));
                    material.mainTexture = loadImags;
                    render.material = material;
                    var size = image.rectTransform.sizeDelta;

                    image.sprite = Sprite.Create(loadImags, new Rect(0, 0, loadImags.width, loadImags.height), new Vector2(0.5f, 0.5f), 100f, 1u, SpriteMeshType.Tight);
                }
            });
        }
    }
}