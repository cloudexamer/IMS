using DevExpress.Xpo;

namespace ims.domain
{
    public class InventoryItem : XPObject
    {
        public InventoryItem(Session session) : base(session)
        {
        }

        string itemNumber;
        public string ItemNumber
        {
            get => itemNumber;
            set => SetPropertyValue(nameof(ItemNumber), ref itemNumber, value);
        }

        string itemName;
        public string ItemName
        {
            get => itemName;
            set => SetPropertyValue(nameof(ItemName), ref itemName, value);
        }

        string category;
        public string Category
        {
            get => category;
            set => SetPropertyValue(nameof(Category), ref category, value);
        }

        int quantityOnHand;
        public int QuantityOnHand
        {
            get => quantityOnHand;
            set => SetPropertyValue(nameof(QuantityOnHand), ref quantityOnHand, value);
        }

        decimal unitCost;
        public decimal UnitCost
        {
            get => unitCost;
            set => SetPropertyValue(nameof(UnitCost), ref unitCost, value);
        }
    }
}