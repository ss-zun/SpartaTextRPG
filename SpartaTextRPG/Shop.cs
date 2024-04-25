using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    public class Shop
    {
        private List<Item> items;
        public Shop(List<Item> _items)
        {
            items = _items;
        }

        // 상점 화면
        public void DisplayShopItem()
        {
            Console.WriteLine("< 상점 >");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]\n");
            Console.WriteLine($"{Character.instance.gold} G\n");

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].isSold == true) // 판매된 상태
                {
                    Console.WriteLine($"{items[i].DisplayItem()} | 구매완료");
                }
                else // 구매가능한 아이템 가격표시
                {
                    Console.WriteLine($"{items[i].DisplayItem()} | {items[i].price} G");
                }

            }
            Console.WriteLine("\n1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
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
                        // 아이템 구매
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            Console.Clear();
                            DisplayPurchaseMode();
                            return;
                        // 아이템 판매
                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            Console.Clear();
                            DisplaySaleMode();
                            return;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
            }
        }

        // 구매 화면
        public void DisplayPurchaseMode()
        {
            Console.WriteLine("< 상점 - 아이템 구매 >");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]\n");
            Console.WriteLine($"{Character.instance.gold} G\n");

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].isSold == true) // 판매된 상태
                {
                    Console.WriteLine($"{i + 1} {items[i].DisplayItem()} | 구매완료");
                }
                else // 구매 가능한 아이템 가격 표시
                {
                    Console.WriteLine($"{i + 1} {items[i].DisplayItem()} | {items[i].price} G");
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

                    // 숫자를 입력했다면
                    if (int.TryParse(keyChar.ToString(), out int key))
                    {
                        // 나가기
                        if (key == 0)
                        {
                            Console.Clear();
                            Game.StartScreen();
                            break;
                        }
                        else // 아이템 선택
                        {
                            if(key >= 1 && key <= items.Count)
                            {
                                // 구매불가
                                if (items[key - 1].isSold)
                                {
                                    Console.WriteLine("이미 구매한 아이템입니다.");
                                }
                                else // 구매가 가능 하다면
                                {
                                    if (Character.instance.gold >= items[key - 1].price)
                                    {
                                        Console.WriteLine("구매를 완료했습니다.");
                                        Character.instance.DecreaseGold(items[key - 1].price); // 골드 감소
                                        Character.instance.inventory.AddItem(items[key - 1]); // 인벤토리에 아이템 추가
                                        items[key - 1].completeSale(); // 판매완료 상태 전환
                                        Console.WriteLine("Press Enter...");
                                        Console.ReadLine(); // 아무키 입력 후 화면 전환
                                        Console.Clear();
                                        DisplayShopItem();
                                        break;
                                    }
                                    else // 골드부족
                                    {
                                        Console.WriteLine("Gold 가 부족합니다.");
                                    }
                                }
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

        // 판매 화면
        public void DisplaySaleMode()
        {
            Console.WriteLine("< 상점 - 아이템 판매 >");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]\n");
            Console.WriteLine($"{Character.instance.gold} G\n");

            Console.WriteLine("[아이템 목록]");
            List<Item> characterItems = Character.instance.inventory.items; // 캐릭터 인벤토리 내 아이템
            for (int i = 0; i < characterItems.Count; i++)
            {
                Console.WriteLine($"{i + 1} {characterItems[i].DisplayItem()} | {characterItems[i].price} G");
            }
            Console.WriteLine("\n0. 나가기\n");

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    char keyChar = Console.ReadKey(true).KeyChar; // true 하면 버퍼 비워줌

                    // 숫자를 입력했다면
                    if (int.TryParse(keyChar.ToString(), out int key))
                    {
                        if (key == 0)
                        {
                            Console.Clear();
                            Game.StartScreen();
                            break;
                        }
                        else if (key >= 1 && key <= characterItems.Count)
                        {
                            if (characterItems[key - 1].isEquipped) // 장착중이었다면 해제
                            {
                                characterItems[key - 1].ChangeEquipped();
                            }
                            characterItems[key - 1].availableSale(); // 판매가능 상태로 전환
                            Character.instance.IncreaseGold((int)(characterItems[key - 1].price * 0.85)); // 골드 증가
                            Character.instance.inventory.DropItem(characterItems[key - 1]); // 판매된 아이템 인벤토리에서 제거

                            Console.Clear();
                            DisplayShopItem();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
            }

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
        }
    }
}
