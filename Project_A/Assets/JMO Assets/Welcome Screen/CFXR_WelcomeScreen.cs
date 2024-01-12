using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// ====================================================================================================
// 유니티 에디터에서 사용할 수 있는 사용자 정의 환영 화면을 만드는 스크립트
// 초기화 시 한 번 환영 화면을 표시하며, 사용자가 '다시 보지 않기'를 선택하면 다음번에는 표시되지 않도록 설정됩니다.
// 여러 버튼을 통해 Unity Asset Store의 특정 페이지로 이동할 수 있도록 링크가 설정되어 있습니다.
// ====================================================================================================


namespace CartoonFX
{
    // 에디터가 로드될 때 자동으로 실행되는 클래스입니다.
    [InitializeOnLoad]
    public class CFXR_WelcomeScreen : EditorWindow
    {
        // 정적 생성자는 에디터 윈도우가 로드될 때 한 번만 호출됩니다.
        static CFXR_WelcomeScreen()
        {
            // delayCall을 사용하여 초기화 코드를 유니티 에디터의 다른 초기화 작업이 완료된 후에 실행합니다.
            EditorApplication.delayCall += () =>
            {
                // 이미 환영 화면이 표시되었는지 여부를 세션 상태에서 확인합니다.
                if (SessionState.GetBool("CFXR_WelcomeScreen_Shown", false))
                {
                    return;
                }
                // 환영 화면이 표시되었음을 세션 상태에 저장합니다.
                SessionState.SetBool("CFXR_WelcomeScreen_Shown", true);

                // 특정 GUID를 가진 에셋의 경로를 가져옵니다. 이 에셋이 'dontshow'로 표시되어 있다면 환영 화면을 표시하지 않습니다.
                var importer = AssetImporter.GetAtPath(AssetDatabase.GUIDToAssetPath("bfd03f272fe010b4ba558a3bc456ffeb"));
                if (importer != null && importer.userData == "dontshow")
                {
                    return;
                }

                // 위 조건들을 만족하지 않는 경우, 환영 화면을 엽니다.
                Open();
            };
        }

        // 에디터 메뉴에 항목을 추가하여 환영 화면을 수동으로 열 수 있게 합니다.
        [MenuItem("Tools/Cartoon FX Remaster FREE - Welcome Screen")]
        static void Open()
        {
            // 환영 화면 윈도우를 생성하고, 크기를 설정합니다.
            var window = GetWindow<CFXR_WelcomeScreen>(true, "Cartoon FX Remaster FREE", true);
            window.minSize = new Vector2(516, 370);
            window.maxSize = new Vector2(516, 370);
        }

        // 윈도우의 GUI를 생성하는 메소드입니다.
        private void CreateGUI()
        {
            // 루트 VisualElement를 가져옵니다.
            VisualElement root = rootVisualElement;
            // 루트의 높이를 100%로 설정합니다.
            root.style.height = new StyleLength(new Length(100, LengthUnit.Percent));

            // UXML 문서를 로드하고, 루트에 인스턴스를 추가합니다.
            var uxmlDocument = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(AssetDatabase.GUIDToAssetPath("bfd03f272fe010b4ba558a3bc456ffeb"));
            root.Add(uxmlDocument.Instantiate());
            // USS 스타일시트를 로드하고 적용합니다.
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath("f8b971f10a610844f968f582415df874"));
            root.styleSheets.Add(styleSheet);

            // Background image : 배경 이미지를 설정합니다.
            root.style.backgroundImage = new StyleBackground(AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath("fed1b64fd853f994c8d504720a0a6d44")));
            root.style.unityBackgroundScaleMode = ScaleMode.ScaleAndCrop;

            // Logo image : 로고 이미지를 설정합니다.
            var titleImage = root.Q<Image>("img_title");
            titleImage.image = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath("a665b2e53088caa4c89dd09f9c889f62"));

            // Buttons : 버튼들에 클릭 이벤트를 추가합니다. 각 버튼은 Unity Asset Store의 특정 페이지를 엽니다.
            root.Q<Label>("btn_cfxr1").AddManipulator(new Clickable(evt => { Application.OpenURL("https://assetstore.unity.com/packages/slug/4010"); }));
            root.Q<Label>("btn_cfxr2").AddManipulator(new Clickable(evt => { Application.OpenURL("https://assetstore.unity.com/packages/slug/4274"); }));
            root.Q<Label>("btn_cfxr3").AddManipulator(new Clickable(evt => { Application.OpenURL("https://assetstore.unity.com/packages/slug/10172"); }));
            root.Q<Label>("btn_cfxr4").AddManipulator(new Clickable(evt => { Application.OpenURL("https://assetstore.unity.com/packages/slug/23634"); }));
            root.Q<Label>("btn_cfxrbundle").AddManipulator(new Clickable(evt => { Application.OpenURL("https://assetstore.unity.com/packages/slug/232385"); }));

            // '다시 보지 않기' 버튼에 클릭 이벤트를 추가합니다. 이 버튼을 클릭하면 환영 화면을 닫고, 다시 표시하지 않도록 설정합니다.
            root.Q<Button>("close_dontshow").RegisterCallback<ClickEvent>(evt =>
            {
                this.Close();
                var importer = AssetImporter.GetAtPath(AssetDatabase.GUIDToAssetPath("bfd03f272fe010b4ba558a3bc456ffeb"));
                importer.userData = "dontshow";
                importer.SaveAndReimport();
            });
            // 닫기 버튼에 클릭 이벤트를 추가합니다. 이 버튼을 클릭하면 환영 화면을 닫습니다.
            root.Q<Button>("close").RegisterCallback<ClickEvent>(evt => { this.Close(); });
        }
    }
}
