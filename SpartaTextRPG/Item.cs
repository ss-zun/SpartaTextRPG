using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    public enum ITEM_TYPE
    {
        Armor =1,
        Weapon
    }

    // 아이템 클래스
    public class Item
    {
        public ITEM_TYPE type { get; protected set; }
        public bool isEquipped { get; private set; } // 장착중인지
        public string name { get; private set; }
        public double value { get; private set; } // 아이템 능력치값
        public string description { get; private set; } // 아이템 설명
        public bool isSold { get; private set; } // 팔렸는지
        public int price { get; private set; }

        public Item(ITEM_TYPE type, bool isEquipped, string name, double value, 
            string description, bool isSold, int price)
        {
            this.type = type;
            this.isEquipped = isEquipped;
            this.name = name;
            this.value = value;
            this.description = description;
            this.isSold = isSold;
            this.price = price;
        }

        // 아이템 정보 출력
        public virtual string DisplayItem()
        {
            return $"{name} | {value} | {description}";
        }

        // 장착/미장착 변경
        public virtual void ChangeEquipped()
        {         
            isEquipped = isEquipped == true ? false : true;
        }

        // 아이템 능력치 반환
        public virtual double GetItemValue() { return value; }

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
        public void EquipItem(Character character); // 스탯 증가
        public void UnequipItem(Character character); // 스탯 감소
    }

    // 방어구 클래스
    public class Armor : Item, IItem
    {
        public Armor(ITEM_TYPE _type, bool _equipped, string _name, double _value,
            string _description, bool _isSold, int _price)
            : base(_type, _equipped, _name, _value, _description, _isSold, _price)
        {
        }
        // 방어력 스탯 증가
        public void EquipItem(Character character)
        {
            character.IncreaseDefense(value);
        }

        // 방어력 스탯 감소
        public void UnequipItem(Character character)
        {
            character.DecreaseDefense(value);
        }

        // 방어구 아이템 능력치 반환
        //public override double GetItemValue() { return itemValue; }

        // 방어구 아이템 출력
        //public override string DisplayItem()
        //{
        //    return $"{name}{new string('　', 10-name.Length)} | 방어력 +{itemValue} | {description}";
        //}
    }

    // 무기 클래스
    public class Weapon : Item, IItem
    {
        public Weapon(ITEM_TYPE _type, bool _equipped, string _name, double _value,
            string _description, bool _isSold, int _price)
            : base(_type, _equipped, _name, _value, _description, _isSold, _price)
        {
        }
        
        // 공격력 스탯 증가
        public void EquipItem(Character character)
        {
            character.IncreaseAttack(value);
        }

        // 공격력 스탯 감소
        public void UnequipItem(Character character)
        {
            character.DecreaseAttack(value);
        }


        // 무기 아이템 능력치 반환
        //public override double GetItemValue() { return itemValue; }

        // 무기 아이템 출력
        //public override string DisplayItem()
        //{
        //    return $"{name}{new string('　', 10-name.Length)} | 공격력 +{itemValue} | {description}";
        //}
    }
}

