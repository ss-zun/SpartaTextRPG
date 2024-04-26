using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpartaTextRPG
{
    public class Inventory
    {
        public List<Item> items { get; private set; } // 아이템 목록

        public Inventory(List<Item> items) 
        {
            this.items = items;
        }

        // 아이템을 인벤토리에 추가
        public void AddItem(Item item)
        {
            items.Add(item);
        }

        // 아이템을 인벤토리에서 제거
        public void DropItem(Item item)
        {
            items.Remove(item);
        }

        // 장착중인 공격력 아이템 능력치를 모두 더하여 반환
        public double TotalAttackItemStatus()
        {
            double totalItemValue = 0;
            foreach (Item item in items)
            {
                if (item.isEquipped == true && item.type == ITEM_TYPE.Weapon)
                    totalItemValue += item.GetItemValue();
            }
            return totalItemValue;
        }

        // 장착중인 방어력 아이템 능력치를 모두 더하여 반환
        public double TotalDefenseItemStatus()
        {
            double totalItemValue = 0;
            foreach (Item item in items)
            {
                if (item.isEquipped == true && item.type == ITEM_TYPE.Armor)
                    totalItemValue += item.GetItemValue();
            }
            return totalItemValue;
        }

        // 인벤토리 아이템 목록 화면
        public void DisplayInventory()
        {
            Console.WriteLine("< 인벤토리 >");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].isEquipped == true) // 장착중
                {
                    Console.WriteLine($"[E]{items[i].DisplayItem()}");
                }
                else // 미장착
                {
                    Console.WriteLine($"{items[i].DisplayItem()}");
                }           
            }
            Console.WriteLine("\n1. 장착 관리");
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
                        // 장착 관리
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            Console.Clear();
                            DisplayEquipmentMode();
                            return;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
            }
        }

        // 장착 관리
        public void DisplayEquipmentMode()
        {
            Console.WriteLine("< 인벤토리 - 장착 관리 >");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].isEquipped == true) // 장착중
                {
                    Console.WriteLine($"{i + 1} [E]{items[i].DisplayItem()}");
                }
                else // 미장착
                {
                    Console.WriteLine($"{i + 1} {items[i].DisplayItem()}");
                }
            }
            Console.WriteLine("\n0. 나가기\n");

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    char keyChar = Console.ReadKey(true).KeyChar; // true 하면 버퍼 비워줌

                    if (int.TryParse(keyChar.ToString(), out int key))
                    {
                        if (key == 0) // 나가기
                        {
                            Console.Clear();
                            Game.StartScreen();
                            break;
                        }
                        else
                        {
                            if (key >= 1 && key <= items.Count)
                            {
                                if (items[key - 1].isEquipped == false) // 미장착 상태
                                {
                                    // 같은 타입의 아이템이 장착중인지 체크
                                    foreach (Item item in items) // item : 인벤토리 내 아이템들
                                    {
                                        if (item.isEquipped && item.type == items[key - 1].type)
                                        {
                                            item.ChangeEquipped(); // 기존 아이템 해제
                                            break;
                                        }
                                    }
                                }
                                // 현재 아이템 장착
                                items[key - 1].ChangeEquipped();
                                Console.Clear();
                                DisplayInventory();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }     
                }
            }
        }
    }
}
