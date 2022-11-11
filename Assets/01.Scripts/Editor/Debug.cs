using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.SceneManagement;

public class DebugWindow : EditorWindow
{
    private static int count = 0;
    [MenuItem("AutoTextFix/ChangeText/������ü")]
    static void ChangeText1()
    {
        ChangeText("Assets/09.Fonts/AutoFix/BMEULJIROTTF.asset");
    }

    [MenuItem("AutoTextFix/ChangeText/�޼�ü")]
    static void ChangeText2()
    {
        ChangeText("Assets/09.Fonts/AutoFix/DalseoHealingBold SDF.asset");
    }
    [MenuItem("AutoTextFix/LoadScene/��Ʋ���ѱ��")]
    static void ToInfinityAndBeyond()
    {
        SceneLoad.Instance.ToBattleScene(true, "Fox", 3, 10);
    }

    [MenuItem("AutoTextFix/Debug")]
    static void Init()
    {
        DebugWindow window = EditorWindow.GetWindow<DebugWindow>("BattleDebug");
        window.Show();
    }

    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;
    void OnGUI()
    {
        if(GUILayout.Button("Kill All Enemies"))
        {
            KillAllEnemy();
        }
    }

    private void KillAllEnemy()
    {
        for(int i = 0; i < TurnBattle.Inst.Enemy.Count; i++)
        {
            TurnBattle.Inst.Enemy[i].GetComponent<BattleCharacter>().myStat.curHP = 0;
        }
    }
    private static void ChangeText(string str)
    {
        count = 0;
        GameObject[] rootObj = GetSceneRootObject();

        for (int i = 0; i < rootObj.Length; i++)
        {
            GameObject gbj = rootObj[i];
            Component[] com = gbj.transform.GetComponentsInChildren(typeof(TextMeshProUGUI), true);

            foreach (TextMeshProUGUI txt in com)
            {
                txt.font = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(str);
                //Debug.Log(txt.name);//�ؽ�Ʈ�޽����� �������ִ� ������Ʈ �̸� ���
                count++;
            }
        }
        Debug.Log("TextMeshProUGUI�� ������ ������Ʈ ���� : " + count); //�� �ٲ� ��Ʈ ����
    }


    private static GameObject[] GetSceneRootObject()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.GetRootGameObjects();
    }
}
