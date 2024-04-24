using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    public class Shop
    {
        private Character character;
        private List<Item> items;
        public Shop(Character _character, List<Item> _items) 
        { 
            character = _character;
            items = _items;
        }

        // 상점 화면
        public void DisplayShopItem()
        {
            Console.WriteLine("< 상점 >");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]\n");
            Console.WriteLine($"{character.gold} G\n");

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i].isSold == true) // 판매된 상태
                {
                    Console.WriteLine($"{items[i].DisplayItem()} | 구매완료");
                }
                else // 구매가능한 아이템 가격표시
                {
                    Console.WriteLine($"{items[i].DisplayItem()} | {items[i].price} G");
                }
                
            }
            Console.WriteLine("\n1. 아이템 구매");
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
                            DisplayPurchaseMode();
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
            Console.WriteLine($"{character.gold} G\n");

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
                            // 구매불가
                            if (items[key - 1].isSold) 
                            {
                                Console.WriteLine("이미 구매한 아이템입니다.");
                            }
                            else // 구매가 가능 하다면
                            {
                                if(character.gold >= items[key - 1].price)
                                {
                                    Console.WriteLine("구매를 완료했습니다.");
                                    character.DecreaseGold(items[key - 1].price); // 골드 감소
                                    character.inventory.AddItem(items[key - 1]); // 인벤토리에 아이템 추가
                                    items[key - 1].SaleCompleted(); // 판매완료 상태 변환
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
