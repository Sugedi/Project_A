using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// ====================================================================================================
// ����Ƽ �����Ϳ��� ����� �� �ִ� ����� ���� ȯ�� ȭ���� ����� ��ũ��Ʈ
// �ʱ�ȭ �� �� �� ȯ�� ȭ���� ǥ���ϸ�, ����ڰ� '�ٽ� ���� �ʱ�'�� �����ϸ� ���������� ǥ�õ��� �ʵ��� �����˴ϴ�.
// ���� ��ư�� ���� Unity Asset Store�� Ư�� �������� �̵��� �� �ֵ��� ��ũ�� �����Ǿ� �ֽ��ϴ�.
// ====================================================================================================


namespace CartoonFX
{
    // �����Ͱ� �ε�� �� �ڵ����� ����Ǵ� Ŭ�����Դϴ�.
    [InitializeOnLoad]
    public class CFXR_WelcomeScreen : EditorWindow
    {
        // ���� �����ڴ� ������ �����찡 �ε�� �� �� ���� ȣ��˴ϴ�.
        static CFXR_WelcomeScreen()
        {
            // delayCall�� ����Ͽ� �ʱ�ȭ �ڵ带 ����Ƽ �������� �ٸ� �ʱ�ȭ �۾��� �Ϸ�� �Ŀ� �����մϴ�.
            EditorApplication.delayCall += () =>
            {
                // �̹� ȯ�� ȭ���� ǥ�õǾ����� ���θ� ���� ���¿��� Ȯ���մϴ�.
                if (SessionState.GetBool("CFXR_WelcomeScreen_Shown", false))
                {
                    return;
                }
                // ȯ�� ȭ���� ǥ�õǾ����� ���� ���¿� �����մϴ�.
                SessionState.SetBool("CFXR_WelcomeScreen_Shown", true);

                // Ư�� GUID�� ���� ������ ��θ� �����ɴϴ�. �� ������ 'dontshow'�� ǥ�õǾ� �ִٸ� ȯ�� ȭ���� ǥ������ �ʽ��ϴ�.
                var importer = AssetImporter.GetAtPath(AssetDatabase.GUIDToAssetPath("bfd03f272fe010b4ba558a3bc456ffeb"));
                if (importer != null && importer.userData == "dontshow")
                {
                    return;
                }

                // �� ���ǵ��� �������� �ʴ� ���, ȯ�� ȭ���� ���ϴ�.
                Open();
            };
        }

        // ������ �޴��� �׸��� �߰��Ͽ� ȯ�� ȭ���� �������� �� �� �ְ� �մϴ�.
        [MenuItem("Tools/Cartoon FX Remaster FREE - Welcome Screen")]
        static void Open()
        {
            // ȯ�� ȭ�� �����츦 �����ϰ�, ũ�⸦ �����մϴ�.
            var window = GetWindow<CFXR_WelcomeScreen>(true, "Cartoon FX Remaster FREE", true);
            window.minSize = new Vector2(516, 370);
            window.maxSize = new Vector2(516, 370);
        }

        // �������� GUI�� �����ϴ� �޼ҵ��Դϴ�.
        private void CreateGUI()
        {
            // ��Ʈ VisualElement�� �����ɴϴ�.
            VisualElement root = rootVisualElement;
            // ��Ʈ�� ���̸� 100%�� �����մϴ�.
            root.style.height = new StyleLength(new Length(100, LengthUnit.Percent));

            // UXML ������ �ε��ϰ�, ��Ʈ�� �ν��Ͻ��� �߰��մϴ�.
            var uxmlDocument = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(AssetDatabase.GUIDToAssetPath("bfd03f272fe010b4ba558a3bc456ffeb"));
            root.Add(uxmlDocument.Instantiate());
            // USS ��Ÿ�Ͻ�Ʈ�� �ε��ϰ� �����մϴ�.
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath("f8b971f10a610844f968f582415df874"));
            root.styleSheets.Add(styleSheet);

            // Background image : ��� �̹����� �����մϴ�.
            root.style.backgroundImage = new StyleBackground(AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath("fed1b64fd853f994c8d504720a0a6d44")));
            root.style.unityBackgroundScaleMode = ScaleMode.ScaleAndCrop;

            // Logo image : �ΰ� �̹����� �����մϴ�.
            var titleImage = root.Q<Image>("img_title");
            titleImage.image = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath("a665b2e53088caa4c89dd09f9c889f62"));

            // Buttons : ��ư�鿡 Ŭ�� �̺�Ʈ�� �߰��մϴ�. �� ��ư�� Unity Asset Store�� Ư�� �������� ���ϴ�.
            root.Q<Label>("btn_cfxr1").AddManipulator(new Clickable(evt => { Application.OpenURL("https://assetstore.unity.com/packages/slug/4010"); }));
            root.Q<Label>("btn_cfxr2").AddManipulator(new Clickable(evt => { Application.OpenURL("https://assetstore.unity.com/packages/slug/4274"); }));
            root.Q<Label>("btn_cfxr3").AddManipulator(new Clickable(evt => { Application.OpenURL("https://assetstore.unity.com/packages/slug/10172"); }));
            root.Q<Label>("btn_cfxr4").AddManipulator(new Clickable(evt => { Application.OpenURL("https://assetstore.unity.com/packages/slug/23634"); }));
            root.Q<Label>("btn_cfxrbundle").AddManipulator(new Clickable(evt => { Application.OpenURL("https://assetstore.unity.com/packages/slug/232385"); }));

            // '�ٽ� ���� �ʱ�' ��ư�� Ŭ�� �̺�Ʈ�� �߰��մϴ�. �� ��ư�� Ŭ���ϸ� ȯ�� ȭ���� �ݰ�, �ٽ� ǥ������ �ʵ��� �����մϴ�.
            root.Q<Button>("close_dontshow").RegisterCallback<ClickEvent>(evt =>
            {
                this.Close();
                var importer = AssetImporter.GetAtPath(AssetDatabase.GUIDToAssetPath("bfd03f272fe010b4ba558a3bc456ffeb"));
                importer.userData = "dontshow";
                importer.SaveAndReimport();
            });
            // �ݱ� ��ư�� Ŭ�� �̺�Ʈ�� �߰��մϴ�. �� ��ư�� Ŭ���ϸ� ȯ�� ȭ���� �ݽ��ϴ�.
            root.Q<Button>("close").RegisterCallback<ClickEvent>(evt => { this.Close(); });
        }
    }
}
