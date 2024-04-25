using System;
using static System.Net.Mime.MediaTypeNames;

namespace SpartaTextRPG
{
    // internal : 같은 프로젝트 내 클래스끼리 접근허용
    internal class Game
    {
        private static int gameSpeed;
        private static string characterName;
        public static List<Item> items;
        public static Character character;
        public static Shop shop;
        public static Dungeon dungeon;
        // 게임 시작 화면
        public static void StartScreen()
        {
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1.상태 보기");
            Console.WriteLine("2.인벤토리");
            Console.WriteLine("3.상점");
            Console.WriteLine("4.던전입장");
            Console.WriteLine("0.게임종료\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
        }

        // 아이템 초기화
        private static void InitializeItems(List<Item> items)
        {
            items.Add(new Armor(false, "수련자 갑옷", 5, "수련에 도움을 주는 갑옷입니다.", false, 1000));
            items.Add(new Armor(false, "무쇠갑옷", 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", false, 2500));
            items.Add(new Armor(false, "스파르타의 갑옷", 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", false, 3500));
            items.Add(new Weapon(false, "낡은 검", 2, "쉽게 볼 수 있는 낡은 검입니다.", false, 600));
            items.Add(new Weapon(false, "청동 도끼", 5, "어디선가 사용됐던거 같은 도끼입니다.", false, 1500));
            items.Add(new Weapon(false, "스파르타의 창", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", false, 2000));

            // 나만의 새로운 아이템 추가
            items.Add(new Armor(false, "오래된 갑옷", 2, "오래되어서 방어력이 낮은 갑옷입니다.", false, 400));
            items.Add(new Weapon(false, "막대사탕 지팡이", 3, "달달한 사탕으로 만든 지팡이입니다.", false, 900));
        }

        // 초기화 모음
        private static void InitGame()
        {
            gameSpeed = 100;
            characterName = "삐약";
            items = new List<Item>(); // 아이템 목록
            character = new Character(new Inventory(), 1, characterName, CHAD.Warrior, 10, 5, 100, 1500);
            shop = new Shop(items);
            dungeon = new Dungeon();

            InitializeItems(items);
            StartScreen();
        }

        // 업데이트
        private static void UpdateGame()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key; // true 하면 버퍼 비워줌

                    switch (key)
                    {
                        // 게임종료
                        case ConsoleKey.D0:
                        case ConsoleKey.NumPad0:
                            Environment.Exit(0); // 0 : 정상종료
                            break;
                        // 상태보기
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            Console.Clear();
                            character.PrintCharacterInfo();
                            break;
                        // 인벤토리
                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            Console.Clear();
                            character.inventory.DisplayInventory();
                            break;
                        // 상점
                        case ConsoleKey.D3:
                        case ConsoleKey.NumPad3:
                            Console.Clear();
                            shop.DisplayShopItem();
                            break;
                        // 던전입장
                        case ConsoleKey.D4:
                        case ConsoleKey.NumPad4:
                            Console.Clear();
                            dungeon.DisplayDungeonEntrance();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
                Thread.Sleep(gameSpeed);
            }
        }

        static void Main(string[] args)
        {
            // Init
            InitGame();
            // Update
            UpdateGame();
        }
    }
}
