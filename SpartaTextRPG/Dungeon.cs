using SpartaTextRPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    public class Dungeon
    {
        protected double recommendedDefense; // 권장 방어력
        protected int goldReward; // 기본 골드 보상
        protected string dungeonLevel; // 던전 난이도

        // 던전 입구
        public void DisplayDungeonEntrance()
        {
            Console.WriteLine("< 던전입장 >");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            Console.WriteLine("1. 쉬운 던전".PadRight(10) + "| 방어력 5 이상 권장");
            Console.WriteLine("2. 일반 던전".PadRight(10) + "| 방어력 11 이상 권장");
            Console.WriteLine("3. 어려운 던전".PadRight(10) + "| 방어력 17 이상 권장");
            Console.WriteLine("0. 나가기\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key; // true 하면 버퍼 비워줌

                    switch (key)
                    {
                        // 나가기
                        case ConsoleKey.D0:
                        case ConsoleKey.NumPad0:
                            Console.Clear();
                            Game.StartScreen();
                            return;
                        // 쉬운 던전
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            Console.Clear();
                            EasyDungeon easyDungeon = new EasyDungeon();
                            easyDungeon.CheckDefense();
                            return;
                        // 일반 던전
                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            Console.Clear();
                            NormalDungeon normalDungeon = new NormalDungeon();
                            normalDungeon.CheckDefense();
                            return;
                        // 어려운 던전
                        case ConsoleKey.D3:
                        case ConsoleKey.NumPad3:
                            Console.Clear();
                            HardDungeon hardDungeon = new HardDungeon();
                            hardDungeon.CheckDefense();
                            return;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
            }
        }

        // 방어력으로 던전을 수행할 수 있을지 판단
        public void CheckDefense()
        {
            int p = new Random().Next(0, 10);
            // 권장 방어력보다 낮고, 40% 확률로 던전 실패
            if ((Character.instance.defensePower < recommendedDefense) && (p < 4))
            {
                FailedDungeon();
            }
            else
            {
                ClearDungeon();
            }
        }

        // 던전 실패
        public void FailedDungeon()
        {        
            Console.WriteLine("< 던전 클리어 실패 >");
            // 보상 없고, 체력 절반 감소
            double oldHealth = Character.instance.health;
            int decreaseHealth = (int)(Character.instance.health / 2);
            Character.instance.DecreaseHealth(decreaseHealth); // 캐릭터 체력 감소
            Console.WriteLine($"체력 {oldHealth} -> {Character.instance.health}\n");

            Console.WriteLine("Press AnyKey");
            Console.ReadLine(); // 아무키 입력 후 화면 전환
            Console.Clear();
            DisplayDungeonEntrance();
        }

        // 던전 클리어
        public void ClearDungeon()
        {
            Console.WriteLine("< 던전 클리어 >");
            Console.WriteLine("축하합니다!!");
            Console.WriteLine($"{dungeonLevel}을 클리어 하였습니다.\n");

            Console.WriteLine("[탐험 결과]");
            // 체력 감소
            double oldHealth = Character.instance.health; // 기존의 내 방어력
            int defenseDifference = (int)(Character.instance.defensePower - recommendedDefense); // 내 방어력 - 권장 방어력
            int decreaseHealth = new Random().Next(20 + defenseDifference, 35 + 1 + defenseDifference); // 20(-+@)~35(-+@)
            Character.instance.DecreaseHealth(decreaseHealth); // 캐릭터 체력 감소
            Console.WriteLine($"체력 {oldHealth} -> {Character.instance.health}");

            // 골드 보상
            int oldGold = Character.instance.gold; // 기존의 내 골드
            double randReward = 0.1 + new Random().NextDouble() * 0.1; // 10~20%
            int totalReward = goldReward + (int)(Character.instance.attackPower * randReward); // 기본 골드 보상 + 공격력 대비 추가 보상
            Character.instance.IncreaseGold(totalReward); // 캐릭터 골드 증가
            Console.WriteLine($"골드 {oldGold} G -> {Character.instance.gold} G\n");

            // 클리어 횟수 증가
            Character.instance.IncreaseClearCount();
            // 레벨업 조건 달성시 레벨업
            if (Character.instance.level == Character.instance.clearCount)
            {
                Character.instance.LevelUp();
            }
            

            Console.WriteLine("0. 나가기\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key; // true 하면 버퍼 비워줌

                    switch (key)
                    {
                        // 나가기
                        case ConsoleKey.D0:
                        case ConsoleKey.NumPad0:
                            Console.Clear();
                            Game.StartScreen();
                            return;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
            }
        }
    }

    // 쉬운 던전
    public class EasyDungeon : Dungeon
    {
        public EasyDungeon()
        {
            recommendedDefense = 5.0;
            goldReward = 1000;
            dungeonLevel = "쉬운 던전";
        }
    }

    // 일반 던전
    public class NormalDungeon : Dungeon
    {
        public NormalDungeon()
        {
            recommendedDefense = 11.0;
            goldReward = 1700;
            dungeonLevel = "일반 던전";
        }
    }

    // 어려운 던전
    public class HardDungeon : Dungeon
    {
        public HardDungeon()
        {
            recommendedDefense = 17.0;
            goldReward = 2500;
            dungeonLevel = "어려운 던전";
        }
    }
}




