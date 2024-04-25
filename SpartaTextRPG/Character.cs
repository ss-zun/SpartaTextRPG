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
        public CHAD chad { get; private set; }
        public int attackPower { get; private set; }
        public int defensePower { get; private set; }
        public int health { get; private set; }
        public int gold { get; private set; }
        public Character(Inventory _inventory, int _level, string _name, CHAD _chad,
            int _attackPower, int _defensePower,
            int _health, int _gold)
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
        }

        // 방어력 스탯 증가
        public void IncreaseDefense(int itemStat)
        {
            defensePower += itemStat;
        }

        // 방어력 스탯 감소
        public void DecreaseDefense(int itemStat)
        {
            defensePower -= itemStat;
        }

        // 공격력 스탯 증가
        public void IncreaseAttack(int itemStat)
        {
            attackPower += itemStat;
        }

        // 공격력 스탯 감소
        public void DecreaseAttack(int itemStat)
        {
            attackPower -= itemStat;
        }

        // 체력 스탯 감소
        public void DecreaseHealth(int decreasedHealth)
        {
            health -= decreasedHealth;
        }

        // 골드 증가
        public void IncreaseGold(int increaseGold)
        {
            gold += increaseGold;
        }

        // 골드 감소
        public void DecreaseGold(int decreaseGold)
        {
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
    }
}
