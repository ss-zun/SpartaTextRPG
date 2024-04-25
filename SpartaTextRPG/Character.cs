using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpartaTextRPG
{
    // 직업
    public enum CHAD
    {
        Tank = 1,
        Warrior,
        Wizard,
        Archer,
        Support
    }

    public class Character
    {
        public static Character instance; // 싱글톤
        public Inventory inventory { get; } // 캐릭터의 인벤토리

        // 캐릭터 속성
        public int level { get; private set; }
        public string name { get; private set; }
        public CHAD chad { get; private set; } // 직업
        public double attackPower { get; private set; }
        public double defensePower { get; private set; }
        public double health { get; private set; }
        public int gold { get; private set; }
        public int clearCount { get; private set; } // 클리어 횟수

        public Character(Inventory _inventory, int _level, string _name, CHAD _chad,
            double _attackPower, double _defensePower,
            double _health, int _gold, int _clearCount)
        {
            if (instance == null)
                instance = this;
            inventory = _inventory; // 인벤토리
            level = _level;
            name = _name;
            chad = _chad;
            attackPower = _attackPower;
            defensePower = _defensePower;
            health = _health;
            gold = _gold;
            clearCount = _clearCount;
        }

        // 방어력 스탯 증가
        public void IncreaseDefense(double increaseDefense)
        {
            defensePower += increaseDefense;
        }

        // 방어력 스탯 감소
        public void DecreaseDefense(double decreaseDefense)
        {
            defensePower -= decreaseDefense;
        }

        // 공격력 스탯 증가
        public void IncreaseAttack(double increaseAttack)
        {
            attackPower += increaseAttack;
        }

        // 공격력 스탯 감소
        public void DecreaseAttack(double decreaseAttack)
        {
            attackPower -= decreaseAttack;
        }

        // 체력 스탯 감소
        public void DecreaseHealth(double decreaseHealth)
        {
            health -= decreaseHealth;
        }

        // 골드 증가
        public void IncreaseGold(int increaseGold)
        {
            gold += increaseGold;
        }

        // 골드 감소
        public void DecreaseGold(int decreaseGold)
        {
            Console.WriteLine(decreaseGold);
            gold -= decreaseGold;
        }

        // 캐릭터 정보 출력
        public void PrintCharacterInfo()
        {
            Console.WriteLine("< 상태 보기 >");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"레벨: {level.ToString("00")}");
            Console.WriteLine($"이름: {name}");
            Console.WriteLine($"직업: {GetChadName()}");
            Console.WriteLine($"공격력: {attackPower} (+{inventory.TotalAttackItemStatus()})");
            Console.WriteLine($"방어력: {defensePower} (+{inventory.TotalDefenseItemStatus()})");
            Console.WriteLine($"체력: {health}");
            Console.WriteLine($"골드: {gold} G\n");

            Console.WriteLine("0. 나가기\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

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

        // 직업 이름 변환
        private string GetChadName()
        {
            switch (chad)
            {
                case CHAD.Tank:
                    return "탱커";
                case CHAD.Warrior:
                    return "전사";
                case CHAD.Wizard:
                    return "마법사";
                case CHAD.Archer:
                    return "궁수";
                case CHAD.Support:
                    return "서포터";
                default:
                    return chad.ToString();
            }
        }

        // 휴식하기
        public void Rest()
        {
            Console.WriteLine("< 휴식하기 >");
            Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {gold} G)\n");

            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

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
                        // 휴식하기
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            CanRest();
                            Console.WriteLine("Press Enter...");
                            Console.ReadLine(); // 아무키 입력 후 화면 전환
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

        // 휴식이 가능한지 판단
        public void CanRest()
        {
            // 보유 금액이 충분하다면
            if(gold - 500 > 0)
            {
                health = 100; // 체력회복 100까지만
                Console.WriteLine("휴식을 완료했습니다.");
            }
            else // 보유 금액 부족
            {
                Console.WriteLine("골드가 부족합니다.");
            }
        }

        // 클리어 횟수 증가
        public void IncreaseClearCount()
        {
            clearCount += 1;
        }

        // 레벨업
        public void LevelUp()
        {
            level += 1;
            clearCount = 0;
            attackPower += 0.5;
            defensePower += 1.0;
        }
    }
}
