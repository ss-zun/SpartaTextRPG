using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    // 아이템 클래스
    public class Item
    {
        public bool isEquipped { get; private set; } // 장착중인지
        public string name { get; private set; }
        public double itemValue { get; private set; } // 아이템 능력치값
        public string description { get; private set; } // 아이템 설명
        public bool isSold { get; private set; } // 팔렸는지
        public int price { get; private set; }

        public Item(bool _equipped, string _name, double _itemValue, 
            string _description, bool _isSold, int _price)
        {
            isEquipped = _equipped;
            name = _name;
            itemValue = _itemValue;
            description = _description;
            isSold = _isSold;
            price = _price;
        }

        // 아이템 정보 출력
        public virtual string DisplayItem()
        {
            return $"{name} | {itemValue} | {description}";
        }

        // 장착/미장착 변경
        public virtual void ChangeEquipped()
        {         
            isEquipped = isEquipped == true ? false : true;
        }

        // 아이템 능력치 반환
        public virtual double GetItemValue() {  return 0; }

        // 판매완료 상태 전환
        public virtual void completeSale()
        {
            isSold = true;
        }

        // 판매가능 상태 전환
        public virtual void availableSale()
        {
            isSold = false;
        }
    }

    // 아이템 인터페이스
    public interface IItem
    {
        public void EquipItem(Character character);
        public void UnequipItem(Character character);
    }

    // 방어구 클래스
    public class Armor : Item, IItem
    {
        public Armor(bool _equipped, string _name, double _itemValue,
            string _description, bool _isSold, int _price)
            : base(_equipped, _name, _itemValue, _description, _isSold, _price)
        {
        }
        // 방어력 스탯 증가
        public void EquipItem(Character character)
        {
            character.IncreaseDefense(itemValue);
        }

        // 방어력 스탯 감소
        public void UnequipItem(Character character)
        {
            character.DecreaseDefense(itemValue);
        }

        // 방어구 아이템 능력치 반환
        public override double GetItemValue() { return itemValue; }

        // 방어구 아이템 출력
        public override string DisplayItem()
        {
            return $"{name}{new string('　', 10-name.Length)} | 방어력 +{itemValue} | {description}";
        }
    }

    // 무기 클래스
    public class Weapon : Item, IItem
    {
        public Weapon(bool _equipped, string _name, double _itemValue,
            string _description, bool _isSold, int _price)
            : base(_equipped, _name, _itemValue, _description, _isSold, _price)
        {
        }
        // 공격력 스탯 증가
        public void EquipItem(Character character)
        {
            character.IncreaseAttack(itemValue);
        }

        // 공격력 스탯 감소
        public void UnequipItem(Character character)
        {
            character.DecreaseAttack(itemValue);
        }

        // 무기 아이템 능력치 반환
        public override double GetItemValue() { return itemValue; }

        // 무기 아이템 출력
        public override string DisplayItem()
        {
            return $"{name}{new string('　', 10-name.Length)} | 공격력 +{itemValue} | {description}";
        }
    }
}

