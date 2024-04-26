using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    public  class DataManager
    {
        public static DataManager instance; // 싱글톤


        // 싱글톤 인스턴스를 가져오기
        public static DataManager GetInstance()
        {
            if (instance == null)
            {
                instance = new DataManager();
            }
            return instance;
        }

        // 데이터 저장
        public void SaveData<T>(T data, string filePath)
        {
            var options = new JsonSerializerOptions
            {
                // 한글을 유니코드로 변환하지 않고 그대로 유지
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true // 들여쓰기 설정
            };
            var jsonData = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, jsonData, System.Text.Encoding.UTF8); // UTF-8로 저장
        }

        // 데이터 불러오기
        public T LoadData<T>(string filePath)
        {
            if (File.Exists(filePath))
            {
                var jsonData = File.ReadAllText(filePath, System.Text.Encoding.UTF8);
                return JsonSerializer.Deserialize<T>(jsonData);
            }
            else
            {
                throw new FileNotFoundException("파일을 찾을 수 없습니다.", filePath);
            }
        }
    }
}
