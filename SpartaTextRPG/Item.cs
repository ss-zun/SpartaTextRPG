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
        public bool isEquipped { get; private set; }
        public string name { get; private set; }
        public int status { get; private set; }
        public string description { get; private set; }
        public bool isSold { get; private set; }
        public int price { get; private set; }

        public Item(bool _equipped, string _name, int _status, 
            string _description, bool _isSold, int _price)
        {
            isEquipped = _equipped;
            name = _name;
            status = _status;
            description = _description;
            isSold = _isSold;
            price = _price;
        }

        // 아이템 정보 출력
        public virtual string DisplayItem()
        {
            return $"{name} | {status} | {description}";
        }

        // 장착/미장착 변경
        public virtual void ChangeEquipped()
        {
            isEquipped = isEquipped == true ? false : true;
        }

        // 아이템 능력치 반환
        public virtual int GetStatus() {  return 0; }

        // 판매완료 상태 변환
        public virtual void SaleCompleted()
        {
            isSold = true;
        }
    }

    // 아이템 스탯 증가 인터페이스
    public interface IEquipItem
    {
        void EquipItem(Character character);
    }

    // 아이템 스탯 감소 인터페이스
    public interface IUnequipItem
    {
        void UnequipItem(Character character);
    }

    // 방어구 클래스
    public class Armor : Item, IEquipItem, IUnequipItem
    {
        public Armor(bool _equipped, string _name, int _status,
            string _description, bool _isSold, int _price)
            : base(_equipped, _name, _status, _description, _isSold, _price)
        {
        }
        // 방어력 스탯 증가
        public void EquipItem(Character character)
        {
            character.IncreaseDefense(status);
        }

        // 방어력 스탯 감소
        public void UnequipItem(Character character)
        {
            character.DecreaseDefense(status);
        }

        // 방어구 아이템 능력치 반환
        public override int GetStatus() { return status; }

        // 방어구 아이템 출력
        public override string DisplayItem()
        {
            return $"{name}{new string('　', 10-name.Length)} | 방어력 +{status} | {description}";
        }
    }

    // 무기 클래스
    public class Weapon : Item, IEquipItem, IUnequipItem
    {
        public Weapon(bool _equipped, string _name, int _status,
            string _description, bool _isSold, int _price)
            : base(_equipped, _name, _status, _description, _isSold, _price)
        {
        }
        // 공격력 스탯 증가
        public void EquipItem(Character character)
        {
            character.IncreaseAttack(status);
        }

        // 공격력 스탯 감소
        public void UnequipItem(Character character)
        {
            character.DecreaseAttack(status);
        }

        // 무기 아이템 능력치 반환
        public int AttackItemStatus()
        {
            return status;
        }

        // 무기 아이템 능력치 반환
        public override int GetStatus() { return status; }

        // 무기 아이템 출력
        public override string DisplayItem()
        {
            return $"{name}{new string('　', 10-name.Length)} | 공격력 +{status} | {description}";
        }
    }
}

