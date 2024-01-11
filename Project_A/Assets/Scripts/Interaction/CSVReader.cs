using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVReader
{
    // =====================================================
    // CSVReader������ ���� ������ �޾Ƽ� list ���·� ��ȯ
    // =====================================================


    static string SPLIT_RE = @"\|(?=(?:[^""]*""[^""]*"")*(?![^""]*""))"; // ���� ǥ������ ����Ͽ� CSV ������ �� ���� ������ ����
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r"; // ���� ǥ������ ����Ͽ� CSV ������ �� ���� ������ ����
    static char[] TRIM_CHARS = { '\"' };  // CSV �����Ϳ��� �� ���� ����ǥ�� �����ϱ� ���� ���� �迭

    public static List<Dictionary<string, object>> Read(string file) // CSV ������ �а� �� ���� Dictionary�� ��ȯ�Ͽ� ����Ʈ�� �߰��ϴ� �Լ�
    {
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load("CSV/" + file) as TextAsset; // Resources �������� CSV ������ �о��
        var lines = Regex.Split(data.text, LINE_SPLIT_RE); // ������ �и�
       
        if (lines.Length <= 1)
        {
            return list;
        }

        var header = Regex.Split(lines[0], SPLIT_RE); // CSV ������ ù ��° ���� ���� ������ ��Ÿ��

        for (var i = 1; i < lines.Length; i++) // �� ���� �о Dictionary�� �߰�
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", ""); // �� ���� ����ǥ�� �������� ����
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n)) // ���ڷ� ��ȯ ������ ��� ����ȯ
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue; // Dictionary�� �� �̸��� ���� �߰�
            }
            list.Add(entry); // ����Ʈ�� Dictionary �߰�
        }
        return list;
    }

    public static List<List<object>> Parsing(string file) // CSV ������ �а� �� ���� ����Ʈ�� ��ȯ�Ͽ� ����Ʈ�� �߰��ϴ� �Լ�
    {
        var list = new List<List<object>>();
        TextAsset data = Resources.Load("CSV/" + file) as TextAsset; // Resources �������� CSV ������ �о��

        var lines = Regex.Split(data.text, LINE_SPLIT_RE); // ������ �и�

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE); // CSV ������ ù ��° ���� ���� ������ ��Ÿ��
        for (var i = 1; i < lines.Length; i++) // �� ���� �о ����Ʈ�� �߰�
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new List<object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", ""); // �� ���� ����ǥ�� �������� ����
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n)) // ���ڷ� ��ȯ ������ ��� ����ȯ
                    finalvalue = n;
                else if (float.TryParse(value, out f))
                    finalvalue = f;

                entry.Add(finalvalue); // ����Ʈ�� ���� �߰�
            }
            list.Add(entry); // ����Ʈ�� ����Ʈ �߰�
        }
        return list;
    }
}
