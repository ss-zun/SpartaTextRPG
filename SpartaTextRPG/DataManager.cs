using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    internal class DataManager
    {
        private static string dataFilePath = "game_data.json";

        // 데이터 저장
        public static void SaveData<T>(T data)
        {
            var jsonData = JsonSerializer.Serialize(data);
            File.WriteAllText(dataFilePath, jsonData);
        }

        // 데이터 불러오기
        public static T LoadData<T>()
        {
            if (File.Exists(dataFilePath))
            {
                var jsonData = File.ReadAllText(dataFilePath);
                return JsonSerializer.Deserialize<T>(jsonData);
            }
            else
            {
                throw new FileNotFoundException("파일을 찾을 수 없습니다.", dataFilePath);
            }
        }
    }
}
