namespace Web_253502_Alkhovik.Domain.Entities
{
    public class Cart
    {
        public Dictionary<int, CartItem> Items { get; set; } = new();
        public virtual void AddToCart(Car car)
        {
            if (Items.ContainsKey(car.Id))
                ++Items[car.Id].Amount;
            else
                Items.Add(car.Id, new CartItem { Item = car, Amount = 1});
        }
        public virtual void RemoveItems(int id)
        {
            if (Items.ContainsKey(id) && --Items[id].Amount <= 0)
            {
                Items.Remove(id);
            }
        }
        public virtual void ClearAll()
        {
            Items.Clear();
        }
        public int Count { get => Items.Sum(item => item.Value.Amount); }
        public decimal CountPrice { get => Items.Sum(item => item.Value.Item.Price * item.Value.Amount); }

        public int TotalAmount { get => Items.Sum(item => item.Value.Item.Amount * item.Value.Amount); }
    }
}