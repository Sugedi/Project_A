using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVReader
{
    // =====================================================
    // CSVReader에서는 엑셀 파일을 받아서 list 형태로 반환
    // =====================================================


    static string SPLIT_RE = @"\|(?=(?:[^""]*""[^""]*"")*(?![^""]*""))"; // 정규 표현식을 사용하여 CSV 파일의 각 열을 나누는 패턴
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r"; // 정규 표현식을 사용하여 CSV 파일의 각 행을 나누는 패턴
    static char[] TRIM_CHARS = { '\"' };  // CSV 데이터에서 양 끝의 따옴표를 제거하기 위한 문자 배열

    public static List<Dictionary<string, object>> Read(string file) // CSV 파일을 읽고 각 행을 Dictionary로 변환하여 리스트에 추가하는 함수
    {
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load("CSV/" + file) as TextAsset; // Resources 폴더에서 CSV 파일을 읽어옴
        var lines = Regex.Split(data.text, LINE_SPLIT_RE); // 행으로 분리
       
        if (lines.Length <= 1)
        {
            return list;
        }

        var header = Regex.Split(lines[0], SPLIT_RE); // CSV 파일의 첫 번째 행은 열의 제목을 나타냄

        for (var i = 1; i < lines.Length; i++) // 각 행을 읽어서 Dictionary에 추가
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", ""); // 양 끝의 따옴표와 역슬래시 제거
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n)) // 숫자로 변환 가능한 경우 형변환
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue; // Dictionary에 열 이름과 값을 추가
            }
            list.Add(entry); // 리스트에 Dictionary 추가
        }
        return list;
    }

    public static List<List<object>> Parsing(string file) // CSV 파일을 읽고 각 행을 리스트로 변환하여 리스트에 추가하는 함수
    {
        var list = new List<List<object>>();
        TextAsset data = Resources.Load("CSV/" + file) as TextAsset; // Resources 폴더에서 CSV 파일을 읽어옴

        var lines = Regex.Split(data.text, LINE_SPLIT_RE); // 행으로 분리

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE); // CSV 파일의 첫 번째 행은 열의 제목을 나타냄
        for (var i = 1; i < lines.Length; i++) // 각 행을 읽어서 리스트에 추가
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new List<object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", ""); // 양 끝의 따옴표와 역슬래시 제거
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n)) // 숫자로 변환 가능한 경우 형변환
                    finalvalue = n;
                else if (float.TryParse(value, out f))
                    finalvalue = f;

                entry.Add(finalvalue); // 리스트에 값을 추가
            }
            list.Add(entry); // 리스트에 리스트 추가
        }
        return list;
    }
}
